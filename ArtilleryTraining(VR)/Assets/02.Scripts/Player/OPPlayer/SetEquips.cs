using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEquips : MonoBehaviour
{
    Camera headCam;
    public GameObject cameraRig; 

    void Start()
    {
        headCam = GetComponent<Camera>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BINOCULARS"))
        {
            other.GetComponentInChildren<Camera>().enabled = true;
            cameraRig.GetComponent<OVRCameraRig>().disableEyeAnchorCameras = true;
        }
        if (other.CompareTag("P96K"))
        {
            if (OPManager.instance_OPManager.idx == 2 || OPManager.instance_OPManager.idx == 5)
                OPManager.instance_OPManager.SetStep(-1);
        }
    }

    void OnTriggerExit(Collider other)
    {
        cameraRig.GetComponent<OVRCameraRig>().disableEyeAnchorCameras = false;
    }
}
