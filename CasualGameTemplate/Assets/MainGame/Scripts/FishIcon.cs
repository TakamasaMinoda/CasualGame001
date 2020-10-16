﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using A_rosuko.ObjectPool;


public class FishIcon : MonoBehaviour
{
	[SerializeField, Header("フェードスピード")] private float g_ChangeSpeed = 1.5f;
	[SerializeField, Header("魚の点数")] int FishScore;
	private SpriteRenderer SpriteRender;
	private Tween g_Anim;

	//初期化
	void Start()
	{
		SpriteRender = this.gameObject.GetComponent<SpriteRenderer>();

		if (SpriteRender)
		{
			SpriteRender.color = new Color(1, 1, 1, 1);
			g_Anim = DOTween.ToAlpha(() => SpriteRender.color, color => SpriteRender.color = color, 0.0f, g_ChangeSpeed);
		}
	}

	//フェードが終わったら削除
	void Update()
	{
		g_Anim.OnComplete(() => End());
	}

	//削除関数
	void End()
	{
		GameObject.Find("DataHolder").GetComponent<Data>().AddFishNum();
		GameObject.Find("ScoreText").GetComponent<Score>().AddScore(FishScore);
		ObjectPool.Instance.SleepGameObject(gameObject);
	}
}
