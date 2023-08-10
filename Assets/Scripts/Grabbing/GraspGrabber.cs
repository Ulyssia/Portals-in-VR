using UnityEngine;
using UnityEngine.InputSystem;

public class GraspGrabber : Grabber
{
    public InputActionProperty grabAction;

    Grabbable currentObject;
    Grabbable grabbedObject;
    Material lineRendererMaterial;

    Vector3 velocity;
    Vector3 previousPosition;
    
    public GameObject MainCam;
    public GameObject controllerL;
    public GameObject controllerR;

    //All lines with just a // to the side of them are from Professor Rosenberg's implementation for rotation
    public Transform controller1; //
    public Transform controller2; //

    private Quaternion lastSpindleRotation; //

    private Transform grabInitParent;


    // Start is called before the first frame update
    void Start()
    {
        grabbedObject = null;
        currentObject = null;

        grabAction.action.performed += Grab;
        grabAction.action.canceled += Release;

        velocity = Vector3.zero;
        previousPosition = this.transform.position;

        // initialize the lastSpindleRotation
        lastSpindleRotation = Quaternion.LookRotation(controller1.position - controller2.position); //

    }

    private void OnDestroy()
    {
        grabAction.action.performed -= Grab;
        grabAction.action.canceled -= Release;


    }

    // Update is called once per frame
    void Update()
    {
        
        //For conserving velocity in thrown objects
        Vector3 newVelocity = (this.transform.position - previousPosition) / Time.deltaTime;
        velocity = 0.25f * velocity + 0.75f * newVelocity;
        previousPosition = this.transform.position;
    
    }

    public override void Grab(InputAction.CallbackContext context)
    {
        if (currentObject && grabbedObject == null)
        {
            if (currentObject.GetCurrentGrabber() != null)
            {
                currentObject.GetCurrentGrabber().Release(new InputAction.CallbackContext());
            }

            grabbedObject = currentObject;
            grabbedObject.SetCurrentGrabber(this);

            if (grabbedObject.GetComponent<Rigidbody>())
            {
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                grabbedObject.GetComponent<Rigidbody>().useGravity = false;
            }
            ///
            grabInitParent = grabbedObject.transform.parent;
            grabbedObject.transform.parent = this.transform;
        }
    }

    public override void Release(InputAction.CallbackContext context)
    {
        if (grabbedObject)
        {
            if (grabbedObject.GetComponent<Rigidbody>())
            {
                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                grabbedObject.GetComponent<Rigidbody>().useGravity = true;
                grabbedObject.GetComponent<Rigidbody>().velocity = velocity;
            }

            grabbedObject.SetCurrentGrabber(null);
            //grabbedObject.transform.parent = null;
            grabbedObject.transform.parent = grabInitParent;
            grabbedObject = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (currentObject == null && other.GetComponent<Grabbable>())
        {
            currentObject = other.gameObject.GetComponent<Grabbable>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (currentObject)
        {
            if (other.GetComponent<Grabbable>() && currentObject.GetInstanceID() == other.GetComponent<Grabbable>().GetInstanceID())
            {
                currentObject = null;
            }
        }
    }
}
