using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public List<GoalPart> goalParts;
    bool isPlayerReach = false;
    [SerializeField] bool iswin;
    private void Update()
    {
        if (iswin)
        {
            iswin = false;
            BlastAll();
        }
    }
    public void PlayerTouchThePart()
    {
        if (!isPlayerReach)
        {
            isPlayerReach = true;
            BlastAll();
            GameManager.singleton.GoalReached();
        }
    }
    public void BlastAll()
    {
        for (int i = 0; i < goalParts.Count; i++)
        {
            goalParts[i].Blast();
        }
    }
}
