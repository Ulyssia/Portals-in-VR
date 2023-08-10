using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChamberController : MonoBehaviour
{
    public GameObject Chamber;
    public GameObject ChambController;

    private float rotLock = 90.0f;
    private Vector3 targetRot = Vector3.one;
    private Vector3 posLock = Vector3.one;
    private float chamberControllerX = 0.0f;
    private float chamberX = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        posLock = ChambController.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Chamb_Controller.transform.rotation = Quaternion.Euler(Chamb_Controller.transform.rotation.eulerAngles.x, rotLock, rotLock);
        /*
        targetRot = Chamber.transform.rotation.eulerAngles + ChambController.transform.rotation.eulerAngles;
        targetRot -= new Vector3(targetRot.x % 90, targetRot.y % 90, targetRot.z % 90);
        ChambController.transform.rotation = Quaternion.Euler(targetRot);
        */
        /*
        chamberControllerX = ChambController.transform.rotation.x;
        chamberX = Chamber.transform.rotation.x;
        if(chamberControllerX > 0 && chamberControllerX <= 90)
        {
            chamberX = 90;
        }
        else if(chamberControllerX > 0 && chamberControllerX > 90 && chamberControllerX <= 180) 
        {
            chamberX = 180;
        }
        if (chamberControllerX < 0 && chamberControllerX >= -90)
        {
            chamberX = -90;
        }
        else if (chamberControllerX < 0 && chamberControllerX < -90 && chamberControllerX >= -180)
        {
            chamberX = -180;
        }
        else
        {
            chamberX = 0;
        }
        if(chamberControllerX > 180 || chamberControllerX < -180)
        {
            chamberControllerX *= -1;
        }


        Chamber.transform.rotation = Quaternion.Euler(new Vector3(chamberX, 0.0f, 0.0f));
        */
        /*
        Vector3 chambR = ChambController.transform.rotation.eulerAngles;
        Debug.Log(chambR);
        float chambX = chambR.x;
        //chambR.x = Mathf.Floor(chambR.x / 30) * 30;
        chambR.y = 0;
        chambR.z = 0;
        Debug.Log(chambX + " " + chambR.x);
        Chamber.transform.rotation = Quaternion.Euler(chambR);
        */
        Chamber.transform.rotation = ChambController.transform.rotation;


        ChambController.transform.position = posLock;

    }
}
