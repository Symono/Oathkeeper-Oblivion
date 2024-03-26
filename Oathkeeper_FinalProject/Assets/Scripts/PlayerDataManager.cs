using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    // Reference to the PlayerData object
    public PlayerData playerData;

    // Ensure that there's only one instance of PlayerDataManager
    private static PlayerDataManager instance;
    
    void Awake()
    {
        // If an instance of PlayerDataManager already exists, destroy this one
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        // Set this instance as the singleton
        instance = this;
        
        // Make this object persistent between scenes
        DontDestroyOnLoad(gameObject);
        
        // Initialize player data if not set
        if (playerData == null)
        {
            playerData = ScriptableObject.CreateInstance<PlayerData>();
        }
    }
}
