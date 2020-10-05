using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace Slicer2D
{
	public class EnemySliceEvent : MonoBehaviour
	{
		GameObject g_ScoreTexObj;

		void Start()
		{
			Slicer2D slicer = GetComponent<Slicer2D>();
			slicer.AddResultEvent(SliceEvent);
			g_ScoreTexObj = GameObject.Find("ScoreText");
		}

		//オブジェクトをスライスしたら
		void SliceEvent(Slice2D slice)
		{
			//スコア追加
			g_ScoreTexObj.GetComponent<Score>().AddScore(100);

			//スライスされた全オブジェクトを読み込み
			foreach (GameObject parts in slice.GetGameObjects())
			{
				//スケールアニメーション止めるよう
				gameObject.GetComponent<Enemy>().SetCutted();

				Rigidbody2D rb = parts.GetComponent<Rigidbody2D>();
				rb.AddForce(new Vector2(Random.Range(300, -300),100));

				//オブジェクトフェードアウト
				//parts.gameObject.AddComponent<FadeOut>();
			}
		}
	}
}
