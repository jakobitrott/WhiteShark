using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VuforiaTargetFound : MonoBehaviour, ITrackableEventHandler
{
   
        private TrackableBehaviour mTrackableBehaviour;
        public QrScript qrScript;
        private bool foundBefore = false;

        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED)
            // ||
            //newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED
            {
            if (!foundBefore)
            {
                // Start QR Scanning when target is found
                qrScript.StartQR();
                foundBefore = true;
            }
            else
            {
                
                qrScript.mainTextScript.results.text = "Found before";
            }
            }
            else
            {
            // Stop QR Scanning
            qrScript.StopQR();
                
            }
        }
    
}
