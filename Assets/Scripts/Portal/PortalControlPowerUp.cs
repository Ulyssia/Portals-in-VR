using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalControlPowerUp : MonoBehaviour
{

    public GameObject player;
    public GameObject orangeControlSwitch;
    public GameObject blueControlSwitch;
    // Start is called before the first frame update
    void Start()
    {
        orangeControlSwitch.SetActive(false);
        blueControlSwitch.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(this.gameObject.tag == "OSwitch")
            {
                orangeControlSwitch.SetActive(true);
            }
            else if(this.gameObject.tag == "BSwitch")
            {
                blueControlSwitch.SetActive(true);
            }

            this.gameObject.SetActive(false);

        }
        
        
    }
}
