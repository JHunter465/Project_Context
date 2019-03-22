using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject CharacterCreationWindow;

    private void Awake()
    {
        Instance = Instance ?? this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            CloseWindow(CharacterCreationWindow);
        }
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
