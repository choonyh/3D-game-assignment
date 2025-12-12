using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class PlayerCollision : MonoBehaviour
{
    private bool doorIsOpen = false;
    private float doorTimer = 0.0f;
    private GameObject currentDoor;
    public float doorOpenTime = 3.0f;
    public AudioClip doorOpenSound;
    public AudioClip doorShutSound;
    

    void Door(AudioClip aClip, bool openCheck, string animName, GameObject thisDoor)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = aClip;
        audio.Play();
        doorIsOpen = openCheck;
        thisDoor.transform.GetComponent<Animation>().Play(animName);
    }

    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5.0f))
        {
            if (hit.collider.gameObject.tag == "Door" && doorIsOpen == false)
            {
                currentDoor = hit.collider.gameObject;
                Door(doorOpenSound, true, "doorOpen", currentDoor);
            }
        }
        if (doorIsOpen)
        {
            doorTimer += Time.deltaTime;

            if (doorTimer > doorOpenTime)
            {
                Door(doorShutSound, false, "doorShut", currentDoor);
                doorTimer = 0.0f;
            }
        }
    }
}
