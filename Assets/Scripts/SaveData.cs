using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
	public float currHP;
	public float HP;

	public float currMP;
	public float MP;

	public float currXP;
	public float XP;

	public int level;

	public float[] position;

	public SaveData(Character character)
	{

		HP = character.HP;
		currHP = character.currHP;

		MP = character.MP;
		currMP = character.currMP;

		XP = character.XP;
		currXP = character.currXP;

		level = character.level;

		position = new float[3] 
		{
			character.transform.position.x,
			character.transform.position.y,
			character.transform.position.z
		};
	}

}
