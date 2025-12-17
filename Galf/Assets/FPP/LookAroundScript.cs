using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LookAroundScript : MonoBehaviour
{
    public float sensitivity;
    public Transform playerBody;
    private float xRotation=0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SettingManager.instance.LoadSettings();
        sensitivity = Mathf.RoundToInt(SettingManager.instance.cameraSensitivity);
    }

    // Update is called once per frame
    void Update()
    {
        
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime * 2;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime * 2;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
