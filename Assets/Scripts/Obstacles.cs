using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public enum ObstaclesType
{
    Buildings,
    StreetLights,
    HangedLights,
    Snowman,
    Tree,
    Ground,
    other
}
public enum TreeColliderType
{
    Main,
    Leaves
}
[System.Serializable]
public class PositionType
{
    public PosSettings
    RandomX,
    RandomY,
    RandomZ;
}
[System.Serializable]
public class PosSettings
{
    public bool Enabled;
    [Range(-100,100)]
    public float MinValue;
    [Range(-100, 100)]
    public float MaxValue;
}
public class Obstacles : MonoBehaviour
{
    [SerializeField] protected ObstaclesType obstaclesType;
    //[SerializeField]public PositionType positions = new PositionType();
    public PosSettings
    RandomX,
    RandomY,
    RandomZ,
    RandomScale;
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] Rigidbody[] ActivateRigidbodies;
    [SerializeField] Collider[] ActivateColliders;
    [SerializeField] GameObject[] OpenObjects;
    [SerializeField] GameObject[] CloseObjects;
    [SerializeField] GameObject EventObject;
    [SerializeField] UnityEvent preSetEvents;
    Animator anim;
    bool triggerCoolDown = false;
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
        if (obstaclesType.Equals(ObstaclesType.HangedLights))
        {
            if(Random.Range(0,4) <= 1)
            {
                gameObject.SetActive(false);
            }
        }
        if (obstaclesType.Equals(ObstaclesType.Tree))
        {
            anim = GetComponentInParent<Animator>();
            transform.localEulerAngles = new Vector3(0, Random.Range(0, 360), 0);
        }
        if (obstaclesType.Equals(ObstaclesType.Snowman))
        {
            transform.localEulerAngles = new Vector3(0, Random.Range(0, 360), 0);
        }
        if (obstaclesType.Equals(ObstaclesType.Ground))
        {
            //GetComponent<NavMeshSurface>().BuildNavMesh();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7 && GameManager.singleton.isAlive)
        {
            if (obstaclesType.Equals(ObstaclesType.StreetLights) || obstaclesType.Equals(ObstaclesType.HangedLights))
            {
                CameraShaker.Instance.ShakeCam(2f, .15f);
                obstaclesType = ObstaclesType.other;
                foreach (Rigidbody rb in ActivateRigidbodies)
                {
                    rb.isKinematic = false;
                    rb.AddForceAtPosition(Vector3.forward * 1000, collision.GetContact(0).point);                    
                }
                InvokePre();
                if (EventObject != null)
                    if (EventObject.activeSelf) 
                        preSetEvents.Invoke();       
            }
            if (obstaclesType.Equals(ObstaclesType.Snowman))
            {
                InvokePre();
                CameraShaker.Instance.ShakeCam(1.5f, .1f);
                if (hitParticles != null)
                {
                    hitParticles.transform.position = collision.GetContact(0).point;
                    hitParticles.gameObject.SetActive(true);
                }
            }
            if (obstaclesType.Equals(ObstaclesType.Buildings) || obstaclesType.Equals(ObstaclesType.Ground))
            {
                GameManager.singleton.Death();
            }
            if (obstaclesType.Equals(ObstaclesType.Tree))
            {
                GameManager.singleton.Death();
            }
            //GameManager.singleton.Death();


        }
    }
    public void InvokePre()
    {
        GetComponent<Collider>().enabled = false;
        if (hitParticles != null)
            hitParticles.gameObject.SetActive(true);
        GameManager.singleton.FearTrigger();
        foreach (Rigidbody rb in ActivateRigidbodies)
        {
            rb.isKinematic = false;
        }
        foreach (Collider coll in ActivateColliders)
        {
            coll.enabled = true;
        }
        foreach (GameObject obj in OpenObjects)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in CloseObjects)
        {
            obj.SetActive(false);
        }
        StartCoroutine(CloseThis());
    }
    IEnumerator CloseThis()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (obstaclesType.Equals(ObstaclesType.Tree))
        {
            if (other.gameObject.layer == 7 || other.gameObject.layer == 6)
            {
                if (!triggerCoolDown)
                {
                    anim.SetTrigger("Crash");
                    triggerCoolDown = true;
                    AudioManager.singleton.PlayPropSound();
                    Invoke("ResetTriggerCoolDown", .25f);
                }
            }
        }
    }
    void ResetTriggerCoolDown()
    {
        triggerCoolDown = false;
    }
}
