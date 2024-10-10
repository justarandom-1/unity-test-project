using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementVector;
    private Rigidbody2D rb;
    private bool onGround = false;
    private int jumpsFromGround = 0;
    // Start is called before the first frame update

    [SerializeField] int speed = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        // rb.AddForce(new Vector2(0, 500));
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsFromGround = 0;
            onGround = true;
        }
    }
    void OnCollisionExit2D (Collision2D collision)
    {
        // rb.AddForce(new Vector2(0, 500));
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed * movementVector.x, rb.velocity.y);
    }

    void OnMove(InputValue value){
        movementVector = value.Get<Vector2>();
        if (Input.GetKey(KeyCode.LeftShift)) {
            Debug.Log("Shift pressed");
            movementVector *= 2f;
        }
        Debug.Log(movementVector);
    }

    void OnJump(InputValue value){
        if(onGround || jumpsFromGround < 2){
            rb.AddForce(new Vector2(0, 400));
            jumpsFromGround++;
        }
    }
}
