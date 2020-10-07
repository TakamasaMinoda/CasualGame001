using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ine : MonoBehaviour
{
	GameObject[] g_Rices;
	List<bool> g_isCutted = new List<bool>();
	int g_CutCount;

	bool StopScript;

	// Start is called before the first frame update
	void Start()
	{
		//自動でコメオブジェクトを入れる
		g_Rices = GameObject.FindGameObjectsWithTag("Rice");
		
		//要素分だけリストに追加する
		for (int i=0; i< g_Rices.Length;i++)
		{
			g_isCutted.Add(false);
		}

		//現在のカウントを0にする
		g_CutCount = 0;
	}

	private void Update()
	{
		for(int i = 0; i < g_Rices.Length; i++)
		{
			if(g_isCutted[i] == false)
			{
				return;
			}
		}

		//すべての米をカットしたら
		//オブジェクトフェードアウト
		if (!gameObject.GetComponent<FadeOut>())
		{
			gameObject.AddComponent<FadeOut>();
		}

		//米もフェードアウト
		GameObject[] Rices = GameObject.FindGameObjectsWithTag("Rice");
		foreach (GameObject a in Rices)
		{

			Rigidbody2D rb;
			if (rb = a.GetComponent<Rigidbody2D>())
			{
				//重力有効
				rb.gravityScale = 5.0f;
			}

			//米フェード
			if (!a.gameObject.GetComponent<FadeOut>())
			{
				a.gameObject.AddComponent<FadeOut>();
			}
		}

	}

	//カウント可算とboolをtrue
	public void SetCutted()
	{
		g_isCutted[g_CutCount] = true;
		g_CutCount++;
	}

}
