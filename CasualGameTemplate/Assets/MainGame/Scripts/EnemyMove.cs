﻿using System.Collections;
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
}