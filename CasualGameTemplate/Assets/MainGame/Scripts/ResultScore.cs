using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
	[SerializeField, Header("スコアテキスト")] Text g_ScoreText;
	[SerializeField, Header("スコア")] int g_NowScore;
	[SerializeField, Header("星りすと")] GameObject[] Stars;
	[SerializeField, Header("星テキストりすと")] Text[] StarsText;


	// Start is called before the first frame update
	void Start()
	{
		g_ScoreText = GetComponent<Text>();
		g_NowScore = GameObject.Find("DataHolder").GetComponent<Data>().GetScore();

		g_ScoreText.text = "Total "+g_NowScore.ToString("D4");

		//星の数だけノルマクリアの判定をする
		for(int i=0;i<Stars.Length;i++)
		{
			int temp = GameObject.Find("DataHolder").GetComponent<Data>().GetNormaSocre(i);
			if(temp< g_NowScore)
			{
				Stars[i].SetActive(true);
				StarsText[i].text = temp.ToString("D4");
			}
			else
			{
				Stars[i].SetActive(false);
			}
		}
	}
}
