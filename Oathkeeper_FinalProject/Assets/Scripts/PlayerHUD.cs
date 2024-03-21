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

   public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
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
