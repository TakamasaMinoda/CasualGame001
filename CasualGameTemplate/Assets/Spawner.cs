using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject[] g_PrefabIne;
	public GameObject[] g_PrefabRice;
	public GameObject[] g_PrefabMonster;

	int g_NowPaterrn;

	bool g_bStopSpawn;

	private void Start()
	{
		g_bStopSpawn = false;
	}

	private void Update()
	{
		if (!g_bStopSpawn)
		{
			//稲と米がいない場合またはモンスターがいない場合
			if (GameObject.FindGameObjectsWithTag("Ine").Length == 0 
				&& GameObject.FindGameObjectsWithTag("Rice").Length == 0
				&& GameObject.FindGameObjectsWithTag("Monster").Length == 0)
			{
				//乱数で種類を決定
				g_NowPaterrn = Random.Range((int)0, (int)2);

				switch (g_NowPaterrn)
				{
					case 0:
						Instantiate(g_PrefabIne[0]);

						Instantiate(g_PrefabRice[0]);
						Instantiate(g_PrefabRice[1]);
						Instantiate(g_PrefabRice[2]);
						break;

					case 1:
						Instantiate(g_PrefabMonster[0]);
						break;
				}

			}
		}
	}

	public void StopSpawn()
	{
		g_bStopSpawn = true;
	}
}
