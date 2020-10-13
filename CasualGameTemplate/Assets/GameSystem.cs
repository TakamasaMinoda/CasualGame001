using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
	[SerializeField,Header("全ゲームオブジェクト")] GameObject g_GameObjects;
	[SerializeField] GameObject g_FadeSprite;

	private void Start()
	{
		if (!g_GameObjects)
		{
			g_GameObjects = GameObject.Find("GameObjects");
		}

		if(!g_FadeSprite)
		{
			g_FadeSprite = GameObject.Find("FadeSprite");
		}

		//フェードアウト
		g_FadeSprite.GetComponent<Image>().color = new Color(1, 1, 1, 1);
		DOTween.ToAlpha(() => g_FadeSprite.GetComponent<Image>().color,color => g_FadeSprite.GetComponent<Image>().color = color,0f,2f);

		StartCoroutine("Init");

	}

	IEnumerator Init()
	{
		//ここに処理を書く
		g_GameObjects.SetActive(false);

		//3s停止
		yield return new WaitForSeconds(3);

		//ここに再開後の処理を書く
		g_GameObjects.SetActive(true);

	}

	IEnumerator End()
	{
		yield return new WaitForSeconds(3);

		//フェードイン
		DOTween.ToAlpha(() => g_FadeSprite.GetComponent<Image>().color, color => g_FadeSprite.GetComponent<Image>().color = color, 1f, 2f);
	}
}
