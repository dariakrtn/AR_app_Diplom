using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class IndicatorManager : MonoBehaviour
{
    [SerializeField] private GameObject Indicator;


    private void Update()
    {
        Indicator.transform.position = GetComponent<Transform>().position;
        Indicator.transform.rotation = GetComponent<Transform>().rotation;
    }
}
