using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] protected ObstaclesType obstaclesType;
    public PosSettings
    RandomX,
    RandomY,
    RandomZ,
    RandomScale;
    // Start is called before the first frame update
    void Start()
    {
        if (RandomX.Enabled)
        {
            transform.localPosition = new Vector3(Random.Range(RandomX.MinValue, RandomX.MaxValue), transform.localPosition.y, transform.localPosition.z);
        }
        if (RandomY.Enabled)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, Random.Range(RandomY.MinValue, RandomY.MaxValue), transform.localPosition.z);
        }
        if (RandomZ.Enabled)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Random.Range(RandomZ.MinValue, RandomZ.MaxValue));
        }
        if (RandomScale.Enabled)
        {
            float lScale = Random.Range(RandomScale.MinValue, RandomScale.MaxValue);
            transform.localScale = Vector3.one * lScale;
        }
    }

}
