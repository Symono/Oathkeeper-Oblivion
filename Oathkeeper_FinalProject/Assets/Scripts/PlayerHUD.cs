using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;

    public Image healthBarFill;
    public Image manaBarFill;

   public void SetHUD(PlayerData unit)
    {
        nameText.text = unit.playerName;
        levelText.text = "Lvl " + unit.level;
        UpdateHealthBar(unit.currentHP, unit.maxHP);
        UpdateManaBar(unit.currentMana, unit.maxMana);

    }
    public void UpdateHealthBar(int currentHP, int maxHP)
    {
        float fillAmount = (float)currentHP / maxHP;
        healthBarFill.fillAmount = fillAmount;
    }
    public void UpdateManaBar(int currentMana, int maxMana)
    {
        float fillManaAmount= (float)currentMana / maxMana;
        manaBarFill.fillAmount = fillManaAmount;

    }
}
