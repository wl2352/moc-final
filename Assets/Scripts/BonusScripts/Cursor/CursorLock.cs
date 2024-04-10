using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLock : MonoBehaviour 
{
	void Update ()
	{
		if (Input.GetKeyDown ("1"))
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		if (Input.GetKeyDown("2"))
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}
