using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    public AudioClip mainMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = mainMenu;
        musicSource.Play();
    }

   
}
