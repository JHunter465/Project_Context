using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int CurrentLevel;
    private bool isSwitching;

    public List<string> SceneList;

    public Color[] HairColor = new Color[2];
    public Color[] HeadColor = new Color[2];
    public Color[] ShirtColor = new Color[2];

    public SpriteRenderer HairSprite;
    public SpriteRenderer HeadSprite;
    public SpriteRenderer ShirtSprite;

    public string[] PlayerName = new string[2];

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(Instance);
    }

    public void UpdatePlayerColors()
    {
        if (CurrentLevel < 2)
        {
            HeadSprite.color = HeadColor[1];
            HairSprite.color = HairColor[1];
            ShirtSprite.color = ShirtColor[1];
        }
        else
        {
            HeadSprite.color = HeadColor[0];
            HairSprite.color = HairColor[0];
            ShirtSprite.color = ShirtColor[0];
        }
    }

    public void OnExit()
    {
        if (isSwitching)
            return;
        isSwitching = true;
        UIManager.Instance.FadeOut();
        Invoke("NextLevel", UIManager.Instance.FadeDelay + UIManager.Instance.FadeDuration);
    }

    public void NextLevel()
    {
        CurrentLevel++;
        SceneManager.LoadScene(CurrentLevel, LoadSceneMode.Single);
        isSwitching = false;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(CurrentLevel, LoadSceneMode.Single);
        isSwitching = false;
    }
}
