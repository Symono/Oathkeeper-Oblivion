using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    Unit playerUnit;
    Unit enemyUnit;
    public TextMeshProUGUI dialogueText;
    public PlayerHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    public GameObject magicButton;
    public GameObject magicCanvas;


    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());

    }
    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerGO.GetComponent<Unit>();

		GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "A Battle has started with " + enemyUnit.unitName;

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        //Damage the enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        enemyHUD.UpdateHealthBar(enemyUnit.currentHP,enemyUnit.maxHP);
        dialogueText.text = "The attack was successful";
        
        yield return new WaitForSeconds(2f);
        //check if the enemy is dead
        if(isDead)
        {
            state = BattleState.WON;
            EndBattle();
            
        }else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        
        //change state based on what happened
    }

    void EndBattle(){
        if(state == BattleState.WON)
        {
            dialogueText.text= "You WIN";
        }
        else{
            dialogueText.text = "You LOSE";
        }
    }
    void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";

    }
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack());
    }
    public void OnMagicButton()
    {
        if (state !=BattleState.PLAYERTURN)
            return;
    }
    public void OnHealButton()
    {
        if (state !=BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerHeal());

    }
    IEnumerator PlayerHeal()
    {
        
        if(playerUnit.currentMana <= 0){
            dialogueText.text = "You are out of Mana please use regular Attack or an Item!";
            //go back
            // Deactivate the magic canvas
            magicCanvas.SetActive(false);
            // Reactivate the regular action buttons
            magicButton.SetActive(true);
        }
        else
        {
            // Heal the player
            playerUnit.Heal(10,15);

            // Update player's health bar UI
            playerHUD.UpdateHealthBar(playerUnit.currentHP, playerUnit.maxHP);
            playerHUD.UpdateManaBar(playerUnit.currentMana,playerUnit.maxMana);
        
            // Display some feedback
            dialogueText.text = "You have been healed!";
            yield return new WaitForSeconds(2f);

            // End player's turn
            state = BattleState.ENEMYTURN;
            // Proceed to enemy's turn
            StartCoroutine(EnemyTurn());
        }
       
    }

    IEnumerator EnemyTurn()
	{
		dialogueText.text = enemyUnit.unitName + " attacks!";

		yield return new WaitForSeconds(1f);

		bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

		playerHUD.UpdateHealthBar(playerUnit.currentHP,playerUnit.maxHP);

		yield return new WaitForSeconds(1f);

		if(isDead)
		{
			state = BattleState.LOST;
			EndBattle();
		} else
		{
			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}

	}
    
}
