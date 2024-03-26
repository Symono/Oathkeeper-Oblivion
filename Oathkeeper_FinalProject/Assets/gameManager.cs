using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject enemyObject;
    public GameObject player;

    private void Start()
    {
        // Ensure this GameObject persists between scenes
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(player);
    }

    public void EndBattleAndTransitionScene()
    {
        
         SceneManager.LoadScene("Start Game"); 
        // Destroy enemy object if it exists
        if (enemyObject != null)
        {
            Destroy(enemyObject);
        }

    }
}
