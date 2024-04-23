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
    [SerializeField] private GameObject Finder;


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
        Finder.gameObject.SetActive(true);
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
            Finder.gameObject.SetActive(true);
        }
    }

    private void UpdateTrackedImage(ARTrackedImage trackedImage, string id)
    {
        if (id == "Human" && id =="Robot" ){ 
            if (trackedImage.trackingState is  TrackingState.None) { //TrackingState.Limited or
                _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(false);
                Finder.gameObject.SetActive(true);
            }

            if (prefabsToSpawn !=null)
            {
                Finder.gameObject.SetActive(false);
                _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(true);
                _arObjects[trackedImage.referenceImage.name].transform.position = trackedImage.transform.position;

            }
        }
        else
        {
            if (trackedImage.trackingState is TrackingState.None)
            { 
                Finder.gameObject.SetActive(true);
            }

            if (prefabsToSpawn != null)
            {
                Finder.gameObject.SetActive(false);
                GetComponent<Transform>().transform.position = _arObjects[trackedImage.referenceImage.name].transform.position;

               
/*
                _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(true);
                _arObjects[trackedImage.referenceImage.name].transform.position = trackedImage.transform.position;*/

            }

        }


    }
}
