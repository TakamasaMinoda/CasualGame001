using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
	MeshRenderer MeshRenderer;
	private float g_ChangeSpeed=0.5f;

	void Start()
	{
		MeshRenderer = GetComponent<MeshRenderer>();
	}

	void Update()
	{
		Color color = MeshRenderer.sharedMaterial.color;
		
		if (color.a < 0.01f)
		{
			Debug.Log("FadeによってDestroy");
			Destroy(this.gameObject);
		}

		MeshRenderer.sharedMaterial.color = Color.Lerp(color, new Color(1, 1, 1, 0), Time.deltaTime* g_ChangeSpeed);

	}
}
