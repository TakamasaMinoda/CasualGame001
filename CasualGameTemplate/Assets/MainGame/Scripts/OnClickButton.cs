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
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(100, 200, 350, 500);
				break;
			case 1:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(100, 300, 500, 650);
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
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(800, 1000, 1450, 2000);
				break;
			case 5:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(600, 1200, 1500, 2500);
				break;
			case 6:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(100, 500, 800, 1200);
				break;
			case 7:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(300, 800, 1200, 1350);
				break;
			case 8:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(500, 800, 1500, 2200);
				break;
			case 9:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(800, 1000, 1450, 2000);
				break;
			case 10:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(600, 1200, 1500, 2500);
				break;
			case 11:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(300, 800, 1200, 1350);
				break;
			case 12:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(500, 800, 1500, 2200);
				break;
			case 13:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(800, 1000, 1450, 2000);
				break;
			case 14:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(800, 1000, 1450, 2000);
				break;
			case 15:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(600, 1200, 1500, 2500);
				break;
			case 16:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(100, 500, 800, 1200);
				break;
			case 17:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(300, 800, 1200, 1350);
				break;
			case 18:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(500, 800, 1500, 2200);
				break;
			case 19:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(800, 1000, 1450, 2000);
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
				GameObject.Find("DataHolder").GetComponent<Data>().ResetNowStage();
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(100, 200, 350, 500);
				break;
			case 1:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(100, 300, 500, 650);
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
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(800, 1000, 1450, 2000);
				break;
			case 5:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(600, 1200, 1500, 2500);
				break;
			case 6:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(100, 500, 800, 1200);
				break;
			case 7:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(300, 800, 1200, 1350);
				break;
			case 8:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(500, 800, 1500, 2200);
				break;
			case 9:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(800, 1000, 1450, 2000);
				break;
			case 10:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(600, 1200, 1500, 2500);
				break;
			case 11:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(300, 800, 1200, 1350);
				break;
			case 12:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(500, 800, 1500, 2200);
				break;
			case 13:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(800, 1000, 1450, 2000);
				break;
			case 14:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(800, 1000, 1450, 2000);
				break;
			case 15:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(600, 1200, 1500, 2500);
				break;
			case 16:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(100, 500, 800, 1200);
				break;
			case 17:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(300, 800, 1200, 1350);
				break;
			case 18:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(500, 800, 1500, 2200);
				break;
			case 19:
				//ノルマの設定を行う //べたうちでしかスコア変更できない
				GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(800, 1000, 1450, 2000);
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

		GameObject.Find("DataHolder").GetComponent<Data>().SaveNowStage();

		SceneManager.LoadScene("Title");
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
