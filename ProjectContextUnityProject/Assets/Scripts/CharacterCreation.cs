using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCreation : MonoBehaviour
{
    private string[] playerName = new string[2];
    private Color hairColor = Color.white;
    private Color headColor = Color.white;
    private Color shirtColor = Color.white;

    public int CurrentScreenIndex = 0;

    [SerializeField] TMP_InputField[] nameTextField;

    [SerializeField] TMP_Dropdown[] hairColorOptions;
    [SerializeField] TMP_Dropdown[] headColorOptions;
    [SerializeField] TMP_Dropdown[] shirtColorOptions;

    [SerializeField] GameObject authenticationOverlay;

    //ZET EVENTSYSTEM UIT

    public void SetPlayerSettings()
    {
        playerName[CurrentScreenIndex] = nameTextField[CurrentScreenIndex].text;
        hairColor = SetColor(hairColorOptions[CurrentScreenIndex].value);
        headColor = SetColor(headColorOptions[CurrentScreenIndex].value);
        shirtColor = SetColor(shirtColorOptions[CurrentScreenIndex].value);

        LevelManager.Instance.PlayerName = playerName;
        LevelManager.Instance.HairColor[CurrentScreenIndex] = hairColor;
        LevelManager.Instance.HeadColor[CurrentScreenIndex] = headColor;
        LevelManager.Instance.ShirtColor[CurrentScreenIndex] = shirtColor;

        if (CurrentScreenIndex != 0)
            StartAuthentication();
    }

    Color SetColor(int _value)
    {
        switch (_value)
        {
            default:
            case 0:
                return Color.red;
            case 1:
                return Color.green;
            case 2:
                return Color.blue;
            case 3:
                return Color.magenta;
            case 4:
                return Color.cyan;
            case 5:
                return Color.yellow;
            case 6:
                return Color.white;
            case 7:
                return Color.black;
        }
    }

    public void GoToRealWindow()
    {
        UIManager.Instance.CloseWindow(UIManager.Instance.FakeCharacterWindow);
        UIManager.Instance.OpenWindow(UIManager.Instance.CharacterCreationWindow);
        SetPlayerSettings();
        CurrentScreenIndex++;
    }

    public void StartAuthentication()
    {
        authenticationOverlay.SetActive(true);
        GameObject.Find("EventSystem").SetActive(false);
    }


}
