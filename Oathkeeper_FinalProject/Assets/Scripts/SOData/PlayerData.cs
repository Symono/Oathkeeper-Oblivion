using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    public string playerName;
    public int level;
    public int experiencePoints;
    public int currentHP;
    public int maxHP;
    public int currentMana;
    public int maxMana;
    public int basicHitDamage;
    public int healAmount;
    public GameObject character;

    // Default constructor
    public PlayerData()
    {
        playerName = "DefaultName";
        level = 1;
        experiencePoints = 0;
        currentHP = 100;
        maxHP = 100;
        currentMana = 50;
        maxMana = 50;
        basicHitDamage = 10;
        healAmount = 20;
    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public void Heal(int amount, int cost)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
        currentMana -= cost;
    }
    public void GainXP(int pointsEarned)
{
    experiencePoints += pointsEarned;
    int levelThreshold = 100; 
    int levelsGained = experiencePoints / levelThreshold;

    if (levelsGained > 0)
    {
        // Increase the player's level
        level += levelsGained;

        // Increase the player's max health and max mana 
        maxHP += 10 * levelsGained; // Increase maxHP by 10 for each level gained
        maxMana += 5 * levelsGained; // Increase maxMana by 5 for each level gained 

        experiencePoints -= levelsGained * levelThreshold;
    }
    
}
// Method to initialize default values
    public void InitializeDefaultValues()
    {
        playerName = "DefaultName";
        level = 1;
        experiencePoints = 0;
        currentHP = 100;
        maxHP = 100;
        currentMana = 50;
        maxMana = 50;
        basicHitDamage = 10;
        healAmount = 20;
    }


}
