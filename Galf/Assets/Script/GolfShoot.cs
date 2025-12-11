using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class GolfShoot : MonoBehaviour
{
    public AudioClip shootSound;
    public GameObject golfObject;
    public float shootForce;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            AudioSource audio = GetComponent<AudioSource>();
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
        }
    }
}
