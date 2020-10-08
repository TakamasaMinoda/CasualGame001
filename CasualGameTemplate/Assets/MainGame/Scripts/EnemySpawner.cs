using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] GameObject[] g_Monster;
	int frame;

	[SerializeField] int[] MonsterList;
	[SerializeField] float[] RepopTime;

	bool g_bStopSpawn;

	void Start()
    {
		frame = 0;
		g_bStopSpawn = false;
	}

    void FixedUpdate()
    {
		if (!g_bStopSpawn)
		{
			frame++;
			for(int i=0; i<RepopTime.Length;i++)
			{
				if(frame== RepopTime[i])
				{
					//オブジェクトプール用にする
					Instantiate(g_Monster[MonsterList[i]],transform.position, Quaternion.identity);
				}
			}
		}
    }

	public void StopSpawn()
	{
		g_bStopSpawn = true;
	}
}
