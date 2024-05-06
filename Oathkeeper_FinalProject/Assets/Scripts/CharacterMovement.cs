using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Movement : MonoBehaviour, IDataPersistence
{
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 10f;

    Rigidbody2D rb;
    private Animator anim;
    private bool grounded;    
    public bool canMove;
    public PlayerData playerData;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
    }

    public void MovePlayer(float horizontalInput)
    {
        Vector2 movement = new Vector2(horizontalInput * speed, rb.velocity.y);
        rb.velocity = movement;

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
            SceneManager.LoadScene("Main Menu");
        }
        if (collision.gameObject.CompareTag("Finish")){
            SceneManager.LoadScene("Main Menu");
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
        
    }
    public void SaveData(GameData data){
        data.playerPosition = this.transform.position;
        data.playerAttributesData.playerName =playerData.playerName;
        data.playerAttributesData.level = playerData.level;
        data.playerAttributesData.experiencePoints  =   playerData.experiencePoints;
        data.playerAttributesData.currentHP =   playerData.currentHP;
        data.playerAttributesData.maxHP =   playerData.maxHP;
        data.playerAttributesData.currentMana = playerData.currentMana;
        data.playerAttributesData.maxMana = playerData.maxMana;
        data.playerAttributesData.basicHitDamage =  playerData.basicHitDamage;
        data.playerAttributesData.healAmount =  playerData.healAmount;
    }
}
