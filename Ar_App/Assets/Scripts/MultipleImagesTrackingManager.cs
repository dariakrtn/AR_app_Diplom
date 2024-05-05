using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultipleImagesTrackingManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabsToSpawn;

    private ARTrackedImageManager _arTrackedImageManager;

    private Dictionary<string, GameObject> _arObjects;

    [SerializeField] private GameObject Camera;

    [SerializeField] private GameObject Stop;


    private void Awake()
    {
        _arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        _arObjects = new Dictionary<string, GameObject>();
    }

    private void Start()
    {
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImageChanged;

        foreach (GameObject prefabs in prefabsToSpawn) 
        {
            GameObject newAROgject = Instantiate(prefabs, Vector3.zero, Quaternion.identity);
            newAROgject.name= prefabs.name;
            newAROgject.gameObject.SetActive(false);
            _arObjects.Add(newAROgject.name, newAROgject);
        }
    }


    private void OnDestroy()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;
    }

    private void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {

        foreach (ARTrackedImage trackedImage in eventArgs.added) 
        {
            string id = trackedImage.name;
            UpdateTrackedImage(trackedImage, id);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            string id = trackedImage.name;
            UpdateTrackedImage(trackedImage, id);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(false);
        }
    }

    private void UpdateTrackedImage(ARTrackedImage trackedImage, string id)
    {
        
            if (trackedImage.trackingState is  TrackingState.None) { //TrackingState.Limited or
                _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(false);
               
            }

            if (prefabsToSpawn !=null)
            {
                Stop.SetActive(true);
                _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(true);
                _arObjects[trackedImage.referenceImage.name].transform.position = trackedImage.transform.position;
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(false);
                }
            }

        }
        
        /*
                        _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(true);
                        _arObjects[trackedImage.referenceImage.name].transform.position = trackedImage.transform.position;*/

    }


}
