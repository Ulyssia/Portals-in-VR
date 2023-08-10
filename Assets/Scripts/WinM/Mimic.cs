using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic : MonoBehaviour
{
    public GameObject cloneParent, oriParent;
    public Transform other;
    public float scaleFactor = 0.01f;

    Transform initParent;
    bool isHeld;
    Vector3 relativePos_other, relativePos_self;
    
    void Start()
    {
        isHeld = false;
        initParent = gameObject.transform.parent;
        string oriName;
        oriName = gameObject.transform.parent.name;

        //Set self reference, Find corresonding object in original world
        if (oriName == "World(Clone)")
        {
            oriParent = GameObject.Find("World");
            cloneParent = GameObject.Find("World(Clone)");
        }
        else if(oriName != "World(Clone)")
        {
            oriParent = GameObject.Find(oriName);
            cloneParent = FindChild(GameObject.Find("World(Clone)"), oriName).gameObject;
        }
        
        other = FindPair(oriParent);
    }

    // Update is called once per frame
    void Update() 
    {
        //other = FindPair(oriParent);

        if (gameObject.transform.parent != initParent)
        {
            isHeld = true;
        }
        else if(gameObject.transform.parent == initParent)
        {
            isHeld = false;
        }
        if(!isHeld)
        {
            relativePos_other = other.position - oriParent.transform.position;
            gameObject.transform.rotation = other.rotation;
            gameObject.transform.position = cloneParent.transform.position + relativePos_other * scaleFactor;
        }
        else if(isHeld)
        {
            relativePos_self = gameObject.transform.position - cloneParent.transform.position;
            other.rotation = gameObject.transform.rotation;
            other.position = oriParent.transform.position + relativePos_self / scaleFactor;
        }
    }

    private Transform FindPair(GameObject Obj)
    {
        Transform myres = null;
        bool recursive = false;

        foreach (Transform trans in Obj.transform)
        {
            if(gameObject.transform.parent.name == "World(Clone)" && trans.parent.name == "World")
            {
                if(trans.name == gameObject.transform.name)
                {
                    myres = trans;
                    return myres;
                }
            }
            if(trans.parent.name == gameObject.transform.parent.name && trans.name == gameObject.transform.name)
            {                
                myres = trans;
                return myres;
            }
            if (trans.childCount > 0)
            {
                recursive = true;
            }
            if (recursive)
            {
                myres = FindPair(trans.gameObject);
            }
            recursive = false;
        }       
        return myres;
    }

    private Transform FindChild(GameObject Obj, string name)
    {
        Transform child = null;
        bool recursive = false;

        foreach(Transform trans in Obj.transform)
        {
            if(trans.name == name)
            {
                return trans;
            }
            if(trans.childCount > 0)
            {
                recursive = true;
            }
            if(recursive)
            {
                child = FindChild(trans.gameObject, name);
            }
        }
        return child;
    }

}
