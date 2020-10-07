using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
	static int g_Score;

	public int GetScore()
	{
		return g_Score;
	}

	public void SetScore(int _Score)
	{
		g_Score = _Score;
	}

}
