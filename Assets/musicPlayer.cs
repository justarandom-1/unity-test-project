using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    private bool preFaded = true;
    private bool postFaded = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    void OnTriggerEnter2D (Collider2D other){
        if (other.gameObject.CompareTag("Player") && preFaded){
            preFaded = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!preFaded && !postFaded){
            audioSource.volume = Mathf.Max(audioSource.volume - Time.deltaTime, 0F);
            if(audioSource.volume == 0){
                postFaded = true;
                audioSource.Pause();
                audioSource.clip = Resources.Load <AudioClip> ("Music/nausicaa_loop");
                audioSource.Play();
            }
        }
        if(postFaded && audioSource.isPlaying && audioSource.volume < 1F){
            audioSource.volume = Mathf.Min(audioSource.volume + 0.25F * Time.deltaTime, 1F);
        }
    }
}
