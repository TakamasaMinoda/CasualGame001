using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberScore : MonoBehaviour
{
	[SerializeField] private Sprite[] sp = new Sprite[10];

	private void Start()
	{
		//NumberAnim();
	}

	public void ChangeSprite(int no)
	{

		if (no > 9 || no < 0) no = 0;

		SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = sp[no];

	}

	//void NumberAnim()
	//{
	//	Sequence anim = DOTween.Sequence()
	//				//.Append(transform.DOScale(new Vector3(0.3f, 0.3f, 1), 1.0f))
	//				.Append(transform.DOMove(new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, 0), 1))
	//				.Append(DOTween.ToAlpha(() => gameObject.GetComponent<SpriteRenderer>().color, color => gameObject.GetComponent<SpriteRenderer>().color = color, 0, 0.5f))
	//				.OnComplete(() =>
	//				{
	//					//GameObject.Find("ScoreText").GetComponent<Score>().AddScore(2000);
	//					Destroy(this.gameObject);
	//				});
	//}
}
