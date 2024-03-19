using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;


public class BattleManager : MonoBehaviour
{
    public GameObject Player;
    public Transform playerStartPosition; // The position where the player should start in the battle scene

    public static BattleManager instance;
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    //start battle

    public void StartBattle(GameObject enemy, Vector3 playerPosition, Vector3 enemyPosition)
    {
        //
        Debug.Log("Battle Started");
        // Access the player GameObject from the Movement script
        GameObject player = FindObjectOfType<Movement>().gameObject;
        
        // Move the player to the specified position
        player.transform.position = playerPosition;
        // Move the enemy to the specified position
        enemy.transform.position = enemyPosition;

        // Ensure the player and enemy are facing each other
        Vector3 direction = (enemyPosition - playerPosition).normalized;
        enemy.transform.right = -direction;

        //disable enemy and player movement
        Movement playerMovement = player.GetComponent<Movement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        Animator playerAnimator = player.GetComponent<Animator>();
        if(playerAnimator != null)
        {
            playerAnimator.SetBool("Run", false);
        }
        // Pause enemy movement
        EnemyPatrol enemyPatrol = enemy.GetComponent<EnemyPatrol>();
        if (enemyPatrol != null)
        {
            enemyPatrol.PauseMovement();
        }
        // Set animation of enemy to idle
        Animator enemyAnimator = enemy.GetComponent<Animator>();
        if (enemyAnimator != null)
        {
            enemyAnimator.SetBool("isRunning", false);
        }
        
    }


    public void EndBattle()
    {
        Debug.Log("Battle Ended!");
    }


}
