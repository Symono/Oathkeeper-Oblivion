using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleManager : MonoBehaviour
{
    public BattleManager battleManager;
    public EnemyPatrol enemyPatrol;
    public Transform playerTransform;
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyPatrol.PauseMovement();
            
           
        }
    }
    void FacePlayer()
    {
        //need to do this
    }

    void Update()
    {
        if (battleManager.state == BattleState.WON)
        {
            Destroy(gameObject);
        }
    }
}

