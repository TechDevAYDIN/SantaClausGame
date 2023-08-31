using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public PosSettings
    RandomX,
    RandomY,
    RandomZ;

    public Transform connectedChimney;
    [SerializeField] ParticleSystem _particles1;
    [SerializeField] ParticleSystem _particles2;
    [SerializeField] ParticleSystem _particles3;


    [SerializeField] ParticleSystem particlesAfterCollect;
    [SerializeField] ParticleSystem particlesAfterCollect2;



    [SerializeField] BoxCollider mainCollider;
    // Start is called before the first frame update
    void Start()
    {
        connectedChimney.GetComponentInParent<chimney>().isActive = true;
        GameManager.singleton.AddRing(this);
        if (RandomX.Enabled)
        {
            if(transform.parent.localPosition.x < 0)
                transform.localPosition = new Vector3(Random.Range(-RandomX.MinValue, -RandomX.MaxValue), transform.localPosition.y, transform.localPosition.z);
            else
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
        _particles1.Play();
        _particles2.Play();
        _particles3.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            GameManager.singleton.CollectRing(connectedChimney);
            transform.parent = Camera.main.transform;
            mainCollider.enabled = false;
            _particles1.gameObject.SetActive(false);
            _particles2.gameObject.SetActive(false);
            _particles3.gameObject.SetActive(false);
            _particles1.Stop();
            _particles2.Stop();
            _particles3.Stop();

            particlesAfterCollect.Play();
            particlesAfterCollect2.Play();
            GetComponent<MeshRenderer>().enabled = false;            
        }               
    }
}
