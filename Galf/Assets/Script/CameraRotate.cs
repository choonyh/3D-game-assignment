using System;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private float rotationAngle = 45;
    [SerializeField] private float speed = 5f;
    private float rightMost;
     private float leftMost;

    private float direction = 1f;

    private float targetAngle;
    private float currentAngle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetAngle = transform.rotation.eulerAngles.y;
        currentAngle = targetAngle;
        rightMost = targetAngle + rotationAngle;
        leftMost = targetAngle - rotationAngle;
        
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle += speed * direction * Time.deltaTime;

        if (currentAngle > rightMost)
        {
            direction = -1;

        }else if(currentAngle < leftMost)
        {
            direction = 1;
        }

        transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, currentAngle, transform.rotation.eulerAngles.z);
    }
}
