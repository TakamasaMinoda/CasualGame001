using System.Collections;
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

			//スライスされた全オブジェクトを読み込み
			foreach (GameObject parts in slice.GetGameObjects())
			{
				Rigidbody2D rb;
				if (rb = parts.GetComponent<Rigidbody2D>())
				{
					parts.GetComponent<PolygonCollider2D>().isTrigger = true;
					rb.freezeRotation = true;
					rb.gravityScale = 1.0f;
					rb.AddForce(new Vector2(Random.Range(-50, 50), 100));
				}

				//スライスを不可にする
				Slicer2D slicer = parts.GetComponent<Slicer2D>();
				slicer.enabled = false;

				//稲のスライスを不可にする
				GameObject[] Ines = GameObject.FindGameObjectsWithTag("Ine");
				foreach (GameObject a in Ines)
				{
					Slicer2D IneSlicer = a.GetComponent<Slicer2D>();
					IneSlicer.enabled = false;
					if (!a.gameObject.GetComponent<FadeOut>())
					{
						a.gameObject.AddComponent<FadeOut>();
					}
				}

				//オブジェクトフェードアウト
				slicer.gameObject.AddComponent<FadeOut>();
			}
		}
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.tag == "Basket")
			{
				//スコア追加
				g_ScoreTexObj.GetComponent<Score>().AddScore(500);
			}
		}
	}
}


