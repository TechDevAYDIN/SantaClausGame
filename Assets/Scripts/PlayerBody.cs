using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    [SerializeField] PlayerController player;
    public void GiftTaken()
    {
        player.GetGift();
    }
    public void ThrowGift()
    {
        player.GiftThrowed();
    }
    /*
    [SerializeField] ParticleSystem DustParticles;
    [SerializeField] ParticleSystem DustParticlesTrails;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            if (!DustParticlesTrails.gameObject.activeSelf)
            {
                DustParticles.Play();
                DustParticlesTrails.gameObject.SetActive(true);
            }
            
        }
    }*/
}
