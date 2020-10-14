using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnClickButton : MonoBehaviour
{
	GameObject g_DataHolder;

	public void Retry()
	{
		switch (GameObject.Find("DataHolder").GetComponent<Data>().GetNowStage())
		{
			case 0:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(100, 500, 800, 1200);

				break;
			case 1:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(300, 800, 1200, 1350);

				break;
			case 2:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(500, 800, 1500, 2200);
				break;
			case 3:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(800, 1000, 1450, 2000);
				break;
			case 4:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(600, 1200, 1500, 2500);

				break;
			default:
				SceneManager.LoadScene("Title");
				break;
		}

		SceneManager.LoadScene("Main");
	}

	public void OnClickStart()
	{

		//今のステージ
		int NowStage = GameObject.Find("DataHolder").GetComponent<Data>().GetNowStage();

		//続きから
		switch (GameObject.Find("DataHolder").GetComponent<Data>().GetNowStage())
		{
			case 0:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().ResetNowStage();
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(100, 500, 800, 1200);
				//レベルデザインプログラムを入れる
				break;
			case 1:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(300, 800, 1200, 1350);
				break;
			case 2:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(500, 800, 1500, 2200);
				break;
			case 3:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(800, 1000, 1450, 2000);
				break;
			case 4:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(600, 1200, 1500, 2500);

				break;
			default:
				//最大ステージまでクリアした
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().ResetNowStage();
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(100, 500, 800, 1200);
				break;
		}

		SceneManager.LoadScene("Main");
	}

	public void OnLoadStage()
	{
		GameObject.Find("DataHolder").GetComponent<Data>().SetNowStage();
		Debug.Log(GameObject.Find("DataHolder").GetComponent<Data>().GetNowStage());

		switch (GameObject.Find("DataHolder").GetComponent<Data>().GetNowStage())
		{
			case 0:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(100, 500, 800, 1200);

				break;
			case 1:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(300, 800, 1200, 1350);

				break;
			case 2:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(500, 800, 1500, 2200);
				break;
			case 3:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(800, 1000, 1450, 2000);
				break;
			case 4:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(600, 1200, 1500, 2500);
				break;
		}

		GameObject.Find("DataHolder").GetComponent<Data>().SaveNowStage();

		if(GameObject.Find("DataHolder").GetComponent<Data>().GetNowStage()< GameObject.Find("DataHolder").GetComponent<Data>().GetMaxStage())
		{
			SceneManager.LoadScene("Main");
		}
		else
		{
			SceneManager.LoadScene("Title");
		}
		
	}

	public void Quit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;

#elif UNITY_STANDALONE
	  Application.runInBackground = false;
      UnityEngine.Application.Quit();
#endif
		Application.runInBackground = false;
		UnityEngine.Application.Quit();
	}
}
