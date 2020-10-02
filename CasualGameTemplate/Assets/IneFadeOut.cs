using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IneFadeOut : MonoBehaviour
{
	MeshRenderer MeshRenderer;

	void Start()
	{
		MeshRenderer = GetComponent<MeshRenderer>();
	}

	void Update()
	{
		Color color = MeshRenderer.sharedMaterial.color;
		
		if (color.a < 0.01f)
		{
			Destroy(this.gameObject);
		}

		MeshRenderer.sharedMaterial.color = Color.Lerp(color, new Color(1, 1, 1, 0), Time.deltaTime);

	}
}
