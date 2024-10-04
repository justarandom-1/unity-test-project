using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementVector;
    private Rigidbody2D rb;
    private bool canJump = false;
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
            Debug.Log("Touched ground");
            canJump = true;
        }
    }
    void OnCollisionExit2D (Collision2D collision)
    {
        // rb.AddForce(new Vector2(0, 500));
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed * movementVector.x, rb.velocity.y);
    }

    void OnMove(InputValue value){
        movementVector = value.Get<Vector2>();
        Debug.Log(movementVector);
        // movementX = v.x;
        // movementZ = v.y;
    }

    void OnJump(InputValue value){
        if(canJump){
            rb.AddForce(new Vector2(0, 500));
        }
    }
}
