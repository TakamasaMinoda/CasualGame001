using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer2D
{
	public class Enemy : MonoBehaviour
	{
		[SerializeField] float g_MaxScale;
		[SerializeField] float AnimSpeed;
		bool g_Cutted;
		// Start is called before the first frame update
		void Start()
		{
			g_Cutted = false;
		}

		private void Update()
		{
			//this.transform.position -= new Vector3(0, Time.deltaTime * AnimSpeed);
		}

		public void SetCutted()
		{
			g_Cutted = true;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.tag == "Basket")
			{
				Destroy(this.gameObject);
			}
		}
	}
}
