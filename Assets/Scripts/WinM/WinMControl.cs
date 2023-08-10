using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMControl : MonoBehaviour
{
    public Transform parentController;
    public Transform World;
    public Transform HitBox;
    
    private Vector3 lastPos;
    Vector2 distance;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = parentController.position;
        lastPos = gameObject.transform.position;
    }
    
    // Update is called once per frame
    void Update()
    {
        distance = new Vector2(parentController.position.x - HitBox.position.x, parentController.position.z - HitBox.position.z);

        if (distance.magnitude > 0.3f)
        {
            gameObject.transform.position = parentController.position;
            lastPos = gameObject.transform.position;
        }
        else if (distance.magnitude <= 0.3f)
        {
            gameObject.transform.position = new Vector3(lastPos.x, parentController.position.y, lastPos.z);
        }
        gameObject.transform.rotation = World.rotation;
    }
}