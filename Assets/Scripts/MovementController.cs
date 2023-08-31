using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MovementController : MonoBehaviour
{
    public bool isAlive = true;
    Rigidbody rb;
    private float delta;
    private Vector2 lastTapPos;
    public DynamicJoystick joystick;
    public float xBound;
    public float speed;

    public float zKalan;
    [SerializeField] Image fillBar;

    [SerializeField] public ParticleSystem CameraSpeedEffect;
    ParticleSystem.EmissionModule emissionModule;
    ParticleSystem.VelocityOverLifetimeModule VelocityOverLifetimeModule;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        emissionModule = CameraSpeedEffect.emission;
        VelocityOverLifetimeModule = CameraSpeedEffect.velocityOverLifetime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAlive)
        {
            rb.velocity = transform.forward * speed;
            LimitCheck();
            JoystickControl();
            fillBar.fillAmount = transform.position.z / zKalan;
        }
        else
        {
            emissionModule.rateOverTime = 0;
            rb.velocity = Vector3.zero;
            speed = 0;
        }
    }
    void LimitCheck()
    {
        if (transform.position.x < -xBound)
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        if (transform.position.x > xBound)
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);

        Ray ray = new Ray(transform.position, Vector3.down * 5f);
        Debug.DrawRay(transform.position, Vector3.down * 5f);
        if (Physics.Raycast(ray, out RaycastHit hit, 1 << 10))
        {
            if (hit.collider.gameObject.layer == 10)
            {/*
                if (transform.position.y < hit.point.y + 15)
                {
                    transform.position = new Vector3(transform.position.x, hit.point.y + 15, transform.position.z);
                }*/
                    
                if (transform.position.y >  20)
                    transform.position = new Vector3(transform.position.x, 20, transform.position.z);
            }
        }
    }
    void JoystickControl()
    {
        if (Input.GetMouseButton(0))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.right * 3f * joystick.Horizontal + Vector3.up * 2f * joystick.Vertical, Time.deltaTime * 12);
            //transform.localEulerAngles = new Vector3(20f * -joystick.Vertical, 20f * joystick.Horizontal, 0);
            VelocityOverLifetimeModule.x = -joystick.Horizontal * .5f;
            VelocityOverLifetimeModule.y = -joystick.Vertical * .5f;
            if (speed < 35)
            {
                speed += Time.deltaTime * 10;
            }
            else
            {
                if (speed >= 35 && speed < 45)
                {
                    if (Input.GetMouseButton(0))
                    {
                        emissionModule.rateOverTime = -900.0404f + 244.1469f * Mathf.Log(speed);
                        speed += (joystick.Vertical) * Time.deltaTime * -12;
                    }
                }
            }
        }
        else
        {
            VelocityOverLifetimeModule.x = 0;
            VelocityOverLifetimeModule.y = 0;
            transform.localEulerAngles = Vector3.zero;
            if (speed <= 25)
            {
                speed += Time.deltaTime * 12;
            }
            else if (speed <= 40)
            {
                emissionModule.rateOverTime = speed;
                speed += Time.deltaTime * 6;
            }

        }
        if (speed > 40)
        {
            emissionModule.rateOverTime = -900.0404f + 244.1469f * Mathf.Log(speed);
            speed -= Time.deltaTime * 8;
        }
    }
   
}
