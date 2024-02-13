using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float mouseSensivity = 1f;
    [SerializeField] private float cameraVerticalRotation = 0f;
    private Vector2 input;

    public static FirstPersonCamera instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        mouseSensivity = PlayerPrefs.GetFloat("Sensivity", 1f);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerMovement.instance.GetIsPaused())
        {
            cameraVerticalRotation -= input.y;
            cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90, 90);
            transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

            player.Rotate(Vector3.up * input.x);
        }
    }
    public RaycastHit AimCenter()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, ~PlayerMovement.instance.gameObject.layer))
        {
            return hit;
        }
        else
        {
            return hit;
        }
    }

    public void mousePosition(InputAction.CallbackContext context)
    {
        if(context.control.device == Gamepad.current)
        {
            input = context.ReadValue<Vector2>() * (mouseSensivity * 2);
        }
        else
        {
            input = context.ReadValue<Vector2>() * (mouseSensivity / 10);
        }
    }
}
