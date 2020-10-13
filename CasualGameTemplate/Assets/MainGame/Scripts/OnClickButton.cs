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
				SceneManager.LoadScene("Stage01");
				break;
			case 1:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(300, 800, 1200, 1350);
				SceneManager.LoadScene("Stage02");
				break;
			case 2:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(500, 800, 1500, 2200);
				SceneManager.LoadScene("Stage03");
				break;
			default:
				SceneManager.LoadScene("Title");
				break;
		}
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
				SceneManager.LoadScene("Stage01");
				break;
			case 1:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(300, 800, 1200, 1350);
				SceneManager.LoadScene("Stage02");
				break;
			case 2:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(500, 800, 1500, 2200);
				SceneManager.LoadScene("Stage03");
				break;
			default:
				//最大ステージまでクリアした
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().ResetNowStage();
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(100, 500, 800, 1200);
				SceneManager.LoadScene("Stage01");
				break;
		}
	}

	public void OnLoadStage()
	{
		GameObject.Find("DataHolder").GetComponent<Data>().SetNowStage();

		switch (GameObject.Find("DataHolder").GetComponent<Data>().GetNowStage())
		{
			case 0:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(100, 500, 800, 1200);
				SceneManager.LoadScene("Stage01");
				break;
			case 1:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(300, 800, 1200, 1350);
				SceneManager.LoadScene("Stage02");
				break;
			case 2:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(500, 800, 1500, 2200);
				SceneManager.LoadScene("Stage03");
				break;
			default:
				SceneManager.LoadScene("Title");
				break;
		}

		GameObject.Find("DataHolder").GetComponent<Data>().SaveNowStage(); GameObject.Find("DataHolder").GetComponent<Data>().SaveNowStage();
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
