using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorHandler : MonoBehaviour
{
   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            //DataPersistenceManager.instance.SaveGame();
            int sceneIndex = DataPersistenceManager.instance.GetIndex();
            SceneManager.LoadSceneAsync(sceneIndex);
            
            
        }
        
    }

}
