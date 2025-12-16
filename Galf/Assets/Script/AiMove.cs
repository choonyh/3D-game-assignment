using UnityEditor.Rendering.LookDev;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AiMove : MonoBehaviour
{

    public Transform player;
    public float detectionRadius = 5f;
    public float attackRadius = 1.0f;
    public float moveSpeed = 3f;
    public float rotationSpeed = 5.0f;
    public float attackCD = 1.0f;
    public int damage = 10;

    public Animator anim;
    Rigidbody rb;
    private float lastAttackedTime = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = Vector3.Distance(rb.position, player.position);
        Vector3 lookPos = player.position - rb.position;
        lookPos.y = 0f;
        
        if (dist <= detectionRadius)
        {
            Vector3 dir =(player.position - rb.position).normalized;
            rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);

            if (lookPos != Vector3.zero)
            {
                Quaternion targetRot = Quaternion.LookRotation(lookPos);
                rb.MoveRotation(Quaternion.Slerp(
                    rb.rotation,
                    targetRot,
                    rotationSpeed * Time.fixedDeltaTime
                ));
            }

            if (dist <= attackRadius)
            {
                if(Time.time -lastAttackedTime >= attackCD)
                {
                    attackPlayer();
                    lastAttackedTime = Time.time;
                }
                //Get Animation -> play animation and -- player HP--


            }
        }

    }

    public void attackPlayer()
    {
        anim.SetTrigger("Lumbering");
        PlayerProperties playerProperties = player.GetComponent<PlayerProperties>();
        if(playerProperties.currentHP != 0)
        {
            playerProperties.TakeDamage(damage);
        }
    }
}

