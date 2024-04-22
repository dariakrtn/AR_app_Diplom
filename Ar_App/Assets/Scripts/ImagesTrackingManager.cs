using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using static UnityEngine.GraphicsBuffer;

public class ImagesTrackingManager : MonoBehaviour
{
    [SerializeField] private GameObject[] navigationTargetObjects;
    [SerializeField] private GameObject Indicator;
    [SerializeField] private GameObject Camera;
    [SerializeField] private TMP_Dropdown navigationTargetDropDown;


/*    private NavMeshPath path; // current calculated path
    private LineRenderer line; // line renderer to display path
    private Vector3 targetPosition = Vector3.zero;

    private bool lineToggle = false;*/


    private ARTrackedImageManager _arTrackedImageManager;

    private Dictionary<string, GameObject> _arObjects;

    private void Awake()
    {
        _arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        _arObjects = new Dictionary<string, GameObject>();
        Debug.Log("void Awske");
    }

    private void Start()
    {
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImageChanged;

        foreach (GameObject prefabs in navigationTargetObjects)
        {
            GameObject newAROgject = Instantiate(prefabs, prefabs.transform.position, prefabs.transform.rotation);
            newAROgject.name = prefabs.name;
            newAROgject.gameObject.SetActive(false);
            _arObjects.Add(newAROgject.name, newAROgject);
            Debug.Log("void Start foreach");
        }
    }
    private void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {

        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateTrackedImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateTrackedImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImageChanged;
    }


    private void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState is TrackingState.None)
        {
            _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(false);
        }

        if (navigationTargetObjects != null)
        {
            _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(true);
            _arObjects[trackedImage.referenceImage.name].transform.position = trackedImage.transform.position;

            Instantiate(Camera, _arObjects[trackedImage.referenceImage.name].transform.position, _arObjects[trackedImage.referenceImage.name].transform.rotation);
            Debug.Log("INSTANTITE 22222222222");

        }
    }
}
    
