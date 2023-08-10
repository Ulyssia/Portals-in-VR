using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Jump : MonoBehaviour
{
    public Vector3 jumpVec;
    public float jumpForce = 2.0f;
    public bool isGrounded;
    public GameObject player;
    private Rigidbody rb;
    public InputActionProperty jumpB;
    public InputActionProperty jumpY;
    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;
        jumpB.action.performed += JumpAction;
        jumpY.action.performed += JumpAction;
        rb = player.gameObject.GetComponent<Rigidbody>();
        jumpVec = new Vector3(0.0f, 2.5f, 0.0f);
    }
    private void OnDestroy()
    {
        jumpB.action.performed -= JumpAction;
        jumpY.action.performed -= JumpAction;
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void JumpAction(InputAction.CallbackContext context)
    {
        Debug.Log("JumpKeyPressed");
        if(isGrounded)
        {
            Debug.Log("Jump");
            rb.AddForce(jumpVec * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
}