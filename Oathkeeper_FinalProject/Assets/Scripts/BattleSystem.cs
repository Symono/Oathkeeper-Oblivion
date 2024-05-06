using System.Collections;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    public TextMeshProUGUI dialogueText;
    public PlayerHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    public GameObject magicButton;
    public GameObject magicCanvas;

    public static BattleSystem instance;

    public PlayerData playerUnit;
    public EnemyData enemyUnit;

    private PlayerData playerData;
    private EnemyData enemyData;



    void Start()
{
    
    StartBattle(playerData, enemyData);
}

    public void StartBattle(PlayerData player, EnemyData enemy)
    {
        if (player == null)
        {
            Debug.Log("Player not found!");
            return;
        }
        if (enemy == null)
        {
            Debug.Log("Enemy not found!");
            return;
        }

        StartCoroutine(SetupBattle(player, enemy));
    }

    IEnumerator SetupBattle(PlayerData player, EnemyData enemy)
    {
         GameObject playerCharacter = Instantiate(player.character, playerBattleStation.position, Quaternion.identity);
        playerCharacter.transform.SetParent(playerBattleStation);

        GameObject enemyCharacter = Instantiate(enemy.character, enemyBattleStation.position, Quaternion.identity);
        enemyCharacter.transform.SetParent(enemyBattleStation);
            
        dialogueText.text = "A Battle has started with " + enemy.EnemyName;

        playerHUD.SetHUD(player);
        enemyHUD.SetHUD(enemy);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack(PlayerData player, EnemyData enemy)
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
            StartCoroutine(EnemyTurn(player, enemy));
        }
    }


    
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You WIN";

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
        StartCoroutine(PlayerAttack(playerUnit, enemyUnit));
    }

    public void OnMagicButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        // Implement magic button functionality
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal(playerUnit, enemyUnit));
    }

    IEnumerator PlayerHeal(PlayerData player, EnemyData enemy)
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
            StartCoroutine(EnemyTurn(player, enemy));
        }
    }

    IEnumerator EnemyTurn(PlayerData player, EnemyData enemy)
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
