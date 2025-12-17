using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class GolfShoot : MonoBehaviour
{
    public AnimationStateController controller;
    public AudioClip shootSound;
    public GameObject golfObject;
    public float shootForce = 30.0f;
    public float attackCD = 0.5f;

    private float lastAttackedTime = 0f;

    // Update is called once per frame

    void Start()
    {

    }
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (Time.time - lastAttackedTime >= attackCD)
            {
                AudioSource audio = GetComponent<AudioSource>();
                controller.anim.SetTrigger("Shoot");
                audio.clip = shootSound;
                audio.Play();
                GameObject temp = Instantiate(golfObject, transform.position, transform.rotation);
                temp.name = "Golf";
                Rigidbody rb = temp.GetComponent<Rigidbody>();
                rb.linearVelocity = transform.TransformDirection(new Vector3(0, 0, shootForce));
                if (temp.GetComponent<Rigidbody>() == null)
                {
                    Debug.Log("Component Missing");
                    temp.AddComponent<Rigidbody>();
                }
                Physics.IgnoreCollision(transform.root.GetComponent<Collider>(), temp.GetComponent<Collider>(), true);
                lastAttackedTime = Time.time;
            }
        }
    }
}
