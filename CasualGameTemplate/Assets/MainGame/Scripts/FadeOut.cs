using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[DisallowMultipleComponent]
public class FadeOut : MonoBehaviour
{
	MeshRenderer MeshRender;
	private float g_ChangeSpeed=1.5f;

	SpriteRenderer SpriteRender;

	Tween g_Anim;

	void Start()
	{
		MeshRender = this.gameObject.GetComponent<MeshRenderer>();
		SpriteRender = this.gameObject.GetComponent<SpriteRenderer>();

		if (MeshRender)
		{
			MeshRender.sharedMaterial.color = new Color(1, 1, 1, 1);
			g_Anim = DOTween.ToAlpha(() => MeshRender.sharedMaterial.color, color => MeshRender.sharedMaterial.color = color, 0.0f, g_ChangeSpeed);
		}
		if (SpriteRender)
		{
			SpriteRender.color = new Color(1, 1, 1, 1);
			g_Anim = DOTween.ToAlpha(() => SpriteRender.color, color => SpriteRender.color = color, 0.0f, g_ChangeSpeed);
		}
	}

	void Update()
	{
		g_Anim.OnComplete(() => Destroy(this.gameObject));
	}
}
