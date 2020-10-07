﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer2D
{
	public class RiceSliceEvent : MonoBehaviour
	{
		GameObject g_ScoreTexObj;

		void Start()
		{
			Slicer2D slicer = GetComponent<Slicer2D>();
			slicer.AddResultEvent(SliceEvent);
			g_ScoreTexObj = GameObject.Find("ScoreText");
			if (GetComponent<SpriteRenderer>())
			{
				GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
			}
		}

		//オブジェクトをスライスしたら
		void SliceEvent(Slice2D slice)
		{
			//米かっとされたので稲プログラムのSetCutted起動
			GameObject Ine = GameObject.FindGameObjectWithTag("Ine");
			Ine.GetComponent<Ine>().SetCutted();

			//スライスされた全オブジェクトを読み込み
			foreach (GameObject parts in slice.GetGameObjects())
			{
				Rigidbody2D rb;
				if (rb = parts.GetComponent<Rigidbody2D>())
				{
					parts.GetComponent<PolygonCollider2D>().isTrigger = true;
					rb.freezeRotation = true;
					rb.gravityScale = 0.0f;
					//rb.AddForce(new Vector2(Random.Range(-5, 5), 10));
				}

				//スライスを不可にする
				Slicer2D slicer = parts.GetComponent<Slicer2D>();
				slicer.enabled = false;

				////稲のスライスを不可にする
				//GameObject[] Ines = GameObject.FindGameObjectsWithTag("Ine");
				//foreach (GameObject a in Ines)
				//{
				//	//Slicer2D IneSlicer = a.GetComponent<Slicer2D>();
				//	//IneSlicer.enabled = false;

				//	//二重アタッチを防ぐ
				//	if (!a.gameObject.GetComponent<FadeOut>())
				//	{
				//		a.gameObject.AddComponent<FadeOut>();
				//	}
				//}

				//オブジェクトフェードアウト
				//if (!slicer.gameObject.GetComponent<FadeOut>())
				//{
				//	slicer.gameObject.AddComponent<FadeOut>();
				//}
			}
		}
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.tag == "Basket")
			{
				//スコア追加
				g_ScoreTexObj.GetComponent<Score>().AddScore(300);
			}
		}
	}
}

