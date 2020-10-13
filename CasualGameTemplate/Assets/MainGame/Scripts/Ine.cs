using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ine : MonoBehaviour
{
	[SerializeField, Header("米が切られたかどうか")] int g_Count=0;
	[SerializeField, Header("米が切られたかどうか")] List<bool> g_isCutted = new List<bool>();
	[SerializeField, Header("切った数")] int g_CutCount;
	[SerializeField, Header("スクリプトを止める")] bool StopScript;

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

		//スポーン機能再開
		//マシーンを止める
		GameObject.Find("EnemySpawn_1").GetComponent<EnemySpawner>().StartSpawn();
	}

	private void Update()
	{
		//要素数分だけ切られているかどうかのチェックを行う
		for(int i=0;i<g_Count;i++)
		{
			if(g_isCutted[i] == false)
			{
				return;
			}
			else
			{
				//切られている米の添え字だからそのゲームオブジェクトを生成する感じにしたい
				//GameObject.Find("IneSpawn").GetComponent<Spawner>().CreateRice(i);
				//生成できたらかっとをfalseにする g_isCutted[i] = false; g_CutCount--;
			}
		}

		//稲フェードアウト
		if (!this.gameObject.GetComponent<FadeOut>())
		{
			this.gameObject.AddComponent<FadeOut>();
		}


		//米フェードアウト
		foreach (Transform child in transform)
		{
			if (!child.gameObject.GetComponent<FadeOut>())
			{
				child.gameObject.AddComponent<FadeOut>();
			}
		}

		//マシーンを止める
		if(GameObject.Find("EnemySpawn_1"))
		{
			GameObject.Find("EnemySpawn_1").GetComponent<EnemySpawner>().StopSpawn();
		}	
	}

	//カウント可算とboolをtrue
	public void SetCutted()
	{
		g_isCutted[g_CutCount] = true;
		g_CutCount++;
	}

}
