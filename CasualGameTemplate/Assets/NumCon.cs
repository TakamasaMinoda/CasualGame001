using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumCon : MonoBehaviour
{
	[SerializeField] private Sprite[] sp = new Sprite[10];

	public void ChangeSprite(int no)
	{

		if (no > 9 || no < 0) no = 0;

		SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = sp[no];

	}
}
