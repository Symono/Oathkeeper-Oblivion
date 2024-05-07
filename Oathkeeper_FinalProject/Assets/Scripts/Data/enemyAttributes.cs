using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class enemyAttributes 
{
    public string EnemyName;
    public int level;
    public int exp;
    public int currentHP;
    public int maxHP;
    public int basicHitDamage;
    public int healAmount;
    public string id;

    public enemyAttributes()
    {
        this.EnemyName = "enemyName";
        this.level = 1;
        this.exp = 50;
        this.currentHP = 25;
        this.maxHP = 25;
        this.id = System.Guid.NewGuid().ToString(); 
    }
}


