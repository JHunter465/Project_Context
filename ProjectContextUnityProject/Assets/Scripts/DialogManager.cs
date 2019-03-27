using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGTALK;

public class DialogManager : MonoBehaviour
{
    [SerializeField] RPGTalk RPGTalk;
    [SerializeField] GameObject authenticationWindow;
    [SerializeField] GameObject answerWindow;
    [SerializeField] GameObject eventSystem;
    [SerializeField] Player player;

    string linePartA = "cutscene";
    public int currentDialog = 1;
    int currentQuestion = 0;
    string linePartBStart = "_start";
    string linePartBEnd = "_end";

    void Start()
    {
        eventSystem = GameObject.Find("EventSystem");
        Invoke("SetLinesPositions", 1);
        RPGTalk.OnMadeChoice += OnMadeChoice;
        player.canMove = false;
        
        //RPGTalk.NewTalk(RPGTalk.lineToStart, RPGTalk.lineToBreak);
    }

    private void Update()
    {
        if (Input.GetButtonDown("XboxA"))
            ApproveAuthentication();
    }

    void SetLinesPositions()
    {
        RPGTalk.lineToStart = linePartA + currentDialog.ToString() + linePartBStart;
        RPGTalk.lineToBreak = linePartA + currentDialog.ToString() + linePartBEnd;
    }

    public void ApproveAuthentication()
    {
        UIManager.Instance.CloseWindow(answerWindow);
        UIManager.Instance.CloseWindow(authenticationWindow);
        eventSystem.SetActive(true);
        RPGTalk.enabled = true;
    }

    public void ReturnMovement()
    {
        player.canMove = true;
    }

    public void OpenAuthenticationWindow()
    {
        UIManager.Instance.OpenWindow(authenticationWindow);
        eventSystem.SetActive(false);
    }

    public void OpenAnswerWindow()
    {
        UIManager.Instance.OpenWindow(answerWindow);
        RPGTalk.enabled = false;
    }

    public void HandleDialog()
    {
        switch (currentDialog)
        {
            default:
            case 1:
                BeginNextDialog();
                break;
            case 2:
                OpenAnswerWindow();
                break;
            case 3:
                ReturnMovement();
                break;
            case 4:
                OpenAnswerWindow();
                break;
            case 5:
                ReturnMovement();
                break;
            case 6:
                OpenAnswerWindow();
                break;
            case 7:
                ReturnMovement();
                break;
            case 8:
                //OpenAnswerWindow();
                break;
            case 9:
                //OpenAnswerWindow();
                break;
        }
    }

    public void BeginNextDialog()
    {
        currentDialog++;    
        SetLinesPositions();
        RPGTalk.NewTalk(RPGTalk.lineToStart, RPGTalk.lineToBreak);
        player.canMove = false;
    }

    void OnMadeChoice(string questionId, int choiceID)
    {
        if (choiceID == 0)
        {
            Debug.Log("0");
            BeginNextDialog();
        }
        else
        {
            Debug.Log("1");
            LevelManager.Instance.RestartLevel();
        }
    }
}
