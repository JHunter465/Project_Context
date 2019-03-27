using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject FakeCharacterWindow;
    public GameObject CharacterCreationWindow;

    [Header("Settings: ")]
    [SerializeField] Image fadeImage;
    public float FadeDelay;
    [Tooltip("The time it takes to completely fade")] public float FadeDuration;
    [SerializeField] AnimationCurve fadeCurve;

    bool isFading = false;

    Coroutine fadeRoutine;

    private void Awake()
    {
        Instance = this;

        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);
    }

    private void Start()
    {
        FadeIn();
    }

    private void Update()
    {
        if (Input.GetButtonDown("XboxA") && LevelManager.Instance.CurrentLevel == 0)
        {
            CloseWindow(CharacterCreationWindow);
            LevelManager.Instance.NextLevel();
        }

#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Q))
        {
            CloseWindow(CharacterCreationWindow);
            LevelManager.Instance.NextLevel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            FadeOut();

        if (Input.GetKeyDown(KeyCode.B))
            FadeIn();
#endif
    }

    public void FadeOut()
    {
        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(IFadeOut());
    }

    public void FadeIn()
    {
        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(IFadeIn());
    }

    IEnumerator IFadeOut()
    {
        float _tweenTime = 0;
        isFading = true;

        Color _startColor = fadeImage.color;

        yield return new WaitForSeconds(FadeDelay);

        while (_tweenTime < 1)
        {
            _tweenTime += Time.deltaTime / FadeDuration;
            float _tweenKey = fadeCurve.Evaluate(_tweenTime);

            fadeImage.color = LerpAlpha(_startColor, 1, _tweenKey);

            yield return null;
        }

        isFading = false;
        yield return null;
    }
    IEnumerator IFadeIn()
    {
        float _tweenTime = 0;
        isFading = true;

        Color _startColor = fadeImage.color;

        yield return new WaitForSeconds(FadeDelay);

        while (_tweenTime < 1)
        {
            _tweenTime += Time.deltaTime / FadeDuration;
            float _tweenKey = fadeCurve.Evaluate(_tweenTime);

            fadeImage.color = LerpAlpha(_startColor, 0, _tweenKey);

            yield return null;
        }

        isFading = false;
        yield return null;
    }

    Color LerpAlpha(Color c, float a, float t)
    {
        Color _returnColor = Color.Lerp(c, new Color(c.r, c.g, c.b, a), t);
        return _returnColor;
    }


    public void CloseWindow(GameObject _window)
    {
        _window.SetActive(false);
    }
    public void OpenWindow(GameObject _window)
    {
        _window.SetActive(true);
    }
}
