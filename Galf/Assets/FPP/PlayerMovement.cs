using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] public float speed = 10f;
    [SerializeField] public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Rigidbody rb;

    public float playerHeight;
    public LayerMask ground;
    public bool isGround;
    public float groundDrag;

    public Animator anim;

    Vector3 velocity;
    public bool isTutorial;

    private GameObject roket;
    
    //Move and then play animation
    private void Start()
    {
        roket = GameObject.Find("Rocket Launcher");
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
        MyInput();
        if (isGround)
        {
            rb.linearDamping = groundDrag;
        }
        else
            rb.linearDamping = 0;
        PlayAnimation();

    }
    private void FixedUpdate()
    {
        MovePlayer();
      
    }
    private void MyInput()
    { 
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        velocity = orientation.right * horizontalInput + orientation.forward * verticalInput;

        rb.AddForce(velocity.normalized * speed * 10f, ForceMode.Force);
    }

    private void PlayAnimation()
    {
        roket.SetActive(true);

        bool isMoving = (horizontalInput != 0 || verticalInput != 0);

        if (isTutorial)
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
            return;

        }
        if (!isMoving)
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
            speed = 5;
        }
        else
        {
            anim.SetBool("Idle", false);

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButton(1))
            {
                speed = 20;
                anim.SetBool("Run", true);
                roket.SetActive(false);
                anim.SetBool("Walk", false);

            }
            else
            {
                speed = 5;
                anim.SetBool("Walk", true);
                anim.SetBool("Run", false);

            }
        }
    }
}
