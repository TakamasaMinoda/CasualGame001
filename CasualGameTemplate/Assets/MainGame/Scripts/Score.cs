using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

	//テキスト
	[SerializeField,Header("スコアテキスト")]Text g_ScoreText;

	//スコア変数
	[SerializeField, Header("点数")] int g_NowScore;

	//初期化
	private void Start()
	{
		g_ScoreText = GetComponent<Text>();
		g_NowScore = 0;
		g_ScoreText.text = "Score:" + g_NowScore.ToString("D4");

		//仮のノルマスコア設定
		GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(100, 500, 1000, 2000);
	}

	public int GetScore()
	{
		return g_NowScore;
	}

	public void AddScore(int _Add)
	{
		g_NowScore += _Add;
		g_ScoreText.text = "Score:" + g_NowScore.ToString("D4");
	}
}
