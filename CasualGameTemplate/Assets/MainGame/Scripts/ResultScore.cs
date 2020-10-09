using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
	[SerializeField, Header("米の数")] int g_RiceScore;
	[SerializeField, Header("野菜の数")] int g_VegeScore;
	[SerializeField, Header("魚の数")] int g_FishScore;
	[SerializeField, Header("肉の数")] int g_MeatScore;

	// Start is called before the first frame update
	void Start()
	{
		g_RiceScore = GameObject.Find("DataHolder").GetComponent<Data>().GetRiceScore();
		g_VegeScore = GameObject.Find("DataHolder").GetComponent<Data>().GetVegeScore();
		g_FishScore = GameObject.Find("DataHolder").GetComponent<Data>().GetFishScore();
		g_MeatScore = GameObject.Find("DataHolder").GetComponent<Data>().GetMeatScore();

		gameObject.GetComponent<Text>().text =  
			"米の数 : " + g_RiceScore.ToString("D2") 
		+"\n野菜の数 : " + g_VegeScore.ToString("D2")
		+ "\n魚の数 : " + g_FishScore.ToString("D2")
		+ "\n肉の数 : " + g_MeatScore.ToString("D2");

	}
}
