using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;


public class Manipulation : MonoBehaviour
{
    float zoomSpeed = 10f;
    private bool isZooming = false;
    private float initialDistance = 0f;
    private float currentDollyDistance = 0f;
    private OVRCameraRig ovrCameraRig;

    void Start()
    {
        ovrCameraRig = GetComponentInChildren<OVRCameraRig>();
        currentDollyDistance = ovrCameraRig.transform.localPosition.z;
    }

    void Update()
    {

        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) && OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            if (!isZooming)
            {
                initialDistance = Vector3.Distance(OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch),
                                                    OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch));
                isZooming = true;
            }
            else
            {
                float currentDistance = Vector3.Distance(OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch),
                                                        OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch));

                float distanceChange = currentDistance - initialDistance;
                distanceChange *= zoomSpeed;

                Vector3 moveDirection = transform.forward * distanceChange;

                transform.position += moveDirection;
                initialDistance = currentDistance;
            }
        }

        else if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            Vector3 moveDirection = new Vector3(OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x, 0f, OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y);
            transform.position += moveDirection * 5.0f * Time.deltaTime;

            Vector3 rotateDirection = new Vector3(OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y * -1.0f, OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x, 0f);
            transform.Rotate(rotateDirection * 10.0f * Time.deltaTime);
        }

        else
        {
            isZooming = false;
        }
    }
}
