using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public float speed;
    public float range = 3;
    float startingX;
    int dir = 1;
    public bool canMove; // Flag to control enemy movement

    public EnemyData enemyData;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", true);
        startingX = transform.position.x;
    }

    void FixedUpdate()
{
    if (canMove)
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime * dir);
        if (transform.position.x < startingX || transform.position.x > startingX + range)
        {
            dir *= -1;
            FlipSprite();
        }
    }
    else
    {
        PauseMovement();
    }
}

    private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Ground"))
    {
        canMove = true; // Only allow movement if collided with an object tagged "Ground"
    }
    else
    {
        canMove = false; // Disable movement if collided with anything other than "Ground"
    }
}

    void OnDisable()
    {
        PauseMovement(); // Call PauseMovement when the script is disabled
    }
   public void FlipSprite()
   {
    // Switch the direction the enemy is facing
    Vector3 newScale = transform.localScale;
    newScale.x *= -1;
    transform.localScale = newScale;
   }
   public void PauseMovement()
    {
        canMove = false; // Disable enemy movement
        anim.SetBool("isRunning", false);
        rb.velocity = Vector2.zero; // Stop the enemy's velocity

    }

    public void ResumeMovement()
    {
        canMove = true; // Enable enemy movement
    }

}