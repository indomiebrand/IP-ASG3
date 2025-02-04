using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlayAreaSpawner : MonoBehaviour
{
    public GameObject playAreaPrefab; //reference to the play area prefab to spawn
    private ARPlaneManager arPlaneManager; //reference to the AR plane manager
    private bool isScanningComplete = false; //track if scanning is done

    void Start()
    {
        arPlaneManager = FindObjectOfType<ARPlaneManager>(); //get ARPlaneManager in the scene
    }

    void Update()
    {
        //check if scanning is complete (if at least one plane is detected)
        if (arPlaneManager.trackables.count > 0 && !isScanningComplete)
        {
            isScanningComplete = true;
            OnScanningComplete(); //call method to spawn play area
        }
    }

    //method that is called when scanning is complete
    private void OnScanningComplete()
    {
        //get the first detected AR plane
        ARPlane firstPlane = GetFirstPlane();
        if (firstPlane != null)
        {
            //instantiate the play area at the plane's position
            GameObject spawnedPlayArea = Instantiate(playAreaPrefab, firstPlane.transform.position, Quaternion.identity);

            //reset its local position to align perfectly with the detected plane
            spawnedPlayArea.transform.position = firstPlane.transform.position;
        }
    }

    //helper method to get the first detected AR plane
    private ARPlane GetFirstPlane()
    {
        foreach (ARPlane plane in arPlaneManager.trackables)
        {
            if (plane.trackingState == TrackingState.Tracking)
            {
                return plane; //return the first tracked plane
            }
        }
        return null;
    }
}



