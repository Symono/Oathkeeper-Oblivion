using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 10f;

    Rigidbody2D rb;
    private Animator anim;
    private bool grounded;
    public GameObject battleCanvas;
    public Vector3 playerStartPosition; // Specific position for the player
    public Vector3 enemyStartPosition; // Specific position for the enemy

    private bool canMove = true; // Flag to control player movement

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (canMove) // Check if the player can move
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            MovePlayer(horizontalInput);

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && grounded) // Check if grounded and not already jumping
            {
                Jump();
            }
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
        // Battle Activation code
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with an enemy");
            // Show the battle scene canvas
            battleCanvas.SetActive(true);

            if (BattleManager.instance != null)
            {
                // Pause player movement
                canMove = false;
                BattleManager.instance.StartBattle(collision.gameObject, playerStartPosition, enemyStartPosition);
            }
            else
            {
                Debug.LogWarning("BattleManager instance is null!");
            }
        }
    }

    public bool IsGrounded()
    {
        return grounded;
    }

    // Method to resume player movement
    public void ResumeMovement()
    {
        canMove = true;
    }
}
