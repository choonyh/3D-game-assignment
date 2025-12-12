using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Animator anim;
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isBottom;

    // Update is called once per frame
    void Update()
    {
        isBottom = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isBottom && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * speed * Time.deltaTime);

        bool isMoving = (x != 0 || z != 0);

        if (!isMoving)
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Walk", false);
            anim.SetBool("Run",false);
            speed = 5;
        }
        else
        {
            anim.SetBool("Idle", false);

            if (Input.GetKey(KeyCode.LeftShift)|| Input.GetMouseButton(1))
            {
                speed = 20;
                anim.SetBool("Run",true);
                anim.SetBool("Walk", false);
                
            }
            else
            {
                speed = 5;
                anim.SetBool("Walk", true);
                anim.SetBool("Run", false);
                
            }
        }

        if (Input.GetButton("Jump") && isBottom)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }
}
