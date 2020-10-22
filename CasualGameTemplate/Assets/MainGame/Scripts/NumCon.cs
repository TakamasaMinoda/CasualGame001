using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumCon : MonoBehaviour
{

	//表示位置
	Vector3 init_pos;

	//表示関連
	private int point;      //表示する値
	private float size = 1; //表示サイズ

	private static int dam_sort = 0;    //数字の表示順
	private const int SORT_MAX = 30000;

	public void Init(int point, Vector3 pos)
	{
		//必要な情報を格納
		this.point = point;

		init_pos = pos;

		//表示用のダメージを作る
		CreateNum(point);

		dam_sort++;
		if (dam_sort > SORT_MAX)
		{
			dam_sort = 0;
		}

	}

	//描画用の数字を作る
	private void CreateNum(int point)
	{

		//桁を割り出す
		int digit = ChkDigit(point);

		//数字プレハブを読み込む、テスト用のフォルダとファイル名
		GameObject obj = LoadGObject("test", "Number");


		//桁の分だけオブジェクトを作り登録していく
		for (int i = 0; i < digit; i++)
		{

			GameObject numObj = Instantiate(obj) as GameObject;

			//子供として登録
			numObj.transform.parent = transform;

			//現在チェックしている桁の数字を割り出す
			int digNum = GetPointDigit(point, i + 1);

			//ポイントから数字を切り替える
			numObj.GetComponent<NumberScore>().ChangeSprite(digNum);

			//サイズをゲットする
			float size_w = numObj.GetComponent<SpriteRenderer>().bounds.size.x;

			//位置をずらす
			float ajs_x = size_w * i - (size_w * digit) / 2;
			Vector3 pos = new Vector3(numObj.transform.position.x - ajs_x, numObj.transform.position.y, numObj.transform.position.z);
			numObj.transform.position = pos + init_pos;

			numObj = null;
		}

	}

	/**
    * 整数の桁数を返す
    * */
	public static int ChkDigit(int num)
	{

		//0の場合1桁として返す
		if (num == 0) return 1;

		//対数とやらを使って返す
		return (num == 0) ? 1 : ((int)Mathf.Log10(num) + 1);

	}
	/**
    * 数値の中から指定した桁の値をかえす
    * */
	public static int GetPointDigit(int num, int digit)
	{

		int res = 0;
		int pow_dig = (int)Mathf.Pow(10, digit);
		if (digit == 1)
		{
			res = num - (num / pow_dig) * pow_dig;
		}
		else
		{
			res = (num - (num / pow_dig) * pow_dig) / (int)Mathf.Pow(10, (digit - 1));
		}

		return res;
	}
	/**
    * オブジェクトを読み込む
    * リソースフォルダから読み込む
    * */
	public static GameObject LoadGObject(string dir_name, string filename)
	{

		GameObject obj;

		//リソースから読み込むパターン
		obj = (GameObject)Resources.Load(dir_name + "/" + filename);

		return obj;

	}
}
