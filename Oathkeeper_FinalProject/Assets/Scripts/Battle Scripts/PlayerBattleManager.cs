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

     private void OnCollisionEnter2D(Collision2D collision)
    {
        // Battle Activation 
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with an enemy");
            //Set Active Battle Canvas
            BattleCanvas.SetActive(true);
            //Start Script BattleManager
            battleManager.StartBattle();
            //Set Player position
            Vector2 newPosition = (Vector2)playerTransform.position * (-1 * distanceToMoveBack); // Calculate new position
            playerTransform.position = newPosition; // Move the player to the new position

            // set animator bool for running to false
            playerAnimator.SetBool("Run", false);

            //Un Activate Character Movement 
            characterMovement.enabled = false;
            }
        }
}

