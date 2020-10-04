using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] GameObject g_Monster;
	int frame;

    void Start()
    {

	}

    void FixedUpdate()
    {
		frame++;
		if (frame%800==0)
		{
			Instantiate(g_Monster);
		}
    }
}
