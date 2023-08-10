using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleThree : MonoBehaviour
{
    public GameObject player;
    public GameObject greenOrb;
    public GameObject key;
    private bool keepAway;
    public float keyMaxVel;
    public GameObject greenLight;

    private Vector3 keyStartPos;
    // Start is called before the first frame update
    void Start()
    {
        keyMaxVel = 10.0f;
        greenLight.SetActive(false);
        greenOrb.SetActive(false);
        keepAway = false;
        keyStartPos = key.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(keepAway && key != null)
        {
            greenLight.SetActive(true);
            greenOrb.SetActive(true);
            Vector3 keyToPlayer = key.transform.position - player.transform.position;
            Vector3 keyToStart = key.transform.position - keyStartPos;
            float keyToPlayerDist = keyToPlayer.magnitude;
            if (keyToPlayerDist <= 7.0f)
            {
                key.transform.position += keyToPlayer.normalized * keyMaxVel * Time.deltaTime;
               // key.transform.position.y = keyStartPos.y;
            }
            else if(keyToPlayerDist > 7.0f && keyToStart.magnitude <= 12.0f)
            {
                key.transform.position -= keyToStart.normalized * keyMaxVel * Time.deltaTime;
            }
            else
            {
                key.transform.position = keyStartPos;
            }

        }
        else
        {
            greenLight.SetActive(false);
            greenOrb.SetActive(false);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Test");
        if(collision.gameObject.tag == "Rig")
        {
            Debug.Log("KeepAway Active");
            keepAway = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Rig")
        {
            keepAway = false;
        }
    }
}
