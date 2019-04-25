using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour 
{

	public Rigidbody2D rb;

	public int lifetime = 0;
	public int lifetimeMax = 1000;

	public bool isExploaded = false;

	public float moveX;
	public float speed = 15f;

	public AudioSource audioEnd;


	void Start() 
	{

		rb = GetComponent<Rigidbody2D>();

		if (moveX != 0) 
		{
			transform.localScale = new Vector3(transform.localScale.x * moveX, 1f, 1f);
		}



		//add sound
	}

	void Update() 
	{
		rb.MovePosition(rb.position + Vector2.right * moveX * speed * Time.deltaTime);

		lifetime++;

		if(lifetime >= lifetimeMax) 
		{
			Explode();
			lifetime = -1;
		}
	}

	void OnTriggerEnter2D(Collider2D obj)
	{
		if (obj.tag == "Player") 
		{
			obj.GetComponent<Character>().currHP -= 10;
		} 
		else if (obj.tag == "NPC") 
		{
			
		}


		Explode();
	}

	void Explode()
	{

		audioEnd.Play();

		isExploaded = true;

		Destroy(gameObject);

	}
}
