using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _eyes;
    [SerializeField] private float _sensitivity;
    [Range (-90.0f, 0.0f)]
    [SerializeField] private float _camLimitMin;
    [Range (0.0f, 90.0f)]
    [SerializeField] private float _camLimitMax;

    private float _camAngle = 0.0f;

    void Update()
    {
        RotateEyes();
        RotateBody();
    }

    private void RotateEyes()
    {
        float yMouse = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;
        _camAngle -= yMouse;
        _camAngle = Mathf.Clamp (_camAngle, _camLimitMin, _camLimitMax);
        _eyes.localRotation = Quaternion.Euler(_camAngle, 0, 0);
    }

    private void RotateBody()
    {
        float xMouse = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * xMouse);
    }
}
