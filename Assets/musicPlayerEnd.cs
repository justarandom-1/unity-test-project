using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicPlayerEnd : MonoBehaviour
{
    private AudioSource audioSource;
    private PlayerController playerScript;
    private Collider2D collider_;
    private bool endingReached = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        collider_ = GetComponent<Collider2D>();
        collider_.enabled = false;
        audioSource.volume = 0F;
        playerScript = (PlayerController) GameObject.Find("Player").GetComponent(typeof(PlayerController));
    }

    void OnTriggerEnter2D (Collider2D other){
        endingReached = true;   
        collider_.enabled = false;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!endingReached && !collider_.enabled && playerScript.getCollectibles() >= 6 && !audioSource.isPlaying){
            collider_.enabled = true;
        }
        if(endingReached && audioSource.isPlaying && audioSource.volume < 1F){
            audioSource.volume = Mathf.Min(audioSource.volume + Time.deltaTime, 1F);
        }
    }
}
