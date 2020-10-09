using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	Text g_ScoreText;

	[SerializeField, Header("米の数")] int g_RiceScore;
	[SerializeField,Header("野菜の数")] int g_VegeScore;
	[SerializeField, Header("魚の数")] int g_FishScore;
	[SerializeField, Header("肉の数")] int g_MeatScore;

	//初期化
	private void Start()
	{
		g_ScoreText = this.gameObject.GetComponent<Text>();
		g_RiceScore = 0;
		g_VegeScore = 0;
		g_FishScore = 0;
		g_MeatScore = 0;


		if (g_ScoreText)
		{
			g_ScoreText.text = 
				 "米の数 : " + g_RiceScore.ToString("D2") 
				+ "\n野菜の数 : " + g_VegeScore.ToString("D2")
				+ "\n魚の数 : " + g_FishScore.ToString("D2")
				+ "\n肉の数 : " + g_MeatScore.ToString("D2");
		}
	}

	//米のスコアを加算
	public void AddRiceScore()
	{
		g_RiceScore++;
		if (g_ScoreText)
		{
			//スコア更新
			g_ScoreText.text =
				 "米の数 : " + g_RiceScore.ToString("D2")
				+ "\n野菜の数 : " + g_VegeScore.ToString("D2")
				+ "\n魚の数 : " + g_FishScore.ToString("D2")
				+ "\n肉の数 : " + g_MeatScore.ToString("D2");
		}
	}

	//野菜のスコアを加算
	public void AddVegeScore()
	{
		g_VegeScore++;
		if (g_ScoreText)
		{
			//スコア更新
			g_ScoreText.text =
				 "米の数 : " + g_RiceScore.ToString("D2")
				+ "\n野菜の数 : " + g_VegeScore.ToString("D2")
				+ "\n魚の数 : " + g_FishScore.ToString("D2")
				+ "\n肉の数 : " + g_MeatScore.ToString("D2");
		}
	}

	//魚のスコアを加算
	public void AddFishScore()
	{
		g_FishScore++;
		if (g_ScoreText)
		{
			//スコア更新
			g_ScoreText.text =
				 "米の数 : " + g_RiceScore.ToString("D2")
				+ "\n野菜の数 : " + g_VegeScore.ToString("D2")
				+ "\n魚の数 : " + g_FishScore.ToString("D2")
				+ "\n肉の数 : " + g_MeatScore.ToString("D2");
		}
	}

	//肉のスコアを加算
	public void AddMeatScore()
	{
		g_MeatScore++;
		if (g_ScoreText)
		{
			//スコア更新
			g_ScoreText.text =
				 "\n米の数 : " + g_RiceScore.ToString("D2")
				+ "\n野菜の数 : " + g_VegeScore.ToString("D2")
				+ "\n魚の数 : " + g_FishScore.ToString("D2")
				+ "\n肉の数 : " + g_MeatScore.ToString("D2");
		}
	}

	public int GetVegeScore()
	{
		return g_VegeScore;
	}
	public int GetRiceScore()
	{
		return g_RiceScore;
	}
	public int GetFishScore()
	{
		return g_FishScore;
	}
	public int GetMeatScore()
	{
		return g_MeatScore;
	}
}
