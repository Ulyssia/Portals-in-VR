using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalProjection : MonoBehaviour
{
    public LineRenderer laserPointer;
    
    public InputActionProperty projectPortal;
    public InputActionProperty moveProjection;
    private Vector2 projReel;
    public float projReelVel = 20.0f;
    public float projReelDeadZone = 0.25f;

    public GameObject blueProjection;
    public GameObject orangeProjection;

    public GameObject bluePortal;
    public GameObject orangePortal;

    public GameObject orangePortalControl;
    public GameObject bluePortalControl;

    public GameObject projectionPoint;

    private Vector3 projPointEuler;
    public Transform player;

    public GameObject locomotion;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        blueProjection.SetActive(false);
        orangeProjection.SetActive(false); 

        projectPortal.action.performed += ProjectPortal;
        projectPortal.action.canceled += SpawnPortal;

    }
    private void OnDestroy()
    {
        projectPortal.action.performed -= ProjectPortal;
        projectPortal.action.canceled -= SpawnPortal;
    }

    // Update is called once per frame
    void Update()
    {
        projectionPoint.transform.rotation = player.transform.rotation;
        projPointEuler = projectionPoint.transform.rotation.eulerAngles;
        projPointEuler.x = 0;
        projPointEuler.z = 0;
    }

    //Creates a projection copy of a portal while holding down the trigger
    public void ProjectPortal(InputAction.CallbackContext context)
    {
        locomotion.SetActive(false);
        //RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            projectionPoint.transform.localPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z - (hit.point.z / 3));


            //If blue portal powerup has been obtained then activate
            if (this.gameObject.tag == "right" && bluePortalControl.gameObject.activeSelf == true)
            {
                blueProjection.SetActive(true);
                blueProjection.transform.parent = projectionPoint.transform;
                blueProjection.transform.position = projectionPoint.transform.localPosition;
                blueProjection.transform.rotation = Quaternion.Euler(projPointEuler);
                
            }
            //If orange portal powerup has been obtained then activate
            else if (this.gameObject.tag == "left" && orangePortalControl.gameObject.activeSelf == true)
            {
                orangeProjection.SetActive(true);
                orangeProjection.transform.parent = projectionPoint.transform;
                orangeProjection.transform.position = projectionPoint.transform.localPosition;
                orangeProjection.transform.rotation = Quaternion.Euler(projPointEuler);
            }


            moveProjection.action.performed += MoveProjection;
        }
    }

    //'Spawns' a portal at the projection location
    public void SpawnPortal(InputAction.CallbackContext context)
    {
        locomotion.SetActive(true);
        
        if (this.gameObject.tag == "right" && bluePortalControl.gameObject.activeSelf == true)
        {
            bluePortal.transform.position = blueProjection.transform.position;
            bluePortal.transform.rotation = Quaternion.Euler(blueProjection.transform.rotation.eulerAngles);
         

            blueProjection.SetActive(false);
        }
        else if (this.gameObject.tag == "left" && orangePortalControl.gameObject.activeSelf == true)
        {
            
            orangePortal.transform.position = orangeProjection.transform.position;
            orangePortal.transform.rotation = Quaternion.Euler(orangeProjection.transform.rotation.eulerAngles);

            orangeProjection.SetActive(false);
            
        }
        
        blueProjection.transform.parent = null;
        orangeProjection.transform.parent = null;
        moveProjection.action.performed -= MoveProjection;
    }

    //Allows the player to move the portal projection forward and back
    
    public void MoveProjection(InputAction.CallbackContext context)
    {
        projReel = context.action.ReadValue<Vector2>();

        if (projReel.y >= projReelDeadZone || projReel.y <= -projReelDeadZone)
        {
            float reelVel = projReelVel * projReel.y * Time.deltaTime;
            //Vector3 reelVec = projectionPoint.transform.position - this.transform.position;\
            Vector3 reelVec = hit.point - this.transform.position;
            reelVec.Normalize();
            
            projectionPoint.transform.Translate(reelVec * reelVel, Space.World);
        }
    }
    
    
}
