using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyBattleManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    [ContextMenu("Generate Guid")]
    private void GenerateGuid(){
        id = System.Guid.NewGuid().ToString();
    }

    public BattleManager battleManager;
    public EnemyPatrol enemyPatrol;
    public Transform playerTransform;
    public EnemyData enemyData;

    public int enemyHealth;

    private bool enemyfought ;


    private void Start() {

        if (!this.enemyfought){
            enemyData.currentHP = enemyData.maxHP;
        }
    
   }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FacePlayer();
            enemyPatrol.PauseMovement();
            this.enemyfought = true;
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
            enemyfought = true;
            enemyHealth = enemyData.currentHP;
            this.gameObject.SetActive(false);
        }
        
    }

 public void LoadData(GameData data){
        data.enemysBattled.TryGetValue(id, out enemyfought);
        if (enemyfought){
            this.gameObject.SetActive(false);
        }
        this.enemyData.currentHP = data.enemyAttributesData.currentHP;
        //this.enemyData.id = data.enemyAttributesData.id;
       
    }

    public void SaveData(GameData data)
    {
        if (data.enemysBattled.ContainsKey(id))
        {
            data.enemysBattled.Remove(id);
        }
        data.enemysBattled.Add(id, enemyfought);
        data.enemyAttributesData.currentHP = this.enemyData.currentHP;
        //data.enemyAttributesData.id = this.enemyData.id;
    }
}