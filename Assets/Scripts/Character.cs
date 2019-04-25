using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour 
{

	//Parameters

	public float HP; //Health
	public float currHP;

	public float MP; //Mana
	public float currMP;

	public int level; //Level

	public float XP; //Expirience
	public float currXP;

	public float jumpForce = 7000f; //Jump force
	public float speed = 5f; //Speed

	public bool isRightSide = true; //Which side character is directed
	public bool isLanded = false; //Is the character on the ground

	//Components

	//HUD
	public GameObject healthBar;
	public GameObject manaBar;
	public GameObject expirienceBar;
	public GameObject levelText;

	public Animator animator; //Animator

	public Rigidbody2D rb; //Rigid body

	public GameObject fbPrefab; //Fireball prefab

	void Start() 
	{
		rb = GetComponent<Rigidbody2D>();

		HP = 100;
		currHP = HP;

		MP = 100;
		currMP = MP;

		level = 1;

		XP = 100;
		currXP = 0;

	}

	void Update() 
	{
		if (currHP > 0) 
		{
			float moveX = Input.GetAxis("Horizontal");

			rb.MovePosition(rb.position + Vector2.right * moveX * speed * Time.deltaTime);

			animator.SetFloat("Speed", Math.Abs(moveX));
			animator.SetBool("IsAtacking", false);


			if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				Jump();
			}

			if((moveX > 0f && !isRightSide) || (moveX < 0f && isRightSide)) {
				if (moveX != 0f) 
				{
					Spin (moveX);
				}
			}

			if (transform.position.y < -10f) 
			{
				transform.position = new Vector3(0f, 0f, 0f);
			}

			if (Input.GetKeyDown(KeyCode.RightControl)) 
			{
				CastFireball();

			}

			UpdateHUD();

			if (currXP >= XP) 
			{
				LevelUp();
			}

		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Jump() //Jump method
	{ 
		if(isLanded) //Allow jump only if no ground
		{
			rb.AddForce(Vector2.up * jumpForce); //Adding jump force

			isLanded = false; 

			animator.SetBool("IsLanded", isLanded); //Changing animation

		}

	}


	void OnTriggerEnter2D(Collider2D obj) //If 
	{
		animator.SetBool("IsLanded", true);
		isLanded = true;
	}

	void UpdateHUD() //Drawing game HUD
	{
		if(currHP > HP) //Health overflow
		{
			currHP = HP;
		}

		if(currMP < MP) //Mana regeneration
		{
			currMP++;
		}


		if(currMP >= MP) //Mana overflow
		{
			currMP = MP;
		}

		float healthScale = currHP / HP; //Getting health bar scale
		healthBar.transform.localScale = new Vector3(healthScale, 0.1f, 1f);

		float manaScale = currMP / MP; //Getting mana bar scale
		manaBar.transform.localScale = new Vector3(manaScale, 0.1f, 1f);

		float expirienceScale = currXP / XP; //Getting expirience bar scale
		expirienceBar.transform.localScale = new Vector3(expirienceScale, 0.1f, 1f);

		levelText.GetComponent<Text>().text = level.ToString();
	}


	void CastFireball()
	{
		if(currMP >= 50) 
		{
			animator.SetBool("IsAtacking", true);
			currMP -= 50;
			float moveX = isRightSide ? 1f : -1f;

			currXP += 10;

			var fb = Instantiate(fbPrefab, new Vector3(rb.transform.position.x + (1f * moveX), rb.transform.position.y, 0f), Quaternion.identity);

			fb.GetComponent<FireBall>().moveX = moveX;
		}
	}

	void LevelUp()
	{
		level++;

		currXP = 0;
		XP += level * 10;

		HP += level * 10;
		currHP = HP;

		MP += level * 10;
		currMP = MP;
	}

	void Spin(float spinX)
	{
		isRightSide = !isRightSide;

		transform.localScale = new Vector3(transform.localScale.x * -1, 1f, 1f);
	}

	public void SaveCharacter()
	{
		SaveLoad.SaveGame(this);
	}

	public void LoadCharacter()
	{
		SaveData data = SaveLoad.LoadGame();

		if(!data.Equals(null)) 
		{
			HP = data.HP;
			currHP = data.currHP;

			MP = data.MP;
			currMP = data.currMP;

			XP = data.XP;
			currXP = data.currXP;

			level = data.level;
			currHP = data.currHP;

			transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
		}
	}
}
