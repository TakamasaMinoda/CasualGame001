using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class smoke : MonoBehaviour
{
	SpriteRenderer SpriteRender;
	Tween g_Anim;
	void Start()
    {
		SpriteRender = this.gameObject.GetComponent<SpriteRenderer>();


		g_Anim = transform.DOScale(new Vector3(1.5f, 1.5f), 0.5f);
	
		//seq.Join(
		//DOTween.ToAlpha(() => SpriteRender.color, color => SpriteRender.color = color, 0.5f, 1.5f)

		//);
		//seq.Append(
		//DOTween.ToAlpha(() => SpriteRender.color, color => SpriteRender.color = color, 0.0f, 1.5f)
		//);
	}

    // Update is called once per frame
    void Update()
    {
		g_Anim.OnComplete(() => DOTween.ToAlpha(() => SpriteRender.color, color => SpriteRender.color = color, 0.0f, 1.0f));
	}
}
