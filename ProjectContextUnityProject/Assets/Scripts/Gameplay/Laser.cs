using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public bool IsOn = true;
    public bool PingPongRotation = false;
    [Range(0, 3)]
    public float RotationDuration = 0.2f;
    public float RotationAngle = 90;

    public AnimationCurve RotationCurve;
    Coroutine rotationRoutine;

    private bool isRotating = false;
    private Vector3 originalEulerAngles;
    LineRenderer lineRenderer;

    private void Awake()
    {
        originalEulerAngles = transform.localEulerAngles;
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (IsOn)
            BeamLaser();

        if (Input.GetKeyDown(KeyCode.Space) && !isRotating)
            RotateLaser();
    }

    public void RotateLaser(bool _clockWise = true)
    {
        Debug.Log("ROTATE OUWE");
        isRotating = true;
        StartRotationRoutine();
        // int rotvalue = _clockWise ? -90 : 90;
        //transform.Rotate(Vector3.forward * rotvalue);
    }

    void StartRotationRoutine()
    {
        if (rotationRoutine != null) StopCoroutine(rotationRoutine);
        rotationRoutine = StartCoroutine(ISmoothRotateLaser());
    }

    public void ResetLaser()
    {
        if (rotationRoutine != null) StopCoroutine(rotationRoutine);
        transform.localEulerAngles = originalEulerAngles;
        isRotating = false;
    }

    IEnumerator ISmoothRotateLaser()
    {
        Vector3 _a = PingPongRotation ? originalEulerAngles : transform.localEulerAngles;
        Vector3 _b = PingPongRotation ? originalEulerAngles + new Vector3(0, 0, RotationAngle) :
        transform.localEulerAngles + new Vector3(0, 0, RotationAngle);

        bool _flip = false;

        if (transform.localEulerAngles != originalEulerAngles)
            _flip = true;

        float _tweenTime = 0;

        while (_tweenTime < 1)
        {
            _tweenTime += Time.deltaTime / RotationDuration;
            float _tweenKey = RotationCurve.Evaluate(_tweenTime);

            if (_flip)
                transform.rotation = RotationLerp(_b, _a, _tweenKey);
            else
                transform.rotation = RotationLerp(_a, _b, _tweenKey);

            yield return null;
        }

        isRotating = false;
        yield return null;
    }

    Quaternion RotationLerp(Vector3 a, Vector3 b, float t)
    {
        return Quaternion.Lerp(Quaternion.Euler(a), Quaternion.Euler(b), t);
    }

    void BeamLaser()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
        Debug.DrawRay(transform.position, transform.right);

        if (hit.collider != null)
        {
            if(hit.collider.tag == "Player")
            {
                //Resets the player
                hit.collider.GetComponent<Player>().TeleportToStart();
                
                //Finds all lasers and resets them
                Laser[] _lasers = FindObjectsOfType<Laser>();
                foreach (var _laser in _lasers)
                {
                    _laser.ResetLaser();
                }
            }

            Vector3 _posA = new Vector3(transform.position.x, transform.position.y, -1);
            Vector3 _posB = new Vector3(hit.point.x, hit.point.y, -1);

            lineRenderer.SetPosition(0, _posA);
            lineRenderer.SetPosition(1, _posB);
        }

        //else
        //{
        //    Vector3 _posA = new Vector3(transform.position.x, transform.position.y, -1);
        //    Vector3 _posB = new Vector3(transform.position.x + 10, transform.position.y, -1);

        //    lineRenderer.SetPosition(0, _posA);
        //    lineRenderer.SetPosition(1, _posB);
        //}
    }
}
