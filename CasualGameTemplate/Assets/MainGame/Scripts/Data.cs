using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
	[SerializeField, Header("米の数")]   static int g_RiceScore;
	[SerializeField, Header("野菜の数")] static int g_VegeScore;
	[SerializeField, Header("魚の数")]   static int g_FishScore;
	[SerializeField, Header("肉の数")]   static int g_MeatScore;

	[SerializeField, Header("米の大きさ")] double[] originalRiceSize = new double[3];
	[SerializeField, Header("亀の大きさ")] double OriginalTatleSize;

	[SerializeField, Header("米アイコン")] GameObject g_RiceIcon;
	[SerializeField, Header("野菜アイコン")] GameObject g_VegeIcon;

	public void SetRiceScore(int _RiceScore)
	{
		g_RiceScore = _RiceScore;
	}

	public void SetVegeScore(int _VegeScore)
	{
		g_VegeScore = _VegeScore;
	}

	public void SetFishScore(int _FishScore)
	{
		g_FishScore = _FishScore;
	}

	public void SetMeatScore(int _MeatScore)
	{
		g_MeatScore = _MeatScore;
	}

	public double GetOriginalRiceSize(int _ID)
	{
		return originalRiceSize[_ID];
	}

	public double GetOriginalTatleSize()
	{
		return OriginalTatleSize;
	}

	public GameObject GetRiceIcon()
	{
		return g_RiceIcon;
	}

	public GameObject GetVegeIcon()
	{
		return g_VegeIcon;
	}
}
