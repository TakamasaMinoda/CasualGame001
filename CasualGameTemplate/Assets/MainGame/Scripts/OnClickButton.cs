using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnClickButton : MonoBehaviour
{
	GameObject g_DataHolder;

	public void LoadMain()
	{
		SceneManager.LoadScene("Main_ver2");
	}

	public void OnLoadStage01()
	{
		//ノルマの設定を行う //べたうちでしかスコア変更できない
		GameObject.Find("DataHolder").GetComponent<Data>().SetNormaScore(100, 500, 1000, 2000);

		SceneManager.LoadScene("Main_ver2");
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
