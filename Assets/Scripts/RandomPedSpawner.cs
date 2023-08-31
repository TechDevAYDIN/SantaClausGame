using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPedSpawner : MonoBehaviour
{
    [SerializeField] GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(crate());
    }
    public IEnumerator crate()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(.1f,1f));
            if (GameManager.singleton.canSpawnMan)
            {
                Instantiate(character, transform.position, Quaternion.identity);
                break;
            }
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
