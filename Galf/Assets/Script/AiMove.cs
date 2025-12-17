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
    public float attackCD = 1.1f;
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
        
        if (dist <= detectionRadius)
        {
            Vector3 lookPos = player.position - rb.position;
            lookPos.y = 0f;

            Vector3 dir =(player.position - rb.position).normalized;
            anim.SetBool("Walk",true);
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

            
        }

    }
    private void Update()
    {
        float dist = Vector3.Distance(rb.position, player.position);

        if (dist <= attackRadius && dist <= detectionRadius)
        {
            if (Time.time - lastAttackedTime >= attackCD)
            {
                anim.SetBool("Walk",false);
                attackPlayer();
                lastAttackedTime = Time.time;
            }
            //Get Animation -> play animation and -- player HP--


        }
    }
    public void attackPlayer()
    {
        PlayerProperties playerProperties = player.GetComponent<PlayerProperties>();
        if (playerProperties.currentHP > 0)
        {
            anim.SetTrigger("Lumbering");
            playerProperties.TakeDamage(damage);
        }
    }
}

