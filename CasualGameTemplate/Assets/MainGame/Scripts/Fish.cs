﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Slicer2D
{
	public class Fish : MonoBehaviour
	{
		[SerializeField, Header("魚の部品の数")] int g_Count = 0;
		[SerializeField, Header("魚が切られたかどうか")] List<bool> g_isCutted = new List<bool>();
		[SerializeField, Header("切った数")] int g_CutCount;
		[SerializeField, Header("スクリプトを止める")] bool StopScript;

		[SerializeField, Header("頭をきったら")] bool SliceHead;

		[SerializeField, Header("煙エフェクト")] GameObject g_Effect;

		void Start()
		{
			//子オブジェクトの数分だけ回す
			foreach (Transform child in transform)
			{
				g_isCutted.Add(false);
				g_Count++;
			}
			//現在のカウントを0にする
			g_CutCount = 0;

			//スクリプトを止めるため変数の初期化
			StopScript = false;

			//エフェクト追加
			g_Effect.SetActive(false);
		}

		private void Update()
		{
			if (!SliceHead)
			{
				//要素数分だけ切られているかどうかのチェックを行う
				for (int i = 0; i < g_Count; i++)
				{
					if (g_isCutted[i] == false)
					{
						return;
					}
				}
			}


			//亀の動きを止める
			this.transform.parent.gameObject.GetComponent<EnemyMove>().StopMove();

			//子どもフェードアウト
			foreach (Transform childTransform in transform)
			{
				if (!this.gameObject.GetComponent<FadeOut>())
				{
					childTransform.gameObject.AddComponent<FadeOut>();
				}
			}

			//亀フェードアウト
			if (!this.gameObject.GetComponent<FadeOut>())
			{
				//エフェクト追加
				g_Effect.SetActive(true);

				this.gameObject.AddComponent<FadeOut>();
			}
		}

		//カウント可算とboolをtrue
		public void SetCutted()
		{
			g_isCutted[g_CutCount] = true;
			g_CutCount++;
		}

		public void CutHead()
		{
			SliceHead = true;
			foreach (Transform child in transform)
			{
				if (child.GetComponent<Slicer2D>())
				{
					child.GetComponent<Slicer2D>().enabled = false;
				}
			}
		}
	}
}
