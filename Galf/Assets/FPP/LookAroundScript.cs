using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LookAroundScript : MonoBehaviour
{
    public float sensitivityX;
    public float sensitivityY;
    public Transform orientation;
    private float xRotation=0f;
    private float yRotation=0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SettingManager.instance.LoadSettings();
        sensitivityX = Mathf.RoundToInt(SettingManager.instance.cameraSensitivity);
        sensitivityY = Mathf.RoundToInt(SettingManager.instance.cameraSensitivity);
    }

    // Update is called once per frame
    void Update()
    {
        
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);

    }
}
