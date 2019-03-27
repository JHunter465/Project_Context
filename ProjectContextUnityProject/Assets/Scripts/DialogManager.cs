using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGTALK;

public class DialogManager : MonoBehaviour
{
    [SerializeField] RPGTalk RPGTalk;

    string linePartA = "cutscene";
    int currentDialog = 1;
    int currentQuestion = 0;
    string linePartBStart = "_start";
    string linePartBEnd = "_end";

    void Start()
    {
        Invoke("SetLinesPositions", 1);
        RPGTalk.OnMadeChoice += OnMadeChoice;
        //RPGTalk.NewTalk(RPGTalk.lineToStart, RPGTalk.lineToBreak);
    }

    void SetLinesPositions()
    {
        RPGTalk.lineToStart = linePartA + currentDialog.ToString() + linePartBStart;
        RPGTalk.lineToBreak = linePartA + currentDialog.ToString() + linePartBEnd;
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

                break;
        }
    }

    public void BeginNextDialog()
    {
        currentDialog++;    
        SetLinesPositions();
        RPGTalk.NewTalk(RPGTalk.lineToStart, RPGTalk.lineToBreak);
    }

    void OnMadeChoice(string questionId, int choiceID)
    {
        if (choiceID == 0)
        {
            Debug.Log("0");
        }
        else
        {
            Debug.Log("1");
            LevelManager.Instance.RestartLevel();
        }
    }
}
