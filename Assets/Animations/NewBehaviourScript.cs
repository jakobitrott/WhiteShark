using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class NewBehaviourScript : MonoBehaviour, ITrackableEventHandler {

	private TrackableBehaviour mTrackableBehaviour;
	private Animator mAnimation;

	void Start()
	{
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		mAnimation = GetComponentInChildren<Animator> ();

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
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			mAnimation.SetTrigger ("whiteShark");
		}
		else
		{
			// Stop audio when target is lost
			mAnimation.SetTrigger("whiteShark");
		}
	}   
}
