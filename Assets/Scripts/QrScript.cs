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

    void Start()
    {
        barCodeReader = new BarcodeReader();
        StartCoroutine(InitializeCamera());
    }

    private IEnumerator InitializeCamera()
    {
        // Waiting a little seem to avoid the Vuforia's crashes.
        yield return new WaitForSeconds(1.5f);

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
                var cameraFeed = CameraDevice.Instance.GetCameraImage(Image.PIXEL_FORMAT.RGB888);
                if (cameraFeed == null)
                {
                    return;
                }
                var data = barCodeReader.Decode(cameraFeed.Pixels, cameraFeed.BufferWidth, cameraFeed.BufferHeight, RGBLuminanceSource.BitmapFormat.RGB24);
                if (data != null)
                {
                    // QRCode detected.
                    Debug.Log(data.Text);
                }
                else
                {
                    Debug.Log("No QR code detected !");
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}