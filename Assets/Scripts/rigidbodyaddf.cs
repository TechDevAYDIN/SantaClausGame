using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigidbodyaddf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.forward * 10);
        }
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.forward * -10);
        }
    }
}
