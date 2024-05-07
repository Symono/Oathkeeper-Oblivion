using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerAttributes 
{
  public string playerName;
    public int level;
    public int experiencePoints;
    public int currentHP;
    public int maxHP;
    public int currentMana;
    public int maxMana;
    public int basicHitDamage;
    public int healAmount;
    public int sceneIndex;
    public Vector2 playerPosition;
    public PlayerAttributes()
    {
        this.playerName = "DefaultName";
        this.level = 1;
        this.experiencePoints = 0;
        this.currentHP = 100;
        this.maxHP = 100;
        this.currentMana = 50;
        this.maxMana = 50;
        this.basicHitDamage = 10;
        this.healAmount = 20;
        this.sceneIndex = 1;
        this.playerPosition = Vector2.zero;
    }
}
