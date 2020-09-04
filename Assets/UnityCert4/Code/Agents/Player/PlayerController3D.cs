using UnityEngine;
using System.Collections;
using System.Security.Policy;

public class PlayerController3D : MonoBehaviour
{
    public static PlayerController3D instance;

    [Header("Stats")]
    [SerializeField] int level = 3;
    [SerializeField] int health = 100;

    [Header("Settings")]
    [SerializeField] float speed = 12f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float mouseSensitivity = 100f;

    [Header("References")]
    [SerializeField] Transform cameraTransform;
    [SerializeField] CharacterController controller;

    //Class reference
    GameManager gm;

    //Status
    Vector3 velocity;
    float lookX;
    float lookY;

    #region MonoBehavior
    void Awake()
    {
        instance = this;
        Subscribe();
    }

    void Start()
    {
        //Reference
        gm = GameManager.Instance;

        //Initialization
        Vector3 cameraRotation = cameraTransform.rotation.eulerAngles;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MouseLook();
        Move();
    }
    #endregion

    #region Control
    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        lookY -= mouseY;
        lookY = Mathf.Clamp(lookY, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(lookY, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x) + (transform.forward * z);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(move * speed * Time.deltaTime);
    }
    #endregion

    #region Save & Load
    void Save ()
    {
        gm.Data.playerLevel     = level;
        gm.Data.playerHealth    = health;
        gm.Data.playerPosition  = transform.position;
    }

    void Load ()
    {
        level               = gm.Data.playerLevel;
        health              = gm.Data.playerHealth;
        transform.position  = gm.Data.playerPosition;
    }
    #endregion

    #region Event subscription
    void Subscribe ()
    {
        SceneEvents.GameSave.Event += Save;
        SceneEvents.GameLoad.Event += Load;
    }

    private void OnDisable()
    {
        SceneEvents.GameSave.Event -= Save;
        SceneEvents.GameLoad.Event += Load;
    }
    #endregion
}