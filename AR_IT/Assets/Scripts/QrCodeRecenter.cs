using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using ZXing;

public class QrCodeRecenter : MonoBehaviour
{
    [SerializeField]
    private ARSession session;
    [SerializeField]
    private ARSessionOrigin sessionOrigin;
    [SerializeField]
    private ARCameraManager cameraManager;
    [SerializeField]
    private List<Target> navigationTargetObjects = new List<Target>();

    private Texture2D cameraImageTexture;
    private IBarcodeReader reader = new BarcodeReader();

/*    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetQrCodeRecenterTarget("Room");
        }
    }
    
    private void OnEnable()
        {
            cameraManager.frameReceived += OnCameraFrameReceived;
        }*/
}
