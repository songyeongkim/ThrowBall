using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

[RequireComponent(typeof(ARPlaneManager))]
public class PlaneSettingManager : MonoBehaviour
{
    private ARPlaneManager planeManager;
    private PlaceOnPlane placeOnPlane;

    private void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
        placeOnPlane = GetComponent<PlaceOnPlane>();
    }

    public void ResetGoalPos()
    {
        SetAllPlanesActive(true);
        placeOnPlane.visualObject.SetActive(true);
        Destroy(placeOnPlane.spawnedObject);
        GameManager.Instance.DeleteBall();
        GameManager.Instance.arPlacing = true;
    }

    public void SetAllPlanesActive(bool value)
    {
        planeManager.enabled = value;

        foreach(var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(value);
            Debug.Log(plane.gameObject.activeSelf);
        }
    }

}
