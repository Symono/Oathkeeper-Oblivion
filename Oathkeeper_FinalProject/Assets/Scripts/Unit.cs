using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
  public string unitName;
  public int unitLevel;
  public int damage;
  public int maxHP;
  public int currentHP;
   public int currentMana;
  public int maxMana;
  

public bool TakeDamage(int dmg)
	{
		currentHP -= dmg;

		if (currentHP <= 0)
			return true;
		else
			return false;
	}

	public void Heal(int amount,int cost)
	{
		currentHP += amount;
		if (currentHP > maxHP)
			currentHP = maxHP;
		currentMana -= cost;
	}
}