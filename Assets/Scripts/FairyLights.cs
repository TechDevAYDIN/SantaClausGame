using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FairyLights : MonoBehaviour
{
    [SerializeField]
    public List<Material> materials;
    public Color NCol()
    {
        return Random.ColorHSV();
    }
}
