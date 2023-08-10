using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour {

	public Transform player;
	public GameObject teleObject;
	public Transform reciever;
	public GameObject recievingPortalFrame;

	private bool objectIsOverlapping = false;

    private void Start()
    {
		teleObject = player.gameObject;
	}

    // Update is called once per frame
    void Update () {

	
	}

	void OnTriggerEnter (Collider other)
	{
		/*
		if (other.gameObject.GetComponent<Teleportable>() != null)
		{
			teleObject = other.gameObject;
			Vector3 portalToObject = (teleObject.transform.position - transform.position).normalized;
			float dotProduct = Vector3.Dot(transform.up, portalToObject);

			// If this is true: The object has moved across the portal
			if (dotProduct < 0f)
			{
				// Teleport!

				float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
				Debug.Log("rotDiff: " + rotationDiff);
				rotationDiff += 180;
				//rotationDiff += transform.rotation.y - reciever.rotation.y;
				teleObject.transform.Rotate(Vector3.up, rotationDiff);

				Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToObject;
				//Vector3 positionOffset = reciever.up * portalToObject.magnitude;
				teleObject.transform.position = reciever.position + positionOffset;

				objectIsOverlapping = false;
			}
		}
		*/
		if (other.gameObject.GetComponent<Teleportable>() != null)
        {
			if(other.gameObject.tag == "Player")
            {
				teleObject = player.gameObject;
            }
			else
            {
				teleObject = other.gameObject;
			}
			Vector3 portalToObject = (teleObject.transform.position - transform.position).normalized;
			Vector3 positionOffset = reciever.up * portalToObject.magnitude;
			teleObject.transform.position = reciever.position + positionOffset;
			float rotationDiff = Quaternion.Angle(recievingPortalFrame.transform.rotation, player.rotation);
			rotationDiff += 180;
			teleObject.transform.Rotate(Vector3.up, rotationDiff);


		}

	}

	void OnTriggerExit (Collider other)
	{
		
		if(teleObject != null) 
		{
			teleObject = null;
        }
		
		objectIsOverlapping = false;
		
		
	}
}
