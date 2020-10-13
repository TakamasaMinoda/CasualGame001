using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
	[SerializeField,Header("全ゲームオブジェクト")] GameObject g_GameObjects;
	[SerializeField] GameObject g_FadeSprite;
	[SerializeField] GameObject TimeUpObj;

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

		if(!TimeUpObj)
		{
			TimeUpObj =  GameObject.Find("TimeUp");
		}

		TimeUpObj.SetActive(false);

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
		g_GameObjects.SetActive(false);

		TimeUpObj.SetActive(true);

		//フェードイン
		DOTween.ToAlpha(() => g_FadeSprite.GetComponent<Image>().color, color => g_FadeSprite.GetComponent<Image>().color = color, 1f, 2f);

		yield return new WaitForSeconds(3);

		SceneManager.LoadScene("Result");

	}

	public void StartFadeIn()
	{
		StartCoroutine("End");
	}
}
