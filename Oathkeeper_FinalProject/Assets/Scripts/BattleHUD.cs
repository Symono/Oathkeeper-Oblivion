using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;

    public Image healthBarFill;
   public void SetHUD(EnemyData unit)
    {
        nameText.text = unit.EnemyName;
        levelText.text = "Lvl " + unit.level;
        UpdateHealthBar(unit.currentHP, unit.maxHP);

    }
    public void UpdateHealthBar(int currentHP, int maxHP)
    {
        float fillAmount = (float)currentHP / maxHP;
        healthBarFill.fillAmount = fillAmount;
    }
}
