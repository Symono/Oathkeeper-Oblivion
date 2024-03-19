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
    private bool canMove = true; // Flag to control enemy movement



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", true);
        startingX = transform.position.x;
    }

    void FixedUpdate()
    {
      if (canMove) // Check if the enemy can move
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime * dir);
            if (transform.position.x < startingX || transform.position.x > startingX + range)
            {
                dir *= -1;
                FlipSprite();
            }
        }
    }
   void FlipSprite()
   {
    // Switch the direction the enemy is facing
    Vector3 newScale = transform.localScale;
    newScale.x *= -1;
    transform.localScale = newScale;
   }
   public void PauseMovement()
    {
        canMove = false; // Disable enemy movement
    }

    public void ResumeMovement()
    {
        canMove = true; // Enable enemy movement
    }

}