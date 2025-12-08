using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour
{
    private Rigidbody objectRigid;
    private Transform objectGrabPointTransform;
    private void Awake()
    {
        objectRigid = GetComponent<Rigidbody>();
    }
    public void Grab(Transform objectGrabPoint)
    {
        this.objectGrabPointTransform = objectGrabPoint;
        
        objectRigid.useGravity = false;
        objectRigid.isKinematic = true;

    }

    public void Drop()
    {
        objectRigid.isKinematic = false;
        this.objectGrabPointTransform = null;
        objectRigid.useGravity = true;

    }

    private void FixedUpdate()
    {
        float lerpSpeed = 15f;
        if (objectGrabPointTransform != null)
        {
            Vector3 targetPosition = objectGrabPointTransform.position;

            objectRigid.MovePosition(Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed));
        }
    }
}
