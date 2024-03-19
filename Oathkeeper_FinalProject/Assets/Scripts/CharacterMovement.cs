using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 10f;

    Rigidbody2D rb;
    private Animator anim;
    private bool grounded;    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        MovePlayer(horizontalInput);
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && grounded) // Check if grounded and not already jumping
        {
            Jump();
        }
    }

    public void MovePlayer(float horizontalInput)
    {
        // Add a deadzone to prevent abrupt stopping
        if (Mathf.Abs(horizontalInput) < 0.01f)
            horizontalInput = 0f;

        Vector2 movement = new Vector2(horizontalInput * speed, rb.velocity.y);
        rb.velocity = movement;

        // Flip player left and right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Set animator parameters
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", grounded);
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        anim.SetTrigger("Jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
        // Battle Activation 
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with an enemy");
            // load into battle scene
            SceneManager.LoadScene("BattleScene");
            
        }
    }

    public bool IsGrounded()
    {
        return grounded;
    }

}
