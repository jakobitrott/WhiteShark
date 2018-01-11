using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System;

using Vuforia;

using System.Threading;

using ZXing;
using ZXing.QrCode;
using ZXing.Common;

public class QrScript : MonoBehaviour {


    private bool cameraInitialized;
    

    private BarcodeReader barCodeReader;
    public MainText mainTextScript;

    public QrScript()
    {
    }

    void Start()
    {
   
        barCodeReader = new BarcodeReader();
        
        mainTextScript.results.text = "Waiting for target...";

    }

    public void StartQR()
    {
        StartCoroutine(InitializeCamera());
    }
    public void StopQR()
    {
        cameraInitialized = false;
    }

    private IEnumerator InitializeCamera()
    {
        // Waiting a little seem to avoid the Vuforia's crashes.
        yield return new WaitForSeconds(0.2f);

        var isFrameFormatSet = CameraDevice.Instance.SetFrameFormat(Image.PIXEL_FORMAT.GRAYSCALE, true);
        
        // Force autofocus.
        var isAutoFocus = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        if (!isAutoFocus)
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL);
        }
        
        cameraInitialized = true;
    }

    private void Update()
    {
        
        if (cameraInitialized)
        {
            try
            {
                var cameraFeed = CameraDevice.Instance.GetCameraImage(Image.PIXEL_FORMAT.GRAYSCALE);
                if (cameraFeed == null)
                {
                    return;
                }
                var data = barCodeReader.Decode(cameraFeed.Pixels, cameraFeed.BufferWidth, cameraFeed.BufferHeight, RGBLuminanceSource.BitmapFormat.Gray8);
                if (data != null)
                {
                    // QRCode detected.
                    mainTextScript.results.text = data.Text;

                }
                else
                {
                    //QR not detected
                    mainTextScript.results.text = "No QR detected.";

                }
            }
            catch (Exception e)
            {
               // Debug.LogError(e.Message);
            }
        }
    }
}