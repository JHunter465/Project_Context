using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCreation : MonoBehaviour
{
    private string playerName = "";
    private Color playerColor = Color.white;

    [SerializeField] TMP_InputField nameTextField;
    [SerializeField] TMP_Dropdown colorOptions;
    [SerializeField] GameObject authenticationOverlay;

    public void SetPlayerSettings()
    {
        playerName = nameTextField.text;

        switch (colorOptions.value)
        {
            default:
            case 0:
                playerColor = Color.red;
                break;
            case 1:
                playerColor = Color.green;
                break;
            case 2:
                playerColor = Color.blue;
                break;
        }

        Debug.Log("name: " + playerName);
        Debug.Log("Color: " + playerColor);

        StartAuthentication();
    }

    public void StartAuthentication()
    {
        authenticationOverlay.SetActive(true);
    }
}
