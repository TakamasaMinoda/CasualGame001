﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
	public void Delete()
	{
		Destroy(this.gameObject);
	}
}
