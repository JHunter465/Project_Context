using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallButton : MonoBehaviour
{
    [Tooltip("When true, the button deactivates after a certain amount of time")]
    public bool IsTimed;
    [Tooltip("Time it takes before deactivation")]
    public float TimeBeforeDeactivation = 10;
    float deactivationTimer;

    [HideInInspector]
    public WallButtonManager Manager;


    public bool isPressed;

    void Start()
    {
        deactivationTimer = TimeBeforeDeactivation;
    }

    void Update()
    {
        if(IsTimed && isPressed)
        {
            if (deactivationTimer > 0)
                deactivationTimer -= Time.deltaTime;
            else
            {
                deactivationTimer = TimeBeforeDeactivation;
                DeactivateButton();
            }
        }
    }

    void ActivateButton()
    {
        isPressed = true;
        Manager.CheckButtons();
    }

    void DeactivateButton()
    {
        isPressed = false;
    }

    public void InteractWithButton(Vector2 _dir)
    {
        if (IsAllignedWithButton(_dir))
            ActivateButton();
    }

    bool IsAllignedWithButton(Vector2 _dir)
    {
        bool _return = false;

        switch (transform.localEulerAngles.z)
        {
            default:
            case 0:
                if (_dir.y > 0f)
                    _return = true;                       
                break;
            case 90:
                if (_dir.x < 0f)
                    _return = true;
                break;
            case 180:
                if (_dir.y < 0f)
                    _return = true;
                break;
            case 270:
                if (_dir.x > 0f)
                    _return = true;
                break;

        }
        return _return;
    }
}
