using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorManager : MonoBehaviour
{
    [SerializeField] private GameObject Indicator;
    [SerializeField] private GameObject Camera;

    private void Update()
    {
        Indicator.transform.position = Camera.transform.position;
        Indicator.transform.rotation = Camera.transform.rotation;
    }
}
