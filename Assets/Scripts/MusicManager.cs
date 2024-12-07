using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager MM;
    AudioSource mySpeaker;
    //[Header("Music")]
    //public List<AudioClip> musicList = new List<AudioClip>();
    // Start is called before the first frame update
    void Start()
    {
        if (MusicManager.MM == null)
        {
            MusicManager.MM = this;
            mySpeaker = GetComponent<AudioSource>();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log("An extra version of music manager exists. Please make sure Music manager only exists one time.");
            Destroy(gameObject);
        }    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Debug.Log("Closing Application.");
            Application.Quit();
        }
    }
    public void PlayMusic(AudioClip musicClip, bool loopMusic = true)
    {
        mySpeaker.clip = musicClip;
        mySpeaker.loop = loopMusic;
        mySpeaker.Play();
    }
}
