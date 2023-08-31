using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBuildingSpawner : MonoBehaviour
{
    public enum BuildingType
    {
        Obstacle,
        Collectible
    }
    [SerializeField] BuildingType buildingType;
    public List<GameObject> buildingsNeutral;
    public List<GameObject> buildingsCollectible;

    [SerializeField]Ring collectibleRing;
    
    // Start is called before the first frame update
    void Start()
    {
        if(buildingType == BuildingType.Collectible)
        {
            Instantiate(buildingsCollectible[Random.Range(0, buildingsCollectible.Count)], transform);
            collectibleRing.connectedChimney = GetComponentInChildren<Building>().chimney;
            GetComponentInParent<PlatformSetter>().rings.Add(collectibleRing);
        }
        if (buildingType == BuildingType.Obstacle)
        {
            Instantiate(buildingsNeutral[Random.Range(0, buildingsNeutral.Count)], transform);
            //GetComponentInChildren<Ring>().gameObject.SetActive(false);
            //GetComponentInParent<PlatformSetter>().rings.Add(GetComponentInChildren<Ring>());
        }
    }
}
