using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public PlayerHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    public GameObject magicButton;
    public GameObject magicCanvas;

    public PlayerData player;
    public EnemyData enemy;

    public Movement characterMovement;

    public GameObject BattleCanvas;

    public GameObject winCanvas;
    public TextMeshProUGUI ExperienceText;
    public TextMeshProUGUI LevelText;

    private GameObject currentEnemy;

    public void StartBattle(GameObject enemyGameObject)
    {
       currentEnemy = enemyGameObject;
        EnemyBattleManager enemyManager = enemyGameObject.GetComponent<EnemyBattleManager>();      
         if (enemyManager != null)
        {
            enemy = enemyManager.enemyData; // Get EnemyData reference from EnemyBattleManager
            StartCoroutine(SetupBattle());
        }
        else
        {
            Debug.LogError("EnemyBattleManager component not found on the enemy GameObject.");
        }
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        dialogueText.text = "A Battle has started with " + enemy.EnemyName;

        playerHUD.SetHUD(player);
        enemyHUD.SetHUD(enemy);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemy.TakeDamage(player.basicHitDamage);
        enemyHUD.UpdateHealthBar(enemy.currentHP, enemy.maxHP);
        dialogueText.text = "The attack was successful";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }


    
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You WIN";
            
            BattleCanvas.SetActive(false);

            winCanvas.SetActive(true);
            
            characterMovement.enabled = true;

            player.GainXP(enemy.exp);

            ExperienceText.text = "You have gained " + enemy.exp +" experience points! ";

            LevelText.text = "Your current Level is " + player.level + " your current exp is " + player.experiencePoints;
                        
        }
        else
        {
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
        if (state != BattleState.PLAYERTURN)
            return;
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }

    IEnumerator PlayerHeal()
    {
        if (player.currentMana <= 0)
        {
            dialogueText.text = "You are out of Mana. Please use regular Attack or an Item!";
            magicCanvas.SetActive(false);
            magicButton.SetActive(true);
        }
        else
        {
            player.Heal(player.healAmount,10);
            playerHUD.UpdateHealthBar(player.currentHP, player.maxHP);
            playerHUD.UpdateManaBar(player.currentMana, player.maxMana);

            dialogueText.text = "You have been healed!";
            yield return new WaitForSeconds(2f);

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemy.EnemyName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = player.TakeDamage(enemy.basicHitDamage);

        playerHUD.UpdateHealthBar(player.currentHP, player.maxHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

}

