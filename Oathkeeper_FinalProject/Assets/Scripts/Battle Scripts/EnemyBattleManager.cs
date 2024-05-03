using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleManager : MonoBehaviour
{
    public BattleManager battleManager;
    public EnemyPatrol enemyPatrol;
    public Transform playerTransform;
   
    public EnemyData enemyData;

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FacePlayer();
            enemyPatrol.PauseMovement();           
        }
    }
    void FacePlayer()
    {
        // Calculate direction to the player
        Vector3 direction = playerTransform.position - transform.position;
        
        // Flip enemy sprite based on the direction
        if (direction.x > 0) // Player is to the right
        {
            transform.localScale = new Vector3(3, 3, 3); // Flip enemy to face right
        }
        else // Player is to the left
        {
            transform.localScale = new Vector3(-3, 3, 3); // Keep enemy facing left
        }
    }
    

    void Update()
    {
        if (enemyData.currentHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    
}

