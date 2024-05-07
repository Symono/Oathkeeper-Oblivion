using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyBattleManager : MonoBehaviour,IDataPersistence
{
    
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid(){
        id = System.Guid.NewGuid().ToString();
    }
    public BattleManager battleManager;
    public EnemyPatrol enemyPatrol;
    public Transform playerTransform;
   
    public EnemyData enemyData;

    public bool enemyAlive = true;
    public int enemyHealth;

    public void Start(){
        
        enemyData.id = System.Guid.NewGuid().ToString();
        
      
    }

    
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
        enemyHealth = enemyData.currentHP;
        
        if (enemyHealth <= 0)
        {
            enemyAlive = false;
            enemyHealth = enemyData.currentHP;
            Destroy(this.gameObject);
        }
    }

    public void LoadData(GameData data){
        data.enemysDestroyed.TryGetValue(id, out enemyHealth);
        enemyData.currentHP = data.enemyAttributesData.currentHP;
        enemyData.id = data.enemyAttributesData.id;
       
    }
    public void SaveData ( GameData data){
        if(data.enemysDestroyed.ContainsKey(id)){
            data.enemysDestroyed.Remove(id);
        }
        data.enemysDestroyed.Add(id,enemyHealth);
        data.enemyAttributesData.currentHP =   enemyData.currentHP;
        data.enemyAttributesData.id = enemyData.id;

    }

}

