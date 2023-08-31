using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyLghtSphere : MonoBehaviour
{
    public List<Material> materials;
    [SerializeField] ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<Renderer>().material = materials[Random.Range(0,materials.Count)];
        ParticleSystem.MainModule mod = particle.main;
        mod.startColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
