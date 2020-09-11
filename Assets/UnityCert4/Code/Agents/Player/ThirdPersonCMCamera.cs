using UnityEngine;
using System.Collections;

namespace Assets.UnityCert4.Code.Agents.Player
{
    public class ThirdPersonCMCamera : MonoBehaviour
    {
        public CharacterController controller;
        public Transform cam;

        public float moveSpeed = 6f;
        public float gravity = -9.81f;
        public float jumpHeight;
        public Vector3 playerVelocity;

        public float turnSmoothTime = 0.1f;
        float turnSmoothVelocity;

        void Update()
        {
            Jump();
            Move1();
        }

        void Move1()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            Vector3 moveDir = Vector3.zero;
            if (direction.magnitude >= 0.1f)
            {
                //Rotation
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(
                    transform.eulerAngles.y, 
                    targetAngle, 
                    ref turnSmoothVelocity, 
                    turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                //Movement
                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward ;

                controller.Move(moveDir * moveSpeed * Time.deltaTime);
            }
        }

        void Jump()
        {
            if (controller.isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            if (Input.GetButtonDown("Jump") && controller.isGrounded)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3f * gravity);
            }

            playerVelocity.y += gravity * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }

        void OnGUI()
        {
            //GUI.Label(new Rect(20, 20, 500, 20), "player rot y" + transform.eulerAngles.y);
            //GUI.Label(new Rect(20, 40, 500, 20), "camera rot y" + cam.eulerAngles.y);

            //GUI.Label(new Rect(20, 80, 500, 20), "back atan angle" + Mathf.Atan2(0, -1) * Mathf.Rad2Deg);

            //GUI.Label(new Rect(20, 120, 500, 20), "Input.GetButtonDown(Jump)" + Input.GetButtonDown("Jump"));
            //GUI.Label(new Rect(20, 140, 500, 20), "isgrounded" + controller.isGrounded);
        }
    }
}


/*
 void Update()
        {
            Move1();
            //CalculateDirection();
        }

        void Move1()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            Vector3 moveDir = Vector3.zero;
            if (direction.magnitude >= 0.1f)
            {
                //Rotation
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                //Movement
                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * moveSpeed;
            }
            
            //Jump
            if (Input.GetButtonDown("Jump") && controller.isGrounded)
            {
                moveDir.y += Mathf.Sqrt(jumpHeight * -3f * gravity);
            }

            //Gravity
            moveDir.y += gravity * Time.deltaTime;
            controller.Move(moveDir * Time.deltaTime);
        }
 */


//void CalculateDirection()
//{
//    horizontal = Input.GetAxisRaw("Horizontal");
//    vertical = Input.GetAxisRaw("Vertical");
//    Vector3 direction = new Vector3(horizontal, 0f, vertical);

//    if (direction.magnitude >= 0.1f)
//    {
//        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

//        Rotation(direction, targetAngle);
//        Move(targetAngle);
//    }
//}

//void Rotation(Vector3 direction, float targetAngle)
//{
//    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
//    transform.rotation = Quaternion.Euler(0f, angle, 0f);
//}

//void Move(float targetAngle)
//{
//    //targetAngle = vertical >= 0 ? targetAngle : targetAngle;
//    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
//    controller.Move(moveDir * speed * Time.deltaTime);
//}
