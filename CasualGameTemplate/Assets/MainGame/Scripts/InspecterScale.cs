using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Slicer2D
{
	public class InspecterScale : MonoBehaviour
	{
		[SerializeField, Header("現在の大きさ")]  double currentSize = 0;
		[SerializeField, Header("現在の大きさのパーセンテージ")]  float currentSizePercent = 0;

		void Start()
		{
			if (currentSize == 0)
			{
				currentSize = Polygon2DList.CreateFromGameObject(gameObject)[0].ToWorldSpace(transform).GetArea();
				currentSizePercent = (float)System.Math.Round((currentSize / GameObject.Find("DataHolder").GetComponent<Data>().GetOriginalBurdSize()) * 100);
			}
		}

		public double GetCurrentSize()
		{
			return currentSize;
		}

		public float GetcurrentSizePercent()
		{
			return currentSizePercent;
		}
	}
}