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
        RPGTalk.variables[0].variableValue = LevelManager.Instance.CurrentLevel < 6 ? LevelManager.Instance.PlayerName[1] : LevelManager.Instance.PlayerName[0];
        eventSystem = GameObject.Find("EventSystem");
        Invoke("SetLinesPositions", 1);
        RPGTalk.OnMadeChoice += OnMadeChoice;
        player.canMove = false;
        RPGTalk.NewTalk(RPGTalk.lineToStart, RPGTalk.lineToBreak);
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
        RPGTalk.enabled = false;
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
                OpenAuthenticationWindow();
                break;
            case 9:
                ReturnMovement();
                break;
            case 10:
                OpenAnswerWindow();
                break;
            case 11:
                ReturnMovement();
                break;
            case 12:
                OpenAnswerWindow();
                break;
            case 13:
                ReturnMovement();
                break;
            case 14:
                OpenAnswerWindow();
                break;
            case 15:
                BeginNextDialog();
                break;
            case 16:
                OpenAuthenticationWindow();
                break;
            case 17:
                ReturnMovement();
                break;
            case 18:
                OpenAnswerWindow();
                break;
            case 19:
                BeginNextDialog();
                break;
            case 20:
                OpenAnswerWindow();
                break;
            case 21:
                ReturnMovement();
                break;
            case 22:
                OpenAnswerWindow();
                break;
            case 23:
                BeginNextDialog();
                break;
            case 24:
                OpenAuthenticationWindow();
                break;
            case 25:
                ReturnMovement();
                break;
            case 26:
                OpenAnswerWindow();
                break;
            case 27:
                ReturnMovement();
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
