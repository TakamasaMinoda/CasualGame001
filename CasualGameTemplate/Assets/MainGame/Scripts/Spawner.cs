using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject[] g_PrefabIne;
	float frame;
	[SerializeField,Header("リスポーン時間")]float RespornTime;

	private void Start()
	{
		frame = 0;
	}

	private void Update()
	{
		//稲と米がいない場合またはモンスターがいない場合
		if (GameObject.FindGameObjectsWithTag("Ine1").Length == 0)
		{
			//フレームカウント
			frame += Time.deltaTime;

			//3s後
			if (frame > RespornTime)
			{
				//オブジェクトプール用にする
				Instantiate(g_PrefabIne[0], this.transform.position, Quaternion.identity);

				frame = 0;
			}
		}
	}
}
