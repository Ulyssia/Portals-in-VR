using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorldOnController : MonoBehaviour
{
    public GameObject attachParent;
    public InputActionProperty toggle; //primary buttons
    public GameObject worldManager;

    private GameObject cloneWorld;
    private bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        isOn = false;

        toggle.action.performed += ToggleOn;
        //toggle.action.canceled += ToggleOff;
    }

    private void OnDestroy()
    {
        toggle.action.performed -= ToggleOn;
        //toggle.action.canceled -= ToggleOff;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ToggleOn(InputAction.CallbackContext context)
    {
        isOn = !isOn;

        if (isOn == false)
        {
            worldManager.GetComponent<ControlWorld>().enabled = false;
            worldManager.GetComponent<ControlWorld>().cloneParent = null;

            cloneWorld = GameObject.Find("World(Clone)");
            if (cloneWorld)
            {
                Destroy(cloneWorld);
            }
            
        }
        else if(isOn == true)
        {
            worldManager.GetComponent<ControlWorld>().enabled = true;
            worldManager.GetComponent<ControlWorld>().cloneParent = attachParent.transform;

        }
    }

    private void ToggleOff(InputAction.CallbackContext context)
    {
        if (isOn)
        {
            isOn = !isOn;
        }
    }
}
