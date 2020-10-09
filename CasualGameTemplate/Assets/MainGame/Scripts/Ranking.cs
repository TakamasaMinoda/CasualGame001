using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
	[SerializeField, Header("数値")]
	int point;

	//セーブ用のID名
	string[] ranking = { "1位", "2位", "3位" };

	//セーブする数値
	int[] rankingValue = new int[3];

	[SerializeField, Header("表示させるテキスト")]
	Text[] rankingText = new Text[3];

	bool g_NewRecord;
	[SerializeField, Header("NewRecordを知らせるテキスト")] GameObject RecordText;

	// Start is called before the first frame update
	void Start()
    {
		GetRanking();

		//point = GameObject.Find("DataHolder").GetComponent<Data>().GetScore();
		SetRanking(point);

		//ランキング描画
		for (int i = 0; i < rankingText.Length; i++)
		{
			rankingText[i].text = ranking[i] +" : "+ rankingValue[i].ToString("D4") + " 点 ";
		}

		if(g_NewRecord)
		{
			RecordText.SetActive(true);
		}
		else
		{
			RecordText.SetActive(false);
		}
	}

	void GetRanking()
	{
		//ランキング呼び出し
		for (int i = 0; i < ranking.Length; i++)
		{
			rankingValue[i] = PlayerPrefs.GetInt(ranking[i]);
		}
	}

	void SetRanking(int _value)
	{
		//書き込み用
		for (int i = 0; i < ranking.Length; i++)
		{
			//取得した値とRankingの値を比較して入れ替え
			if (_value > rankingValue[i])
			{
				if(i==0)
				{
					g_NewRecord = true;
				}
				var change = rankingValue[i];
				rankingValue[i] = _value;
				_value = change;
			}
		}

		//入れ替えた値を保存
		for (int i = 0; i < ranking.Length; i++)
		{
			PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
		}
	}
}
