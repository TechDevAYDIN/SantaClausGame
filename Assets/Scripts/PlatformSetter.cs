using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSetter : MonoBehaviour
{
    public List<Ring> rings;

    [SerializeField] int OpenRings;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CloseSome());
    }

    IEnumerator CloseSome()
    {
        yield return new WaitForSeconds(.05f);
        for (int i = 0; i < OpenRings; i++)
        {
            rings[i].gameObject.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
