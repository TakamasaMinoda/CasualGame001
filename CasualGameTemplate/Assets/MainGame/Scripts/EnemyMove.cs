using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
	[SerializeField,Header("モンスターの歩く速さ")] private float g_WalkSpeed;

	private void Start()
	{
		GetComponent<Rigidbody2D>().AddForce(new Vector3(-g_WalkSpeed, 0, 0),ForceMode2D.Impulse);
	}

	public void StopMove()
	{
		GetComponent<Rigidbody2D>().Sleep();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//稲に触れた時、米をなくす
		if(collision.gameObject.tag=="Ine1" || collision.gameObject.tag == "Ine2")
		{
			if(collision.gameObject.transform.childCount!=0)
			{
				//米のカウントをあげる
				Destroy(collision.gameObject.transform.GetChild(0).gameObject);
				Destroy(this.gameObject);
			}
		}
	}
}
