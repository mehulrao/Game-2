using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float gravity = -9.8f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private Vector3 vSpeed;
    public float jumpSpeed = 6f;
    public float jumpHeight = 3f;
    Vector3 moveDir;


    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle - 90, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        if(controller.isGrounded && vSpeed.y < 0){
            vSpeed.y = -2f;
        }
        if(controller.isGrounded){
            if(Input.GetButtonDown("Jump")) {
                    vSpeed.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        Debug.Log(controller.isGrounded);
        vSpeed.y += gravity * Time.deltaTime;
        controller.Move(vSpeed * Time.deltaTime);
    }

}
