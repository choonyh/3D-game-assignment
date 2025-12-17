using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    public Animator anim;
    float turn = 0.0f;
    float turnVelocity = 0.0f;
    float horizontalInput;
    float verticalInput;

    public bool isTutorial = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        MyInput();
        PlayAnimation();
        float mouseX = Input.GetAxis("Mouse X");
        float turnTarget = 0.0f;

        if (mouseX < -0.1f)
            turnTarget = -1.0f;
        else if (mouseX > 0.1f)
            turnTarget = 1.0f;
        else
            turnTarget = 0.0f;

        float smoothTime = 0.01f;
        turn = Mathf.SmoothDamp(turn, turnTarget, ref turnVelocity, smoothTime);

        anim.SetFloat("Turn", turn);
    }
    private void PlayAnimation()
    {

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
        }
        else
        {
            anim.SetBool("Idle", false);

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButton(1))
            {
                anim.SetBool("Run", true);
                anim.SetBool("Walk", false);

            }
            else
            {
                anim.SetBool("Walk", true);
                anim.SetBool("Run", false);

            }
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
}
