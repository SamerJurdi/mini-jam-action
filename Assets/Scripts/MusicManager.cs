using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource mySpeaker;
    //[Header("Music")]
    //public List<AudioClip> musicList = new List<AudioClip>();
    // Start is called before the first frame update
    void Start()
    {
        mySpeaker = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
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
}
