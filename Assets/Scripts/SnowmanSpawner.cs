using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanSpawner : MonoBehaviour
{
    public enum PropType
    {
        Tree,
        Christmas
    }
    [SerializeField] PropType proType;
    public List<GameObject> propsTree;
    public List<GameObject> propsChristmas;

    [SerializeField] int xRand, zRand;
    // Start is called before the first frame update
    void Start()
    {
        if (proType == PropType.Tree)
        {
            Instantiate(propsTree[Random.Range(0, propsTree.Count)], transform.position, Quaternion.identity);
        }
        if (proType == PropType.Christmas)
        {
            transform.localPosition += new Vector3(Random.Range(-xRand,xRand+1),0,Random.Range(-zRand,zRand+1));
            Instantiate(propsChristmas[Random.Range(0, propsChristmas.Count)], transform.position, Quaternion.identity);
        }
    }
}
