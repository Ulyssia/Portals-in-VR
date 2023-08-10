using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour {

	public Transform playerCameraOffset;
	public Transform mainCam;
	public Transform portal;
	public Transform otherPortal;
	
	// Update is called once per frame
	void LateUpdate () {
	
		Vector3 playerOffsetFromPortal = playerCameraOffset.position - otherPortal.position;
		transform.position = portal.position + playerOffsetFromPortal;

		float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);

		Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
		Vector3 newCameraDirection = portalRotationalDifference * mainCam.forward;
		transform.rotation = Quaternion.LookRotation(-newCameraDirection, Vector3.up);
		
	}


}
