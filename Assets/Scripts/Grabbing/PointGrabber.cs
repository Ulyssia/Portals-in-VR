using UnityEngine;
using UnityEngine.InputSystem;

public class PointGrabber : Grabber
{
    public LineRenderer laserPointer;
    public Material grabbablePointerMaterial;

    public InputActionProperty touchAction;
    public InputActionProperty grabAction;

    public InputActionProperty moveAction;
    private Vector2 reelAct;
    public float maxReelVel = 2.0f;
    public float reeldeadZone = 0.25f;

    Material lineRendererMaterial;
    Transform grabPoint;
    Grabbable grabbedObject;
    Transform initialParent;

    Vector3 velocity;
    Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        laserPointer.enabled = true;
        lineRendererMaterial = laserPointer.material;

        grabPoint = new GameObject().transform;
        grabPoint.name = "Grab Point";
        grabPoint.parent = this.transform;
        grabbedObject = null;
        initialParent = null;

        grabAction.action.performed += Grab;
        grabAction.action.canceled += Release;

        /*
        touchAction.action.performed += TouchDown;
        touchAction.action.canceled += TouchUp;
        */

        velocity = Vector3.zero;
        previousPosition = this.transform.position;
    }

    private void OnDestroy()
    {
        grabAction.action.performed -= Grab;
        grabAction.action.canceled -= Release;

        /*
        touchAction.action.performed -= TouchDown;
        touchAction.action.canceled -= TouchUp;
        */

       
    }

    // Update is called once per frame
    void Update()
    {
        if (laserPointer.enabled)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                laserPointer.SetPosition(1, new Vector3(0, 0, hit.distance));

                if (hit.collider.GetComponent<Grabbable>())
                {
                    laserPointer.material = grabbablePointerMaterial;
                }
                else
                {
                    laserPointer.material = lineRendererMaterial;
                }
            }
            else
            {
                laserPointer.SetPosition(1, new Vector3(0, 0, 100));
                laserPointer.material = lineRendererMaterial;
            }
        }
        //For conserving velocity in thrown objects
        Vector3 newVelocity = (grabPoint.transform.position - previousPosition) / Time.deltaTime;
        velocity = 0.25f * velocity + 0.75f * newVelocity;
        previousPosition = grabPoint.transform.position;

    }

    public override void Grab(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.collider.GetComponent<Grabbable>() && laserPointer.enabled)
            {
                grabPoint.localPosition = new Vector3(0, 0, hit.distance);

                if (hit.collider.GetComponent<Grabbable>().GetCurrentGrabber() != null)
                {
                    hit.collider.GetComponent<Grabbable>().GetCurrentGrabber().Release(new InputAction.CallbackContext());
                }

                grabbedObject = hit.collider.GetComponent<Grabbable>();
                grabbedObject.SetCurrentGrabber(this);

                if (grabbedObject.GetComponent<Rigidbody>())
                {
                    grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                    grabbedObject.GetComponent<Rigidbody>().useGravity = false;
                    grabbedObject.GetComponent<Rigidbody>().velocity = velocity * 0.5f;
                }

                initialParent = grabbedObject.transform.parent;
                grabbedObject.transform.parent = grabPoint;

                moveAction.action.performed += Move;

            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        reelAct = context.action.ReadValue<Vector2>();

        if (reelAct.y >= reeldeadZone || reelAct.y <= -reeldeadZone) {

            float reelVel = maxReelVel * reelAct.y * Time.deltaTime;
            Vector3 reelVec =  grabPoint.transform.position - this.transform.position;         
            
            reelVec.Normalize();
            grabPoint.transform.Translate(reelVec * reelVel, Space.World);  //Manipulates the grabPoint's global position using the y direction (up/down) from the control stick
           
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
            }

            grabbedObject.transform.parent = initialParent;
            grabbedObject = null;
            moveAction.action.performed -= Move;
        }
    }
    /*
    void TouchDown(InputAction.CallbackContext context)
    {
        laserPointer.enabled = true;
    }

    void TouchUp(InputAction.CallbackContext context)
    {
        laserPointer.enabled = false;
    }
    */
}
