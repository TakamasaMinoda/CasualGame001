using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer2D
{
	public class RiceSliceEvent : MonoBehaviour
	{
		GameObject g_ScoreTexObj;
		[SerializeField, Header("米の番号")]  int g_RiceID; //バグIDがかわる
		[SerializeField, Header("現在の大きさ")] double currentSize = 0;
		[SerializeField, Header("現在の大きさのパーセンテージ")] float currentSizePercent = 0;

		void Start()
		{
			Slicer2D slicer = GetComponent<Slicer2D>();
			slicer.AddResultEvent(SliceEvent);

			//テキストオブジェクトの取得
			g_ScoreTexObj = GameObject.Find("ScoreText");

			//スプライトのa値を初期化
			if (GetComponent<SpriteRenderer>())
			{
				GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
			}

			//米の大きさを取得
			if (currentSize == 0)
			{
				currentSize = Polygon2DList.CreateFromGameObject(gameObject)[0].ToWorldSpace(transform).GetArea();
				currentSizePercent = (float)System.Math.Round((currentSize / GameObject.Find("DataHolder").GetComponent<Data>().GetOriginalRiceSize(g_RiceID)) * 100);
			}
		}

		//オブジェクトをスライスしたら
		void SliceEvent(Slice2D slice)
		{
			//親稲を取得
			GameObject OyaIne = transform.root.gameObject;
			OyaIne.GetComponent<Ine>().SetCutted();

			//スライスされた全オブジェクトを読み込み
			foreach (GameObject parts in slice.GetGameObjects())
			{
				//スライスを不可にする
				Slicer2D slicer = parts.GetComponent<Slicer2D>();
				slicer.enabled = false;
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.tag == "Basket")
			{
				//スコア追加
				//元の大きさと現在の大きさの％でスコアを決める % = 今の大きさ/元の大きさ*100
				float Ten = currentSizePercent * 10;

				//スコア追加
				g_ScoreTexObj.GetComponent<Score>().AddScore((int)Ten);
			}
		}
	}
}


