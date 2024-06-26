using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public Vector2 playerPosition;
    public PlayerAttributes playerAttributesData;
    public enemyAttributes enemyAttributesData;


    public SerializableDictionary<string,bool> enemysBattled;

    // The values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData(string name,int sceneIndex) 
    {
        //playerPosition = Vector2.zero;
        playerAttributesData = new PlayerAttributes(); 
        playerAttributesData.playerName = name;
        playerAttributesData.sceneIndex = sceneIndex;
        //playerAttributesData.playerPosition = playerPosition;
        playerPosition = Vector2.zero;
        enemysBattled = new SerializableDictionary<string, bool>();
        enemyAttributesData = new enemyAttributes();
    }

    // Method to get the character name
    public string GetCharacterName()
    {
        return playerAttributesData.playerName;
    }
    public int GetSceneIndex(){
        return playerAttributesData.sceneIndex;
    }
}
