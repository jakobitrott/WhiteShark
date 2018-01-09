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
        StartCoroutine(InitializeCamera());
        mainTextScript.results.text = "Ready to scan";

    }

    private IEnumerator InitializeCamera()
    {
        // Waiting a little seem to avoid the Vuforia's crashes.
        yield return new WaitForSeconds(2f);

        var isFrameFormatSet = CameraDevice.Instance.SetFrameFormat(Image.PIXEL_FORMAT.GRAYSCALE, true);
        Debug.Log(String.Format("FormatSet : {0}", isFrameFormatSet));

        // Force autofocus.
        var isAutoFocus = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        if (!isAutoFocus)
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL);
        }
        Debug.Log(String.Format("AutoFocus : {0}", isAutoFocus));
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
                   
                    
                }
            }
            catch (Exception e)
            {
               // Debug.LogError(e.Message);
            }
        }
    }
}