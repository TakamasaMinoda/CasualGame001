using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer2D
{
	public class Enemy : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D collision)
		{
			//稲に当たったら点数を減らす
			if (collision.gameObject.tag == "Damage")
			{
				Destroy(this.gameObject);
			}
		}
	}
}
