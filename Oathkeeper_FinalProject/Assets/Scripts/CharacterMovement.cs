using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class Movement : MonoBehaviour, IDataPersistence
{
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 10f;

    Rigidbody2D rb;
    private Animator anim;
    private bool grounded;    
    public bool canMove;
    public PlayerData playerData;

    public Vector2 globalPosition;

    public GameObject winnerCanvas;

    public AudioSource music;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //music.Play();   

    }

    void FixedUpdate()
    {
        if (canMove) // Proceed with movement only if not in battle
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            MovePlayer(horizontalInput);
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && grounded) // Check if grounded and not already jumping
            {
                Jump();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)){
            DataPersistenceManager.instance.SaveGame();
            int sceneIndex = DataPersistenceManager.instance.GetIndex();
            SceneManager.LoadSceneAsync(0);
        }
    }

    public void MovePlayer(float horizontalInput)
    {
        Vector2 movement = new Vector2(horizontalInput * speed, rb.velocity.y);
        rb.velocity = movement;
        // Update player position
        //playerData.playerPosition = this.transform.position;

        // Flip player left and right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Set animator parameters
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", grounded);
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        anim.SetTrigger("Jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
        if ( collision.gameObject.CompareTag("DeadZone")){
            // take damage
            playerData.currentHP -= 5; 
            Debug.Log("You took damage");
        }
       
            if (collision.gameObject.CompareTag("Finish"))
        {
            playerData.sceneIndex += 1;

            DataPersistenceManager.instance.SaveGame();
            int sceneIndex = DataPersistenceManager.instance.GetIndex();


            if (sceneIndex > 3) // Check if the scene index is 3
            {
                // Activate the winner canvas
                winnerCanvas.SetActive(true);
            }
            else
            {
                SceneManager.LoadSceneAsync(sceneIndex);
                globalPosition = Vector2.zero;
            }
        }
            if(collision.gameObject.CompareTag("End"))
            {
                winnerCanvas.SetActive(true);
            }
        
        }
        
    

    public bool IsGrounded()
    {
        return grounded;
    }
    public void LoadData(GameData data){
        
        this.transform.position = data.playerPosition;
        playerData.playerName = data.playerAttributesData.playerName;
        playerData.level = data.playerAttributesData.level;
        playerData.experiencePoints = data.playerAttributesData.experiencePoints;  
        playerData.currentHP = data.playerAttributesData.currentHP;
        playerData.maxHP = data.playerAttributesData.maxHP;
        playerData.currentMana = data.playerAttributesData.currentMana;
        playerData.maxMana = data.playerAttributesData.maxMana;
        playerData.basicHitDamage = data.playerAttributesData.basicHitDamage;
        playerData.healAmount = data.playerAttributesData.healAmount;
        playerData.sceneIndex = data.playerAttributesData.sceneIndex;
        
    }
    public void SaveData(GameData data){
         // If the player reaches the finish line, set the position to Vector2.zero
    if (SceneManager.GetActiveScene().buildIndex != playerData.sceneIndex)
    {
        data.playerPosition = Vector2.zero;
    }
    else
    {
        // Otherwise, save the current player position
        data.playerPosition = transform.position;
    }
        globalPosition = data.playerPosition;
        data.playerAttributesData.playerName =playerData.playerName;
        data.playerAttributesData.level = playerData.level;
        data.playerAttributesData.experiencePoints  =   playerData.experiencePoints;
        data.playerAttributesData.currentHP =   playerData.currentHP;
        data.playerAttributesData.maxHP =   playerData.maxHP;
        data.playerAttributesData.currentMana = playerData.currentMana;
        data.playerAttributesData.maxMana = playerData.maxMana;
        data.playerAttributesData.basicHitDamage =  playerData.basicHitDamage;
        data.playerAttributesData.healAmount =  playerData.healAmount;
        data.playerAttributesData.sceneIndex = playerData.sceneIndex;
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0); // Load the main menu scene
    }
}