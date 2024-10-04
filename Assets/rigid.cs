using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigid : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        rb.AddForce(new Vector2(0, 500));
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Touched ground");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
