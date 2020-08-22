using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class My3DCharController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 100f;
    public Transform cameraTransform;

    Vector3 velocity;
    float lookX;
    float lookY;

    void Start()
    {
        Vector3 cameraRotation = cameraTransform.rotation.eulerAngles;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MouseLook();
        Move();
    }


    void MouseLook ()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        lookY -= mouseY;
        lookY = Mathf.Clamp(lookY, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(lookY, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void OnGUI ()
    {
        GUI.Label(new Rect(20, 20, 900, 20), "cameraTransform.rotation " + cameraTransform.rotation);
    }

    void Move ()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x) + (transform.forward * z);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(move * speed * Time.deltaTime);
    }
}
