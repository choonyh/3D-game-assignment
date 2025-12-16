using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    public Animator anim;
    float turn = 0.0f;
    float turnVelocity = 0.0f;

    public bool isTutorial = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
    
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
}
