using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 10f;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        MovePlayer(horizontalInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public void MovePlayer(float horizontalInput)
    {
        Vector2 movement = new Vector2(horizontalInput * speed, rb.velocity.y);
        rb.velocity = movement;
        //flip player left and right
        if(horizontalInput > 0.01f)
          transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
          transform.localScale = new Vector3(-1,1,1);  
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
