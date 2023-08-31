using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapeFollow : MonoBehaviour
{
    [SerializeField]
    Transform followPos;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - followPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = followPos.position + offset ;
    }
}
