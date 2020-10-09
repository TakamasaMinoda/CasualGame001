using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A_rosuko.ObjectPool;

public class DestroyEnemy : MonoBehaviour
{
	public void Delete()
	{
		Destroy(gameObject);
	}
}
