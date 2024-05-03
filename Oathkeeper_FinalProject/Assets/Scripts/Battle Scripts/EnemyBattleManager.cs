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
            enemyPatrol.PauseMovement();           
        }
    }
    void FacePlayer()
    {
        //need to do this
    }

    void Update()
    {
        if (enemyData.currentHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    
}

