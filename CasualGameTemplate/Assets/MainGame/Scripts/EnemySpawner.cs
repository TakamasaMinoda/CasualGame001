using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] GameObject[] g_Monster;
	int frame;

	//Inspectorに複数データを表示するためのクラス
	[System.SerializableAttribute]
	public class MonsterListList
	{
		public List<int> MonsterList = new List<int>();

		public MonsterListList(List<int> list)
		{
			MonsterList = list;
		}
	}

	//Inspectorに表示される
	[SerializeField]
	private List<MonsterListList> LevelList = new List<MonsterListList>();

	[SerializeField] int g_Level;

	[SerializeField] float[] RepopTime;

	[SerializeField] bool g_bStopSpawn;

	void Start()
    {
		frame = 0;
		g_bStopSpawn = false;

		g_Level = GameObject.Find("DataHolder").GetComponent<Data>().GetNowStage();
	}

    void FixedUpdate()
    {
		if (!g_bStopSpawn)
		{
			frame++;

			if (frame == RepopTime[g_Level])
			{
				int rand = Random.Range(0, LevelList[g_Level].MonsterList.Count);
				//オブジェクトプール用にする
				Instantiate(g_Monster[LevelList[g_Level].MonsterList[rand]], transform.position, Quaternion.identity);

				frame = 0;
			}
		}
    }
	public void StartSpawn()
	{
		g_bStopSpawn = false;
	}

	public void StopSpawn()
	{
		g_bStopSpawn = true;
	}
}
