using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region SINGLETON
    public static AudioManager Singleton;
    public static AudioManager singleton
    {
        get
        {
            if (Singleton == null)
            {
                Singleton = GameObject.FindObjectOfType<AudioManager>();

                if (Singleton == null)
                {
                    GameObject container = new GameObject("AM");
                    Singleton = container.AddComponent<AudioManager>();
                }
            }
            return Singleton;
        }
    }
    #endregion
    [SerializeField] AudioSource musicSource;
    public List<AudioClip> musics;
    [SerializeField]AudioSource mainWindSource;
    [SerializeField]AudioSource PropellerSource;
    [SerializeField] AudioSource collectRing;
    [SerializeField] AudioSource crushSound;
    [SerializeField] AudioSource santaVoice;
    [SerializeField] AudioClip[] santaClips = new AudioClip[4];
    [SerializeField] Rigidbody player;
    float aPitch, aVol;
    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = musics[Random.Range(0,musics.Count)];
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //print(player.velocity.magnitude);
        if(player.velocity.magnitude > 40)
        {
            aPitch = (float)(-1.9277 + 0.7948 * Mathf.Log(player.velocity.magnitude));
            //aVol = (float)(-2.0832f + 0.6072f * Mathf.Log(player.velocity.magnitude));
            mainWindSource.pitch = aPitch;
        }
        if (player.velocity.magnitude <= 40 && player.velocity.magnitude > 34)
        {
            aPitch = (float)(-1.9277f + 0.7948f * Mathf.Log(player.velocity.magnitude));
            //aVol = (float)(-2.0832f + 0.6072f * Mathf.Log(player.velocity.magnitude));
            mainWindSource.pitch = aPitch;
        }
        if(player.velocity.magnitude == 0)
        {
            mainWindSource.pitch = 0;
        }
        if(player.velocity.magnitude < 30)
            mainWindSource.volume = 0;
        if (player.velocity.magnitude >= 30)
            mainWindSource.volume = 0.25f;
    }
    public void PlayHoSound()
    {
        int randomValue = Random.Range(0, 7);
        if(randomValue <= 4)
        {
            santaVoice.clip = santaClips[randomValue];
            santaVoice.Play();
        }
    }
    public void PlayPropSound()
    {
        PropellerSource.Stop();
        PropellerSource.Play();
    }
    public void PlayRingSound()
    {
        collectRing.Play();
    }
    public void PlayCrushSound()
    {
        crushSound.Play();
    }
}
