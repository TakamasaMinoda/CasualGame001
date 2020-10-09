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

	[SerializeField, Header("魚のしっぽの大きさ")] double OriginalFishTailSize;
	[SerializeField, Header("魚のあたまの大きさ")] double OriginalFishHeadSize;

	[SerializeField, Header("牛のしっぽの大きさ")] double OriginalCowTailSize;
	[SerializeField, Header("牛のあたまの大きさ")] double OriginalCowHeadSize;

	[SerializeField, Header("米アイコン")] GameObject g_RiceIcon;
	[SerializeField, Header("野菜アイコン")] GameObject g_VegeIcon;
	[SerializeField, Header("魚アイコン")] GameObject g_FishIcon;
	[SerializeField, Header("肉アイコン")] GameObject g_MeatIcon;

	[SerializeField, Header("スコアテキスト")] GameObject ScoreText;



	public void SetScore()
	{
		SetRiceScore(ScoreText.GetComponent<Score>().GetRiceScore());
		SetVegeScore(ScoreText.GetComponent<Score>().GetVegeScore());
		SetFishScore(ScoreText.GetComponent<Score>().GetFishScore());
		SetMeatScore(ScoreText.GetComponent<Score>().GetMeatScore());
	}

	public void SetRiceScore(int _RiceScore)
	{
		g_RiceScore = _RiceScore;
	}
	
	public int GetRiceScore()
	{
		return g_RiceScore;
	}
	public int GetVegeScore()
	{
		return g_VegeScore;
	}
	public int GetFishScore()
	{
		return g_FishScore;
	}
	public int GetMeatScore()
	{
		return g_MeatScore;
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

	public double GetOriginalFishTailSize()
	{
		return OriginalFishTailSize;
	}

	public double GetOriginalFishHeadSize()
	{
		return OriginalFishHeadSize;
	}

	public double GetOriginalCowTailSize()
	{
		return OriginalCowTailSize;
	}

	public double GetOriginalCowHeadSize()
	{
		return OriginalCowHeadSize;
	}

	public GameObject GetRiceIcon()
	{
		return g_RiceIcon;
	}

	public GameObject GetVegeIcon()
	{
		return g_VegeIcon;
	}

	public GameObject GetFishIcon()
	{
		return g_FishIcon;
	}

	public GameObject GetMeatIcon()
	{
		return g_MeatIcon;
	}
}
