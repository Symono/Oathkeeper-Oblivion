using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public int level;
    public Vector3 playerPosition;
    public PlayerAttributes playerAttributesData;

    // The values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData() 
    {
        this.level = 0;
        playerPosition = Vector3.zero;
        playerAttributesData = new PlayerAttributes(); 
    }

    // Method to get the character name
    public string GetCharacterName()
    {
        return playerAttributesData.playerName;
    }
}
