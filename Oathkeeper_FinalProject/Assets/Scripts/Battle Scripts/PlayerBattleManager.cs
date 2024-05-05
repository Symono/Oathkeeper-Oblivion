using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBattleManager : MonoBehaviour
{
    public GameObject BattleCanvas;
    public BattleManager battleManager;

    public Movement characterMovement;
    public Transform playerTransform;
    public float distanceToMoveBack = 5f;

    public Animator playerAnimator;

    private GameObject currentEnemy;


     private void OnCollisionEnter2D(Collision2D collision)
{
    // Battle Activation 
    if (collision.gameObject.CompareTag("Enemy"))
    {
        Debug.Log("Collided with an enemy");
        
        // Set current enemy
        currentEnemy = collision.gameObject;
        
        // Set Active Battle Canvas
        BattleCanvas.SetActive(true);
        
        // Start Script BattleManager
        battleManager.StartBattle(currentEnemy);
        
        // Calculate direction vector from player to enemy
        Vector2 direction = playerTransform.position - currentEnemy.transform.position;
        
        // Calculate new position by moving the player in the opposite direction from the enemy
        Vector2 newPosition = (Vector2)playerTransform.position + direction.normalized * distanceToMoveBack;
        
        // Move the player to the new position
        playerTransform.position = newPosition;
        
        //have player face the enemy
        FaceEnemy();
        
        // Set animator bool for running to false
        playerAnimator.SetBool("Run", false);
        // Check if the player is currently in the jump animation
        if (playerAnimator.GetBool("Jump"))
        {
            // Set jump animator parameter to false to transition to idle animation
            playerAnimator.SetBool("Jump", false);
        }
        // Deactivate Character Movement 
        characterMovement.enabled = false;
    }
}
 void FaceEnemy()
    {
        // Calculate direction to the enemy
        Vector3 direction = currentEnemy.transform.position - transform.position;
        
        // Flip player sprite based on the direction
        if (direction.x > 0) // enemy is to the right
        {
            transform.localScale = new Vector3(1, 1, 1); // Flip player to face right
        }
        else // Player is to the left
        {
            transform.localScale = new Vector3(-1, 1, 1); // Keep player facing left
        }
    }
}

