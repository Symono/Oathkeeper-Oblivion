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

    private bool Grounded;

    public EnemyData enemyData;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", false);
        FlipSprite();
        startingX = transform.position.x;
    }

    void FixedUpdate()
    {

    if (Grounded )
    {
        anim.SetBool("isRunning", true);
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
        Grounded = true; // Only allow movement if collided with an object tagged "Ground"
    }
    else
    {
        Grounded = false; // Disable movement if collided with anything other than "Ground"
    }
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
        anim.SetBool("isRunning", false);
        rb.velocity = Vector2.zero; // Stop the enemy's velocity

    }


}