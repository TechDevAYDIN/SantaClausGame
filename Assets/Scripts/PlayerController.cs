using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isAlive, isHoldingGift;
    Vector3 startIdleRotation;
    [SerializeField] Transform target;
    MovementController mc;
    Rigidbody rb;
    [SerializeField]Animator anim;
    public List<Transform> childTransforms;
    public List<Rigidbody> DeathAddRigidbodies;
    [SerializeField] GameObject leftArm, rightArm, leftElbow, rightElbow;
    [SerializeField] Transform rightHand;
    [SerializeField] Animator[] deerAnimators;

    [SerializeField] GameObject HoldingGift;

    [SerializeField] Transform targetChimney;
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        isHoldingGift = false;
        mc = target.GetComponent<MovementController>();
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        startIdleRotation = transform.localEulerAngles;
        StartCoroutine(TriggerWave());
    }

    // Update is called once per frame
    void FixedUpdate()
    {/*
        if(isAlive)
            FollowTarget();*/
        //transform.position = new Vector3(0,0, target.position.z);
        if (!isHoldingGift && mc.isAlive)
        {
            anim.SetBool("TakeGift",true);
        }
        if (isHoldingGift)
        {
            anim.SetFloat("Blend", Mathf.Clamp((childTransforms[3].position.x) / 15, -1f, 1f));
            //anim.SetFloat("Blend", Mathf.Lerp(anim.GetFloat("Blend"), Mathf.Clamp((childTransforms[3].position.x) / 15, -1f, 1f), Time.deltaTime * 8));
        }
        for (int i = 0; i < childTransforms.Count; i++)
        {
            if(i == 0)
            {
                childTransforms[i].localPosition = Vector3.Lerp(childTransforms[i].localPosition, target.position - target.forward * 1f, Time.deltaTime * 15);
                //childTransforms[i].transform.rotation = Quaternion.Lerp(childTransforms[i].rotation, target.rotation, Time.deltaTime * 22);
                var targetRotation = Quaternion.LookRotation(target.localPosition - childTransforms[i].position);
                childTransforms[i].rotation = Quaternion.Slerp(childTransforms[i].rotation, targetRotation, Time.deltaTime * 25);
            }
            else
            {

                /*
                childTransforms[i].localPosition = new Vector3(Mathf.Lerp(childTransforms[i].localPosition.x, childTransforms[i-1].localPosition.x, Time.deltaTime * 20), 
                                                                Mathf.Lerp(childTransforms[i].localPosition.y, childTransforms[i - 1].localPosition.y, Time.deltaTime * 20), 
                                                                childTransforms[i].localPosition.z);*/
                childTransforms[i].localPosition = Vector3.Lerp(childTransforms[i].localPosition, childTransforms[i - 1].localPosition - childTransforms[i - 1].forward * 1.5f, Time.deltaTime * 20);
                //childTransforms[i].transform.rotation = Quaternion.Lerp(childTransforms[i].rotation, childTransforms[i - 1].rotation, Time.deltaTime * 10);
                //childTransforms[i].LookAt(childTransforms[i - 1]);
                var targetRotation = Quaternion.LookRotation(childTransforms[i - 1].localPosition - childTransforms[i].localPosition);
                childTransforms[i].rotation = Quaternion.Slerp(childTransforms[i].rotation, targetRotation, Time.deltaTime * 16);
            }
        }
        /*
        anim.SetFloat("Blend", Mathf.Lerp(anim.GetFloat("Blend"), Mathf.Clamp((target.position.x - childTransforms[2].position.x) / 2, -1f, 1f), Time.deltaTime * 8));
        //anim.SetFloat("Blend", Mathf.Lerp(anim.GetFloat("Blend"),Mathf.Clamp((target.position.x - transform.position.x) / 2, -1f, 1f), Time.deltaTime * 3));
        anim.SetFloat("BlendUp", Mathf.Lerp(anim.GetFloat("BlendUp"), Mathf.Clamp((target.position.y - childTransforms[2].position.y) / 2, -1f, 1f), Time.deltaTime * 8));*/
    }
    private void FollowTarget()
    {
        for(int i = 0; i < childTransforms.Count ; i++)
        {/*
            if(i == 0)
            {
                childTransforms[i].localPosition = Vector3.Lerp(transform.localPosition, target.localPosition - target.transform.forward, Time.deltaTime * 60);
                //childTransforms[i].transform.localRotation = target.localRotation;
            }
            else
            {
                /*childTransforms[i].localPosition = new Vector3(Mathf.Lerp(childTransforms[i].position.x, childTransforms[i-1].position.x, Time.deltaTime * 20), 
                                                                Mathf.Lerp(childTransforms[i].position.y, childTransforms[i - 1].position.y, Time.deltaTime * 20), 
                                                                childTransforms[i-1].position.z - childTransforms[i - 1].forward.z * 1.5f);
                childTransforms[i].localPosition = Vector3.Lerp(childTransforms[i].localPosition, childTransforms[i - 1].localPosition - childTransforms[i - 1].transform.forward * .5f, Time.deltaTime * 25) ;
                childTransforms[i].transform.rotation = Quaternion.Lerp(childTransforms[i].rotation, childTransforms[i - 1].rotation, Time.deltaTime * 22);
            }*/
        }
        //rb.MovePosition(Vector3.Lerp(transform.position, target.position, Time.deltaTime * 20));
        //transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 10);
        /*
        Vector3 targetRotation;
        targetRotation = new Vector3(startIdleRotation.x,
        startIdleRotation.y,
        startIdleRotation.z + (target.position.x - this.transform.position.x) * 5);
        */
        
        //transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, targetRotation, Time.deltaTime * 5);

        //rb.MoveRotation(Quaternion.Euler(targetRotation));
        //transform.localEulerAngles = targetRotation;

        /*
    Vector2 newposx = target.gameObject.transform.position;
    Vector3 newPos = theTarStartPosition;
    newPos.z += transform.position.z;
    newPos.y = transform.position.y;
    newPos.x = newposx.x;
    target.transform.position = newPos;*/
    }
    public void Started()
    {
        foreach(Animator deer in deerAnimators)
        {
            deer.SetBool("isRun", true);
            deer.speed = Random.Range(.9f, 1.05f);
        }
        /*
        for (int i = 0; i < deerAnimators.Count; i++) 
        {
            print(deerAnimators[i]);
            deerAnimators[i].SetBool("isRun", true);
        }*/
    }
    public void GetGift()
    {

        GameObject obj = ObjectPooler.SharedInstance.GetPooledObject();
        obj.transform.parent = rightHand;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = Vector3.zero;
        HoldingGift = obj;
        isHoldingGift = true;
        anim.SetBool("TakeGift", false);
    }
    public void ThrowGift(Transform chimneyPos)
    {
        targetChimney = chimneyPos;
        if(childTransforms[3].position.x >= 0)
        {
            anim.SetBool("ThrowRight", true);
        }
        else if (childTransforms[3].position.x < 0)
        {
            anim.SetBool("ThrowLeft", true);
        }
    }
    public void GiftThrowed()
    {
        HoldingGift.transform.parent = null;
        //HoldingGift.GetComponent<Rigidbody>().isKinematic = false;
        //HoldingGift.GetComponent<Rigidbody>().AddForce(Vector3.right * Mathf.Clamp(childTransforms[3].position.x, -1, 1) * 400);
        StartCoroutine(GiftMove(HoldingGift));
        anim.SetBool("TakeGift", false);
        anim.SetBool("ThrowLeft", false);
        anim.SetBool("ThrowRight", false);
        isHoldingGift = false;
        
    }
    public IEnumerator GiftMove(GameObject gift)
    {
        GameObject tempGift = gift;
        float time = 0;
        Vector3 startPos = gift.transform.position;
        Vector3 halfpos = (targetChimney.position + gift.transform.position) * .5f;
        halfpos += Vector3.up * 6;
        while (time < 1)
        {
            Vector3 lerp1 = Vector3.Lerp(startPos, halfpos, time * 2);
            Vector3 lerp2 = Vector3.Lerp(halfpos, targetChimney.position, time * 2);
            
            tempGift.transform.position = Vector3.Lerp(lerp1, lerp2, time);
            time += Time.deltaTime * 2;
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(CloseGift(gift));
        GameObject obj = ObjectPooler2.SharedInstance.GetPooledObject();
        obj.transform.parent = null;
        obj.transform.localPosition = targetChimney.transform.position;
        targetChimney.GetComponentInParent<chimney>().Collected();
        AudioManager.singleton.PlayRingSound();
        RDG.Vibration.Vibrate(75 , 50);
        //obj.transform.localEulerAngles = Vector3.zero;
    }

    public IEnumerator CloseGift(GameObject objtoClose)
    {
        GameObject tempGift = objtoClose;        
        //tempGift.GetComponent<Rigidbody>().isKinematic = true;
        tempGift.SetActive(false);
        yield return new WaitForSeconds(.1f);
    }
    public void Death()
    {
        if (isAlive)
        {
            isAlive = false;
            RDG.Vibration.Vibrate(500);
            for (int i = 0; i < DeathAddRigidbodies.Count; i++)
            {
                DeathAddRigidbodies[i].isKinematic = false;
                DeathAddRigidbodies[i].AddTorque(Vector3.one * Random.Range(0,180));
                DeathAddRigidbodies[i].AddForce(new Vector3(Random.Range(0, 200), Random.Range(0, 200), Random.Range(2, 20) * 100));
            }
            foreach (Animator deer in deerAnimators)
            {
                deer.SetBool("isRun", false);
            }
            /*
            leftArm.transform.parent = leftElbow.transform;
            leftArm.GetComponent<Rigidbody>().isKinematic = false;
            rightArm.transform.parent = rightElbow.transform;
            rightArm.GetComponent<Rigidbody>().isKinematic = false;          */
        }
    }
    IEnumerator TriggerWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(.25f);
            if (isAlive)
            {
                Collider[] colliders = Physics.OverlapSphere(childTransforms[3].transform.position, 50, 1 << 16);
                foreach (Collider col in colliders)
                {
                    if (col.GetComponent<AIBehavior>() != null)
                    {
                        col.GetComponent<AIBehavior>().WaveTrigger(childTransforms[3].gameObject);
                    }
                }
            }
        }
    }
    public void TriggerFear()
    {
        if (isAlive)
        {
            Collider[] colliders = Physics.OverlapSphere(childTransforms[3].transform.position, 70, 1 << 16);
            foreach (Collider col in colliders)
            {
                if (col.GetComponent<AIBehavior>() != null)
                {
                    col.GetComponent<AIBehavior>().Fear(childTransforms[3].gameObject);
                }
            }
        }
    }
}
