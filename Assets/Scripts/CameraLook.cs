using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    private float _mouseSensitivity = 100f;

    [SerializeField]
    private Transform _playerBody;

    private float _XRotation = 0;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        LookAround();
    }

    private void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        _XRotation -= mouseY;
        _XRotation = Mathf.Clamp(_XRotation, -70, 55);

        transform.localRotation = Quaternion.Euler(_XRotation, 0, 0);
        _playerBody.Rotate(Vector3.up * mouseX);
    }
}
