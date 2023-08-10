using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlWorld : MonoBehaviour
{
    public GameObject world;
    public Transform cloneParent;
    public float scaleFactor = 0.01f;

    private GameObject myClone;
    private GameObject Odd;
    
    void Start()
    {
        myClone = Duplicate(world);
    }

    void Update()
    {
        if(GameObject.Find("World(Clone)") == null)
        {
            myClone = Duplicate(world);
        }
           
        //Detect if new objects have been added to the scene. Duplicate that
        /*
        if(GameObject.Find("World(Clone)"))
        {
            Odd = FindOdd(world ,myClone).gameObject;

        }*/
    }

    /*
    private Transform FindOdd(GameObject Obj, GameObject clone)
    {
        Transform odd = null;
        return odd;
    }*/

    //Duplicate objects under World node
    private GameObject Duplicate(GameObject Obj)
    {
        GameObject clone = Instantiate(Obj, cloneParent);
        clone.transform.localPosition = new Vector3(-0.1f, -0.1f, 0.1f);
        clone.transform.localScale = Vector3.one * scaleFactor;

        //remove ceilings
        DeactivateObj(clone, "Ceiling");

        //disable grabbable on normal objects(optional)
        //DisableComponent(clone);

        //set mimic for all objects that can be manipulated(from Original world to WinM)
        //and 2 way mimicking for portals
        AttachMimic(clone);

        return clone;
    }

    private void DeactivateObj(GameObject Obj, string name)
    {
        bool recursive = true;
        foreach (Transform trans in Obj.transform)
        {
            if (trans.gameObject.name == name)
            {
                trans.gameObject.SetActive(false);
            }
            else if (trans.childCount == 0)
            {
                recursive = false;
            }
            if (recursive)
            {
                DeactivateObj(trans.gameObject, name);
            }
            recursive = true;
        }
    }

    private void AttachMimic(GameObject Obj)
    {
        bool recursive = true;
        /*
        if (Obj.GetComponent<Grabbable>() != null)
        {
            Obj.AddComponent<Mimic>(); //reference to mimicing transformation
        }*/
        //Obj.AddComponent<Mimic>();

        foreach (Transform trans in Obj.transform)
        {
            if (trans.childCount == 0)
            {
                recursive = false;
            }

            //trans.gameObject.AddComponent<Mimic>();
            
            if (trans.gameObject.GetComponent<Grabbable>() != null)
            {
                trans.gameObject.AddComponent<Mimic>(); //reference to mimicing transformation
            }

            if (recursive)
            {
                AttachMimic(trans.gameObject);
            }
            recursive = true;
        }
    }

    private void DisableComponent(GameObject Obj)
    {
        bool recursive = true;
        if(Obj.transform.tag != "Portal" && Obj.GetComponent<Grabbable>())
        {
            Obj.GetComponent<Grabbable>().enabled = false;
        }

        foreach (Transform trans in Obj.transform)
        {
            if (trans.childCount == 0)
            {
                recursive = false;
            }
            if (trans.gameObject.GetComponent<Grabbable>() != null)
            {
                if(trans.tag != "Portal")
                {
                    trans.gameObject.GetComponent<Grabbable>().enabled = false;
                    continue;
                }
            }
            
            if (recursive)
            {
                DisableComponent(trans.gameObject);
            }
            recursive = true;
        }
    } 

}
