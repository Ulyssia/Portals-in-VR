using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportable : MonoBehaviour
{
    private GameObject currentTeleObject;
    // Start is called before the first frame update
    void Start()
    {
        currentTeleObject = null;
    }

    public void SetCurrentTeleObject(GameObject teleObject)
    {
        currentTeleObject = teleObject;
    }

    public GameObject GetCurrentTeleObject()
    {
        return currentTeleObject;
    }

}
