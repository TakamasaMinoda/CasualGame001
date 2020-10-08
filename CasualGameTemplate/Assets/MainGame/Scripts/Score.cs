using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	int g_Num = 0;
	[SerializeField]Text ScoreText;

	[SerializeField,Header("野菜の数")] int Vegi;


	private void Start()
	{
		ScoreText = this.gameObject.GetComponent<Text>();
		Vegi = 0;
		if (ScoreText)
		{
			ScoreText.text = "野菜の数 : " + Vegi.ToString("D2");
		}
	}

	public int GetScore()
	{
		return g_Num;
	}

	public void AddScore(int _Num)
	{
		g_Num += _Num;
		if (ScoreText)
		{ 
			ScoreText.text = "スコア : " + g_Num.ToString("D4") + "点";
		}
	}

	public void AddVegi()
	{
		Vegi++;
		if (ScoreText)
		{
			ScoreText.text = "野菜の数 : " + Vegi.ToString("D2");
		}
	}
}
