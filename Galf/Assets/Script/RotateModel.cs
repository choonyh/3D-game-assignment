using UnityEngine;

public class RotateModel : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = player.position - new Vector3(transform.position.x , player.position.y,transform.position.z);
        orientation.forward = dir.normalized;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * z + orientation.right * x;

        if (inputDir != Vector3.zero) {
            playerObj.forward = Vector3.Slerp(playerObj.forward, dir, rotationSpeed);
        }
    }
}
