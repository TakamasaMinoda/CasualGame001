using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnClickButton : MonoBehaviour
{
	public void LoadMain()
	{
		SceneManager.LoadScene("Main");
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
