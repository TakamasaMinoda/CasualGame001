using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StartText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		Sequence seq = DOTween.Sequence()
		//(1,1,1)に移動
		.Append(
		DOTween.ToAlpha(() =>GetComponent<Text>().color, color => GetComponent<Text>().color=color,0,1.5f)
		).SetLoops(-1, LoopType.Yoyo);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
