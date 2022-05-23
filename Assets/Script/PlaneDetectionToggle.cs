using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

[RequireComponent(typeof(ARPlaneManager))]
public class PlaneDetectionToggle : MonoBehaviour
{
    private ARPlaneManager planeManager;
    private ARPointCloudManager pointManager;
    [SerializeField]
    private TextMeshProUGUI toggleButtonText;

    private void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
        pointManager = GetComponent<ARPointCloudManager>();
        toggleButtonText.text = "Disabled";
    }

    public void TogglePlaneDetection()
    {
        planeManager.enabled = !planeManager.enabled;
        string toggleButtonMessage = "";

        if(planeManager.enabled)
        {
            toggleButtonMessage = "Disabled";
            SetAllPlanesActive(true);
        }
        else
        {
            toggleButtonMessage = "Enabled";
            SetAllPlanesActive(false);
        }

        toggleButtonText.text = toggleButtonMessage;
    }

    private void SetAllPlanesActive(bool value)
    {
        foreach(var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(value);
        }
    }

}
