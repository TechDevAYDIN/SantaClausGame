using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHand : MonoBehaviour
{
    [SerializeField] GameObject FollowedTarget;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(FollowedTarget.transform.position);
        rb.MoveRotation(FollowedTarget.transform.rotation);
    }
}
