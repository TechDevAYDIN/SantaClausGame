using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBehavior : MonoBehaviour
{
    bool isAlive = true;
    // Durumlarý tutmak için bir enum oluþturun
    public enum AIState
    {
        Normal,
        Fear,
        Greeting,
        Dead
    }
    public List<GameObject> bodies;
    public List<Collider> ragdollColliders;
    public List<Transform> allWaypoints = new List<Transform>();
    [SerializeField] Rigidbody hipsRb;
    // Navmesh sistemini kullanmak için NavMeshAgent nesnesini oluþturun
    private NavMeshAgent agent;

    // Waypoint noktalarýný tutmak için bir dizi oluþturun
    //public Transform[] closestWaypoints = new Transform[4];
    // Durumu tutmak için bir deðiþken oluþturun
    public AIState currentState;

    // Oyuncuyu tutmak için bir nesne oluþturun
    private GameObject player;

    private Animator anim;

    [SerializeField] Transform chosenWaypoint;
    [SerializeField] Transform lastWaypoint;
    void Start()
    {
        transform.localScale = Vector3.one * Random.Range(2.75f, 3.25f);
        // Baþlangýçta NavMeshAgent nesnesini ve oyuncuyu bulun
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        bodies[Random.Range(0, bodies.Count)].SetActive(true);
        // Baþlangýç durumunu ayarlayýn
        currentState = AIState.Normal;
        
        Invoke("CheckState", .2f);
    }
    
    void Update()
    {
        if(currentState.Equals(AIState.Normal))
        {

            if (chosenWaypoint != null)
            {
                if (Vector3.Distance(transform.position, chosenWaypoint.position) <= 3f)
                {
                    anim.SetBool("isWalking", false);
                    CheckState();
                }
            }
        }
        if (currentState.Equals(AIState.Greeting))
        {
            if(player != null)
            {
                transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            }
        }
        if (currentState.Equals(AIState.Fear))
        {
            if (chosenWaypoint != null)
            {
                if (Vector3.Distance(transform.position, chosenWaypoint.position) <= 3f)
                {
                    anim.SetBool("isWalking", false);
                    CheckState();
                }
            }
        }
    }
    public void CheckState()
    {
        // Durumlara göre farklý iþlemler yapýn
        switch (currentState)
        {
            case AIState.Normal: // Normal durum
                // Önceden belirlenmiþ waypoint noktalarýndan rastgele birine git
                ChooseWaypoint();
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isWaving", false);
                if(chosenWaypoint == null)
                {
                    ChooseWaypoint();
                    agent.SetDestination(chosenWaypoint.position);
                }
                else
                {
                    agent.SetDestination(chosenWaypoint.position);
                }
                
                //agent.destination = chosenWaypoint.position;
                break;
            case AIState.Fear: // Korkma durumu
                // Oyuncudan kaçmaya çalýþ
                ChooseWaypoint();
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", true);
                anim.SetBool("isWaving", false);
                if (chosenWaypoint == null)
                {
                    ChooseWaypoint();
                    agent.SetDestination(chosenWaypoint.position);
                }
                else
                {
                    agent.SetDestination(chosenWaypoint.position);
                }
                //agent.destination = chosenWaypoint.position;
                break;
            case AIState.Greeting: // Selamlama durumu
                Greeting();
                break;
        }
    }
    public void Death(Vector3 pos)
    {
        if(currentState != AIState.Dead)
        {
            currentState = AIState.Dead;
            isAlive = false;
            GameManager.singleton.FearTrigger();
            GetComponent<CapsuleCollider>().enabled = false;
            agent.enabled = false;
            anim.enabled = false;
            foreach (Collider coll in ragdollColliders)
            {
                coll.enabled = true;
                coll.GetComponent<Rigidbody>().isKinematic = false;
                coll.GetComponent<Rigidbody>().AddExplosionForce(5000, pos, 10);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7 && isAlive)
        {
            Death(collision.transform.position);
            AudioManager.singleton.PlayPropSound();
            RDG.Vibration.Vibrate(75, 50);
        }
    }
    public void Greeting()
    {
        // Oyuncuya el sallama animasyonunu çalýþtýr
        anim.SetBool("isRunning", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isWaving", true);
        agent.enabled = false;
    }
    public void Fear(GameObject pl)
    {
        player = pl;
        anim.SetBool("isRunning", true);
        anim.SetBool("isWalking", false);
        anim.SetBool("isWaving", false);
        agent.enabled = true;
        agent.speed = Random.Range(10, 15);
        if (currentState != AIState.Dead)
            currentState = AIState.Fear;
        CheckState();
    }
    public void WaveTrigger(GameObject pl)
    {        
        player = pl;
        if(currentState != AIState.Fear)
            currentState = AIState.Greeting;
        CheckState();
    }
    public void ChooseWaypoint()
    {
        GetClosestWaypoints();
        Transform rand = allWaypoints[Random.Range(0, allWaypoints.Count)];
        while (rand == lastWaypoint)
            rand = allWaypoints[Random.Range(0, allWaypoints.Count)];
        lastWaypoint = chosenWaypoint;
        chosenWaypoint = rand;
    }
    public void GetClosestWaypoints()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 80, 1 << 18);
        //List<Transform> allWaypoints = new List<Transform>();
        allWaypoints.Clear();
        foreach (Collider c in colliders)
        {
            allWaypoints.Add(c.transform);
        }
        /*
        closestWaypoints[0] = null;
        closestWaypoints[1] = null;
        closestWaypoints[2] = null;
        closestWaypoints[3] = null;
        closestWaypoints[4] = null;
        foreach (Transform t in allWaypoints)
        {
            if (Vector3.Distance(transform.position, t.position) > 5)
            {
                // Dizinin ilk elemaný için eðer boþsa doldurun
                if (closestWaypoints[0] == null)
                {
                    closestWaypoints[0] = t;
                }
                // Dizinin ikinci elemaný için eðer boþsa veya mevcut elemandan daha uzaksa doldurun
                else if (closestWaypoints[1] == null || Vector3.Distance(transform.position, t.position) < Vector3.Distance(transform.position, closestWaypoints[1].position))
                {
                    closestWaypoints[1] = t;
                }
                // Dizinin üçüncü elemaný için eðer boþsa veya mevcut elemandan daha uzaksa doldurun
                else if (closestWaypoints[2] == null || Vector3.Distance(transform.position, t.position) < Vector3.Distance(transform.position, closestWaypoints[2].position))
                {
                    closestWaypoints[2] = t;
                }
                // Dizinin dördüncü elemaný için eðer boþsa veya mevcut elemandan daha uzaksa doldurun
                else if (closestWaypoints[3] == null || Vector3.Distance(transform.position, t.position) < Vector3.Distance(transform.position, closestWaypoints[3].position))
                {
                    closestWaypoints[3] = t;
                }
                // Dizinin beþinci elemaný için eðer boþsa veya mevcut elemandan daha uzaksa doldurun
                else if (closestWaypoints[4] == null || Vector3.Distance(transform.position, t.position) < Vector3.Distance(transform.position, closestWaypoints[4].position))
                {
                    closestWaypoints[4] = t;
                }
            }
        }*/
    }
}
