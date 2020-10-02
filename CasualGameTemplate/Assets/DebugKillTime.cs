using Slicer2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugKillTime : MonoBehaviour
{
	float KillTime=3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		KillTime -= Time.deltaTime;

		if (KillTime<=0.0f)
		{
			if(GameObject.Find("Ine(Clone)"))
			{
				
			}

			GameObject.Find("IneSpawner").GetComponent<IneSpawn>().CreateIne();
			KillTime = 3;
		}

		Text KillTimeText = GetComponent<Text>();

		KillTimeText.text = "判定時間 : " + KillTime.ToString("f1")+"秒";
    }

	public float GetKillTime()
	{
		return KillTime;
	}
}
