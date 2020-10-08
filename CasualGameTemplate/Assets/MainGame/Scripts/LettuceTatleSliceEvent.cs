﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer2D
{
	public class LettuceTatleSliceEvent : MonoBehaviour
	{
		[SerializeField, Header("スコアオブジェクト")] GameObject g_ScoreTexObj;

		[SerializeField, Header("現在の大きさ")] double currentSize = 0;
		[SerializeField, Header("現在の大きさのパーセンテージ")] float currentSizePercent = 0;

		[SerializeField, Header("食べ物の種類")] GameObject Type;

		void Start()
		{
			Slicer2D slicer = GetComponent<Slicer2D>();
			slicer.AddResultEvent(SliceEvent);
			g_ScoreTexObj = GameObject.Find("ScoreText");

			//モンスターの大きさを取得
			if (currentSize == 0)
			{
				currentSize = Polygon2DList.CreateFromGameObject(gameObject)[0].ToWorldSpace(transform).GetArea();
				Debug.Log(GameObject.Find("DataHolder").GetComponent<Data>().GetOriginalTatleSize());
				currentSizePercent = (float)System.Math.Round((currentSize / GameObject.Find("DataHolder").GetComponent<Data>().GetOriginalTatleSize()) * 100);
			}

			Type = GameObject.Find("DataHolder").GetComponent<Data>().GetVegiIcon();
		}

		//オブジェクトをスライスしたら
		void SliceEvent(Slice2D slice)
		{
			//親オブジェクトを取得
			GameObject OyaKame = transform.root.gameObject.transform.GetChild(0).gameObject;
			Debug.Log(OyaKame.name); // 親オブジェクト名を出力
			OyaKame.GetComponent<LettuceTatle>().SetCutted();

			//スライスされた全オブジェクトを読み込み
			foreach (GameObject parts in slice.GetGameObjects())
			{
				Rigidbody2D rb;
				if (rb = parts.GetComponent<Rigidbody2D>())
				{
					parts.GetComponent<PolygonCollider2D>().isTrigger = false;
					rb.freezeRotation = true;
					rb.gravityScale = 1.0f;
					rb.AddForce(new Vector2(Random.Range(-50, 50), 100));
				}
				//スライスを不可にする
				Slicer2D slicer = parts.GetComponent<Slicer2D>();
				slicer.enabled = false;
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			//稲に当たったら点数を減らす
			if (other.gameObject.tag == "Ine")
			{
				Destroy(this.gameObject);
			}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.tag == "Basket")
			{
				//スコア追加
				//元の大きさと現在の大きさの％でスコアを決める % = 今の大きさ/元の大きさ*100
				//float Ten = currentSizePercent * 10;
				//g_ScoreTexObj.GetComponent<Score>().AddScore((int)Ten);
				
				//アイコンの生成
				if (0 <= currentSizePercent && currentSizePercent < 30) //0～２９
				{
					Instantiate(Type, this.transform.position, Quaternion.identity);
				}
				else if(30 <= currentSizePercent && currentSizePercent < 70)
				{
					Instantiate(Type, this.transform.position, Quaternion.identity);
					Instantiate(Type, this.transform.position, Quaternion.identity);
				}
				else if (70 <= currentSizePercent && currentSizePercent < 100)
				{
					Instantiate(Type, this.transform.position, Quaternion.identity);
					Instantiate(Type, this.transform.position, Quaternion.identity);
					Instantiate(Type, this.transform.position, Quaternion.identity);
				}

				Destroy(this.gameObject);
			}
		}
	}
}