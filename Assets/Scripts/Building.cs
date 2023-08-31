using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Transform chimney;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.SetVector("_Tiling", new Vector2(Random.Range(75, 101) * .01f, 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7)
        {
            GameManager.singleton.Death();
        }
    }
}
