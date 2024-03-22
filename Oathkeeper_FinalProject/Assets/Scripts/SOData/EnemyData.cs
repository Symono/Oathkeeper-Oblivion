using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    [SerializeField]
    public string EnemyName;
    public int level;
    public int currentHP;
    public int maxHP;
    public int basicHitDamage;
    public int healAmount;

    public GameObject character;
    
    
 public bool TakeDamage(int dmg)
	{
		currentHP -= dmg;

		if (currentHP <= 0)
			return true;
		else
			return false;
	}
}

