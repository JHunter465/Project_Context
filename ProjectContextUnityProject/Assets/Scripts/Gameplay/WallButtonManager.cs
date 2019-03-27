using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallButtonManager : MonoBehaviour
{
    public List<WallButton> ButtonList;
    public List<LeveredDoor> DoorList;

    void Awake()
    {
        foreach (var _wallButton in ButtonList)
        {
            _wallButton.Manager = this;
        }
    }

    public void CheckButtons()
    {
        if (AllButtonsActive())
            OpenDoors();
    }

    public void OpenDoors()
    {
        foreach (var _door in DoorList)
        {
            _door.openClose();
        }
    }

    bool AllButtonsActive()
    {
        bool _returnState = true;

        foreach (var _wallButton in ButtonList)
        {
            if (!_wallButton.isPressed)
                _returnState = false;
        }

        return _returnState;
    }
}
