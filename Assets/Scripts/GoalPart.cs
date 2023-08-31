using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPart : Goal
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6 || collision.gameObject.layer == 7)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 100 + Vector3.forward * 300);
            base.PlayerTouchThePart();
        }
    }
    public void Blast()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-60, 60), Random.Range(10, 60), Random.Range(0, 150)), ForceMode.Impulse);
        GetComponent<Rigidbody>().AddTorque(Vector3.up * Random.Range(50, 300) + Vector3.right * Random.Range(50, 300), ForceMode.Impulse);
    }
}
