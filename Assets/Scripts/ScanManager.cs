using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ScanManager : MonoBehaviour
{
    public GameObject playAreaPrefab; 
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private GameObject spawnedPlayArea;

    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                if (spawnedPlayArea == null)
                {
                    spawnedPlayArea = Instantiate(playAreaPrefab, hitPose.position, hitPose.rotation);
                }
            }
        }
    }
}




