using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] GameObject g_Monster;
	int frame;

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
			if (frame % 800 == 0)
			{
				Instantiate(g_Monster);
			}
		}
    }

	public void StopSpawn()
	{
		g_bStopSpawn = true;
	}
}
