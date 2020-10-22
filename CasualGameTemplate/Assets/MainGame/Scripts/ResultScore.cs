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

	[SerializeField, Header("個数テキスト")] Text[] g_IconText;
	// Start is called before the first frame update
	void Start()
	{
		g_ScoreText = GetComponent<Text>();
		g_NowScore = GameObject.Find("DataHolder").GetComponent<Data>().GetScore();

		g_ScoreText.text = g_NowScore.ToString();

		//個数描画
		for(int i=0;i<4;i++)
		{
			g_IconText[i].text = "×"+GameObject.Find("DataHolder").GetComponent<Data>().GetIconNum(i).ToString();
		}

		//星の数だけノルマクリアの判定をする
		for(int i=0;i<Stars.Length;i++)
		{
			int temp = GameObject.Find("DataHolder").GetComponent<Data>().GetNormaSocre(i);
			if(temp< g_NowScore)
			{
				Stars[i].GetComponent<Image>().color = new Color(1, 1, 1);
				StarsText[i].text = temp.ToString();
			}
			else
			{
				Stars[i].GetComponent<Image>().color = new Color(0,0,0);
				StarsText[i].text = temp.ToString();
			}
		}
	}
}
