using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Listens for touch events and performs an AR raycast from the screen touch point.
/// AR raycasts will only hit detected trackables like feature points and planes.
///
/// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
/// and moved to the hit position.
/// </summary>
[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;

    UnityEvent placementUpdate;

    public GameObject visualObject;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }

    private PlaneSettingManager planeSettingManager;

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        planeSettingManager = GetComponent<PlaneSettingManager>();

        if (placementUpdate == null)
            placementUpdate = new UnityEvent();

        placementUpdate.AddListener(DiableVisual);
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        if(Input.GetMouseButtonDown(0) && Application.isEditor)
        {
            touchPosition = visualObject.transform.position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if(GameManager.Instance.arPlacing && !Application.isEditor)
        {
            if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                // Raycast hits are sorted by distance, so the first one
                // will be the closest hit.
                var hitPose = s_Hits[0].pose;

                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                    GameManager.Instance.SetBall();
                    planeSettingManager.SetAllPlanesActive(false);
                    GameManager.Instance.arPlacing = false;
                }
                placementUpdate.Invoke();
            }
        }

        if (spawnedObject == null && Application.isEditor && Input.GetMouseButtonDown(0))
        {
            Debug.Log("mobile");
            spawnedObject = Instantiate(m_PlacedPrefab, visualObject.transform.position, m_PlacedPrefab.transform.rotation);
            GameManager.Instance.SetBall();
            placementUpdate.Invoke();
        }
    }

    public void ResetSpawnedObj()
    {
        if(spawnedObject != null)
        {
            Destroy(spawnedObject);
            spawnedObject = null;
        }
    }

    public void DiableVisual()
    {
        visualObject.SetActive(false);
    }

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    ARRaycastManager m_RaycastManager;
}