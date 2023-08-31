using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject playerPos;
    [SerializeField] Transform hips;
    public float xBound;
    private Vector3 offset;
    public Vector3 offsetChanged;
    [SerializeField] Rigidbody playerRb;
    public bool isPlaying, isPov, isDead, isSnow;
    void Start()
    {
        Application.targetFrameRate = 60;
        if (isPov)
        {
            offset = transform.localPosition;
        }
        else
        {
            offset = transform.position - playerPos.transform.position;
        }
        
    }

    public void PlayerKickedBoss(GameObject gameObject)
    {
        playerPos = gameObject;
        offset = new Vector3(2, 7, -7);
        isPlaying = false;
        isDead = false;
    }

    void LateUpdate()
    {
        if (isSnow)
        {
            Vector3 curpos = transform.position;
            curpos.x = playerPos.transform.position.x;
            if (curpos.x < -xBound)
            {
                curpos.x = -xBound;
            }
            if (curpos.x > xBound)
            {
                curpos.x = xBound;
            }
            float clampedOff = Mathf.Clamp(playerRb.velocity.magnitude / 10, 2, 3f);
            offsetChanged = new Vector3(offset.x, offset.y * clampedOff / 2, offset.z * clampedOff / 2);
            if (offsetChanged.z > offset.z)
            {
                curpos.y = playerPos.transform.position.y + offset.y;
                curpos.z = playerPos.transform.position.z + offset.z;
            }
            if (offsetChanged.z <= offset.z)
            {
                curpos.y = playerPos.transform.position.y + offsetChanged.y;
                curpos.z = playerPos.transform.position.z + offsetChanged.z;
            }
            transform.position = curpos;
        }
        else
        {
            if (isPlaying)
            {
                if (isPov)
                {
                    float clampedOff = Mathf.Clamp(playerRb.velocity.magnitude * .07f, 2, 3f);
                    offsetChanged = new Vector3(offset.x, offset.y * clampedOff * .5f, offset.z * clampedOff * .5f);
                    transform.localPosition = offsetChanged;

                    if (playerRb.velocity.magnitude > 40)
                    {
                        GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, (float)(-270.6284 + 89.6284 * Mathf.Log(playerRb.velocity.magnitude)), Time.deltaTime * 10);
                        //GetComponent<Camera>().fieldOfView = playerRb.velocity.magnitude + 20;
                    }
                    else
                        GetComponent<Camera>().fieldOfView = 60;
                }

                else
                {
                    Vector3 curpos = transform.position;
                    curpos.x = playerPos.transform.position.x;
                    if (curpos.x < -xBound)
                    {
                        curpos.x = -xBound;
                    }
                    if (curpos.x > xBound)
                    {
                        curpos.x = xBound;
                    }
                    float clampedOff = Mathf.Clamp(playerRb.velocity.magnitude / 10, 2, 3f);
                    offsetChanged = new Vector3(offset.x, offset.y * clampedOff / 2, offset.z * clampedOff / 2);
                    if (offsetChanged.z > offset.z)
                    {
                        curpos.y = playerPos.transform.position.y + offset.y;
                        curpos.z = playerPos.transform.position.z + offset.z;
                    }
                    if (offsetChanged.z <= offset.z)
                    {
                        curpos.y = playerPos.transform.position.y + offsetChanged.y;
                        curpos.z = playerPos.transform.position.z + offsetChanged.z;
                    }
                    Vector3 smoothedPos = Vector3.Lerp(transform.position, curpos, Time.deltaTime * 8);
                    transform.position = curpos;

                    transform.localRotation = new Quaternion(transform.localRotation.x, Mathf.Lerp(transform.localRotation.y, playerPos.transform.localRotation.y, Time.deltaTime * 15), transform.localRotation.z, transform.localRotation.w);
                }

            }
            else if (isDead)
            {
                transform.LookAt(hips);
                Vector3 curpos = transform.position;
                curpos.x = hips.position.x;
                if (curpos.x < -xBound)
                {
                    curpos.x = -xBound;
                }
                if (curpos.x > xBound)
                {
                    curpos.x = xBound;
                }
                float clampedOff = Mathf.Clamp(playerRb.velocity.magnitude / 10, 2, 3f);
                offsetChanged = new Vector3(offset.x, offset.y * clampedOff / 2, offset.z * clampedOff / 2);
                if (offsetChanged.z > offset.z)
                {
                    curpos.y = hips.position.y + offset.y;
                    curpos.z = hips.position.z + offset.z;
                }
                if (offsetChanged.z <= offset.z)
                {
                    curpos.y = hips.position.y + offsetChanged.y;
                    curpos.z = hips.position.z + offsetChanged.z;
                }
                Vector3 smoothedPos = Vector3.Lerp(transform.position, curpos, Time.deltaTime * 8);
                transform.position = curpos;
            }
        }        
    }
    public void Death()
    {
        transform.parent = null;
        offset = transform.position-hips.transform.position; 
        isDead = true;
        isPov = false;
        isPlaying = false;
    }
}
