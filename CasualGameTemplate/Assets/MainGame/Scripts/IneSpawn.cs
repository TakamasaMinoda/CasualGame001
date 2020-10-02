using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IneSpawn : MonoBehaviour
{

	public GameObject g_PrefabIne;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

	}

	public void CreateIne()
	{
		Instantiate(g_PrefabIne);
	}
}
