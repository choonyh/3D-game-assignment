using System.Collections;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Lumberjack : MonoBehaviour
{   

    [Header("Search Player/Trees")]
    public float detectPlayerRadius = 5f;
    public float detectTreeRadius = 20f;
    public Transform player;
    public LayerMask treeLayer;

    [Header("Move Towards and Rotation")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 5.0f;

    [Header("Attack Related")]
    public float attackCD = 1.1f;
    public float attackRadius = 2.0f;
    public float choppingRadius = 3.0f;
    private float lastAttackedTime = 0f;
    public int damage = 10;
    public int chopDamage = 1;
    public int chopCD = 1;
    private Tree target;
    private Coroutine routine;

    [Header("Lumberjack Related")]
    public Animator anim;
    Rigidbody rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        FindTree();
        if (player == null)
        {
            Debug.Log("No player");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null && Vector3.Distance(player.position, rb.position) <= detectPlayerRadius)
        {
            
            MoveTo(player.position);
        }
        else if (target != null)
        {
            float dist = Vector3.Distance(rb.position, target.transform.position);
            if (dist > choppingRadius)
            {
                MoveTo(target.transform.position);
            }
        }
        //if (dist <= detectPlayerRadius)
        //{
        //    Vector3 lookPos = player.position - rb.position;
        //    lookPos.y = 0f;

        //    Vector3 dir = (player.position - rb.position).normalized;
        //    anim.SetBool("Walk", true);
        //    rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);

        //    if (lookPos != Vector3.zero)
        //    {
        //        Quaternion targetRot = Quaternion.LookRotation(lookPos);
        //        rb.MoveRotation(Quaternion.Slerp(
        //            rb.rotation,
        //            targetRot,
        //            rotationSpeed * Time.fixedDeltaTime
        //        ));
        //    }
        //}    

    }
    private void Update()
    {
        Debug.Log($"Player pos: {player.position} | Lumberjack pos: {rb.position}");
        float playerDist = Vector3.Distance(rb.position, player.position);

        if (playerDist <= detectPlayerRadius)
        {Debug.Log($"Player distance: {playerDist}");
            PlayerLogic(playerDist);

        }
        else
        {
            TreeLogic();
        }
        if(playerDist > detectPlayerRadius && target == null)
        {
            anim.SetBool("Walk", false);
        }
    }

    public void PlayerLogic(float dist)
    {
        if (target != null)
        {
            target.ReleaseTree();
            target = null;
        }
        anim.SetBool("Walk", true);

        if (dist <= attackRadius && Time.time - lastAttackedTime >= attackCD)
        {
            anim.SetBool("Walk", false);
            attackPlayer();
            lastAttackedTime = Time.time;
        }
        if(routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
    }

    public void TreeLogic()
    {
        if (target == null)
        {
            FindTree();
            return;
        }

        float distance = Vector3.Distance(rb.position, target.transform.position);

        if(distance <= choppingRadius)
        {
            anim.SetBool("Walk", false);
            if (routine == null)
            {
                routine = StartCoroutine(ChopTree());
            }
            
        }
        else
        {
            anim.SetBool("Walk", true);
        }
        if (target == null && routine != null) { StopCoroutine(routine); routine = null; }

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

    public void MoveTo(Vector3 pos)
    {
        Vector3 direction = pos - rb.position;
        direction.y = 0f;
        if (direction.sqrMagnitude < 0.01f) return;
        direction.Normalize();

        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

        Quaternion targetRot = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Slerp(
                    rb.rotation,
                    targetRot,
                    rotationSpeed * Time.fixedDeltaTime));
    }

    void FindTree()
    {
        Collider[] hits = Physics.OverlapSphere(rb.position, detectTreeRadius, treeLayer);

        float closestDistance = Mathf.Infinity;
        Tree bestTree = null;

        foreach (Collider hit in hits)
        {
            Tree tree = hit.GetComponent<Tree>();
            if (tree == null) continue;

            if (!tree.TryReserve(this)) continue;

            float dist = Vector3.Distance(rb.position, tree.transform.position);
            if (dist < closestDistance)
            {
                if (bestTree != null)
                    bestTree.ReleaseTree();

                closestDistance = dist;
                bestTree = tree;
            }
            else
            {
                tree.ReleaseTree();
            }
        }

        target = bestTree;
    }

    IEnumerator ChopTree()
    {

        while (target != null)
        {
            yield return new WaitForSeconds(chopCD);

            if (target == null) break;
            anim.SetTrigger("Lumbering");
            target.TakeDamage(chopDamage);

            if (target.currentHealth <= 0f)
            {
                target = null;
                routine = null;

                FindTree();
                yield break;
            }
        }

    }

    private void OnDisable()
    {
        if (target != null)
            target.ReleaseTree();
    }
}

