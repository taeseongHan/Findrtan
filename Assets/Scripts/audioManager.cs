using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public AudioClip bgmusic;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        //계속해서 음악 재생
        audioSource.clip = bgmusic;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
