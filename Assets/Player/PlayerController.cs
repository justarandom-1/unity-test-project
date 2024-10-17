using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementVector;
    private Rigidbody2D rb;
    private Transform transform_;
    [SerializeField] bool onGround = false;
    [SerializeField] int jumpsFromGround = 0;
    [SerializeField] int collectibles = 0;
    private GameObject star_block;
    private GameObject star_block1;
    // Start is called before the first frame update

    [SerializeField] Animator animator;
    [SerializeField] int speed = 0;

    private bool isDashing = false;

    public AudioSource audioSource;
    private AudioClip moveSFX;
    private AudioClip dashSFX;
    private AudioClip jumpSFX;
    private AudioClip collectSFX;
    private bool isCollecting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transform_ = GetComponent<Transform>();
        star_block = GameObject.Find("star_block");
        star_block1 = GameObject.Find("star_block (1)");

        audioSource = GetComponent<AudioSource>();
        moveSFX = Resources.Load <AudioClip> ("SFX/porcupine");
        dashSFX = Resources.Load <AudioClip> ("SFX/dash");
        jumpSFX = Resources.Load <AudioClip> ("SFX/jump");
        collectSFX = Resources.Load <AudioClip> ("SFX/collect");
    }

    void OnCollisionEnter2D (Collision2D collision)
    {;
        if (collision.gameObject.CompareTag("Ground") 
        && transform_.position.y > collision.GetContact(0).point.y)
        {
            jumpsFromGround = 0;
            onGround = true;
        }
    }

    void OnCollisionExit2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectible") && !isCollecting)
        {
            isCollecting = true;
            other.enabled = false;
            other.gameObject.GetComponent<Animator>().SetTrigger("Collect");
            audioSource.PlayOneShot(collectSFX);

            collectibles++;
            Sprite updateBlock = Resources.LoadAll<Sprite>("star_block")[collectibles];
            star_block.GetComponent<SpriteRenderer>().sprite = updateBlock;
            star_block1.GetComponent<SpriteRenderer>().sprite = updateBlock;
            
            if(collectibles == 6){
                GameObject.Find("MusicPlayer").GetComponent<AudioSource>().Pause();
                audioSource.PlayOneShot(Resources.Load <AudioClip> ("SFX/rumbling"));
                GameObject.Find("william").SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        isCollecting = false;
        rb.velocity = new Vector2(speed * movementVector.x, rb.velocity.y);
        animator.SetBool("isMoving", movementVector.x != 0);
        animator.SetBool("canDash", (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && jumpsFromGround == 0);

        if(animator.GetBool("canDash") && animator.GetBool("isMoving") && !isDashing){
            audioSource.PlayOneShot(dashSFX, 0.7F);
            isDashing = true;
            movementVector = new Vector2(movementVector.x * 2, movementVector.y);
        }

        if(!animator.GetBool("canDash") && isDashing){
            isDashing = false;
            movementVector = new Vector2(movementVector.x / 2, movementVector.y);
        }

        if(!animator.GetBool("isMoving")){
            isDashing = false;
        }

        if(animator.GetBool("isMoving") && !audioSource.isPlaying  && jumpsFromGround == 0)
        {
            audioSource.PlayOneShot(moveSFX, 0.15F);
        }
    }

    void OnMove(InputValue value){
        movementVector = value.Get<Vector2>();
        if(movementVector.x * transform_.localScale.x > 0){
            transform_.localScale = new Vector3(transform_.localScale.x * -1, transform_.localScale.y, transform_.localScale.z);
        }
    }

    void OnJump(InputValue value){
        if(onGround){
            audioSource.PlayOneShot(jumpSFX, 0.5F);
            rb.AddForce(new Vector2(0, 400));
        }else if(jumpsFromGround < 2){
            audioSource.PlayOneShot(jumpSFX, 0.3F);
            rb.AddForce(new Vector2(0, 300));
        }
        jumpsFromGround++;
    }

    public int getCollectibles(){
        return collectibles;
    }
}
