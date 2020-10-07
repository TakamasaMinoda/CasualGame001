using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
	static int g_Score;

	[SerializeField,Header("鳥の大きさ")]  double originalBurdSize = 0;
	[SerializeField, Header("米の大きさ")]  double[] originalRiceSize = new double[3];

	public int GetScore()
	{
		return g_Score;
	}

	public void SetScore(int _Score)
	{
		g_Score = _Score;
	}

	public double GetOriginalBurdSize()
	{
		return originalBurdSize;
	}


	public double GetOriginalRiceSize(int _ID)
	{
		return originalRiceSize[_ID];
	}
}
