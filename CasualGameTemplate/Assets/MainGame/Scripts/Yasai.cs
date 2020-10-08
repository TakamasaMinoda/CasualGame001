using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Yasai : MonoBehaviour
{
	private float g_ChangeSpeed = 1.5f;
	SpriteRenderer SpriteRender;

	Tween g_Anim;

	void Start()
	{
		SpriteRender = this.gameObject.GetComponent<SpriteRenderer>();

		if (SpriteRender)
		{
			SpriteRender.color = new Color(1, 1, 1, 1);
			g_Anim = DOTween.ToAlpha(() => SpriteRender.color, color => SpriteRender.color = color, 0.0f, g_ChangeSpeed);
		}
	}

	void Update()
	{
		g_Anim.OnComplete(() => End());
	}

	void End()
	{
		GameObject.Find("ScoreText").GetComponent<Score>().AddVegi();
		Destroy(this.gameObject);
	}
}
