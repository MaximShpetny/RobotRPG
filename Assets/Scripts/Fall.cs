using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour 
{

	void OnTriggerEnter2D(Collider2D obj)
	{
		if(true)
		{
			obj.transform.position = new Vector3(0f, 0f, 0f);
		}
	}
}
