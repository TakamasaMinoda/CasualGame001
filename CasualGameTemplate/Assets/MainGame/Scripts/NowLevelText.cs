using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NowLevelText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		int Level = GameObject.Find("DataHolder").GetComponent<Data>().GetNowStage();
		if(Level==0)
		{
			gameObject.GetComponent<Text>().text = "はじめから";
		}
		else if(Level > GameObject.Find("DataHolder").GetComponent<Data>().GetMaxStage())
		{
			gameObject.GetComponent<Text>().text = "もう一回はじめから";
		}
		else
		{
			gameObject.GetComponent<Text>().text = "今のレベル:" + Level.ToString();
		}
	}
}
