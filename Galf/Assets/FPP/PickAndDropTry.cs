using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAndDropTry : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform objectGrabPoint;
    [SerializeField] private LayerMask pickUpLayerMask;


    private Grabable grabable;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!grabable)
            {
                float pickupDistance = 2f;

                if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, pickupDistance))
                {
                    //Debug.Log(hit.transform);
                    if (hit.transform.TryGetComponent(out grabable))
                    {
                        //Debug.Log(grabable);
                        grabable.Grab(objectGrabPoint);
                    }
                }
            }
            else
            {
                grabable.Drop();
                grabable = null;

            }
        }
    }
}
