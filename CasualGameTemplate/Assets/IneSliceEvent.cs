using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Slicer2D
{
	public class IneSliceEvent : MonoBehaviour
	{

		void Start()
		{
			Slicer2D slicer = GetComponent<Slicer2D>();
			slicer.AddResultEvent(SliceEvent);
		}

		//オブジェクトをスライスしたら
		void SliceEvent(Slice2D slice)
		{
			//スライスされた全オブジェクトを読み込み
			foreach (GameObject parts in slice.GetGameObjects())
			{
				DestroyIne(parts);
			}
		}

		public void DestroyIne(GameObject _obj)
		{
			Rigidbody2D rb = _obj.GetComponent<Rigidbody2D>();
			rb.AddForce(new Vector2(0, Random.Range(-100, -200)));

			//スライスを不可にする
			Slicer2D slicer = _obj.GetComponent<Slicer2D>();
			slicer.enabled = false;

			//オブジェクトフェードアウト
			slicer.gameObject.AddComponent<IneFadeOut>();
		}
	}
}
