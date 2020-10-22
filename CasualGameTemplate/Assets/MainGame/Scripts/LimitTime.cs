using Slicer2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LimitTime : MonoBehaviour
{
	[SerializeField] float g_LimitTime = 30;
	float g_NowTime;
	Text LimitText;

	[SerializeField] NumCon NumCS;

	// Start is called before the first frame update
	void Start()
	{
		g_NowTime = g_LimitTime;
		LimitText = gameObject.GetComponent<Text>();
		
	}

	// Update is called once per frame
	void Update()
	{
		g_NowTime -= Time.deltaTime;

		if (g_NowTime >= 0)
		{
			LimitText.text = "あと" + g_NowTime.ToString("f0") + "秒";
		}
		else
		{
			//エネミー削除
			GameObject[] Enemys = GameObject.FindGameObjectsWithTag("Monster");
			foreach(GameObject enemy in Enemys)
			{
				Destroy(enemy);
			}

			//データホルダーにスコアを保存する
			GameObject.Find("DataHolder").GetComponent<Data>().SetScore();

			//フェードインスタート
			GameObject.Find("GameSystem").GetComponent<GameSystem>().StartFadeIn();
		}
	}

	public float GetLimitTime()
	{
		return g_LimitTime;
	}

	public float GetNowTime()
	{
		return g_NowTime;
	}

	public void SubLimitTime()
	{
		g_NowTime -= 5;
	}
}
