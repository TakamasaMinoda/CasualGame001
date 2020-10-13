using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer2D
{
	public class CowWing : MonoBehaviour
	{
		GameObject g_ScoreTexObj;
		[SerializeField, Header("現在の大きさ")] double currentSize = 0;
		[SerializeField, Header("現在の大きさのパーセンテージ")] float currentSizePercent = 0;

		[SerializeField, Header("食べ物の種類")] GameObject Type;

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

				//ここかえる
				currentSizePercent = (float)System.Math.Round((currentSize / GameObject.Find("DataHolder").GetComponent<Data>().GetOriginalCowHeadSize()) * 100);
			}

			Type = GameObject.Find("DataHolder").GetComponent<Data>().GetMeatIcon();
		}

		//オブジェクトをスライスしたら
		void SliceEvent(Slice2D slice)
		{
			//親稲を取得
			GameObject OyaCow = transform.root.gameObject.transform.GetChild(0).gameObject;
			OyaCow.GetComponent<Cow>().SetCutted();

			//スライスされた全オブジェクトを読み込み
			foreach (GameObject parts in slice.GetGameObjects())
			{
				Rigidbody2D rb;
				if (rb = parts.GetComponent<Rigidbody2D>())
				{
					parts.GetComponent<PolygonCollider2D>().isTrigger = false;
					rb.freezeRotation = true;
					rb.gravityScale = 1.0f;
					rb.AddForce(new Vector2(0, 100));
				}

				//スライスを不可にする
				Slicer2D slicer = parts.GetComponent<Slicer2D>();
				slicer.enabled = false;
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.tag == "Basket")
			{
				//アイコンの生成
				if (0 <= currentSizePercent && currentSizePercent < 70)
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

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.tag == "Basket")
			{
				//アイコンの生成
				if (0 <= currentSizePercent && currentSizePercent < 70)
				{
					Instantiate(Type, this.transform.position, Quaternion.identity);
				}
				else if (70 <= currentSizePercent && currentSizePercent < 100)
				{
					Instantiate(Type, this.transform.position, Quaternion.identity);
					Instantiate(Type, this.transform.position, Quaternion.identity);
				}

				Destroy(this.gameObject);
			}
		}
	}
}
