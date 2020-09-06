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
        
    //Reference
    GameManager gm;
    Transform cameraTransform;
    CharacterController controller;

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
        cameraTransform = GetComponentInChildren<Camera>().transform;
        controller = GetComponent<CharacterController>();

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
        gm.GameData.playerLevel     = level;
        gm.GameData.playerHealth    = health;
        gm.GameData.playerPosition  = transform.position;
    }

    void Load ()
    {
        Debug.Log("Player loads data");
        level = gm.GameData.playerLevel;
        health              = gm.GameData.playerHealth;
        transform.position  = gm.GameData.playerPosition;
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