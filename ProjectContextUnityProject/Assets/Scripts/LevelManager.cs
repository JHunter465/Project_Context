using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int CurrentLevel;
    private bool isSwitching;

    public List<string> SceneList;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(Instance);
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
