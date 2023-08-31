using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chimney : MonoBehaviour
{
    public bool isActive;
    public Color32 startColor;
    public Color32 endColor;
    [Range(0, 10)]
    public float speed;
    [SerializeField] GameObject Particles;
    MeshRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        if (!isActive)
            renderer.material.color = endColor;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
            renderer.material.color = Color.Lerp(startColor,endColor, Mathf.PingPong(Time.time * speed, 1));
    }
    public void Collected()
    {
        isActive = false;
        renderer.material.color = endColor;
        Particles.SetActive(true);
    }
}
