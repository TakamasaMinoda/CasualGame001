using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
	

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
	[SerializeField, Header("スコア")] static int g_NowScore;

	//種類ごとにも個数を入れる
	[SerializeField, Header("米の数")]static int[] g_IconNum= new int[4];

	//ノルマスコア
	[SerializeField, Header("ノルマスコアリスト")] static int[] g_NormaScore = new int[4];

	//プレイヤーが今いるステージの名前
	[SerializeField, Header("今のステージ")] static int g_NowStage=0; //最終的にplayerprefでクリアしたステージからやり直し可能にする
	[SerializeField, Header("最大のステージ数")] int g_MaxStage;

	//個数ゲット
	public int GetIconNum(int i)
	{
		return g_IconNum[i];
	}

	//個数を増やす
	public void AddRiceNum()
	{
		g_IconNum[0]++;
	}
	public void AddVegeNum()
	{
		g_IconNum[1]++;
	}
	public void AddFishNum()
	{
		g_IconNum[2]++;
	}
	public void AddMeatNum()
	{
		g_IconNum[3]++;
	}

	private void Awake()
	{
		//現在のステージのロード
		LoadNowStage();
		Debug.Log(g_NowStage);
	}

	public int GetMaxStage()
	{
		return g_MaxStage;
	}

	public void SaveNowStage()
	{
		PlayerPrefs.SetInt("現在のステージ", g_NowStage);
	}

	public int LoadNowStage()
	{
		g_NowStage = PlayerPrefs.GetInt("現在のステージ");
		return g_NowStage;
	}

	public int GetNowStage()
	{
		return g_NowStage;
	}

	public void SetNowStage()
	{
		g_NowStage++;
	}

	public void ResetNowStage()
	{
		g_NowStage = 0;
		SaveNowStage();
	}

	public void SetNormaScore(int _Norma01, int _Norma02, int _Norma03, int _Norma04)
	{
		g_NormaScore[0] = _Norma01;
		g_NormaScore[1] = _Norma02;
		g_NormaScore[2] = _Norma03;
		g_NormaScore[3] = _Norma04;
	}

	public int GetNormaSocre(int n)
	{
		return g_NormaScore[n];
	}

	public void SetScore()
	{
		g_NowScore = ScoreText.GetComponent<Score>().GetScore();
	}

	public int GetScore()
	{
		return g_NowScore;
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
