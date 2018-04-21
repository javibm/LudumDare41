using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T Instance
	{
		get
		{
			if(_instance == null)
			{
				Debug.LogWarning("Instancia de Singleton no inicializada: " + typeof(T).ToString());
			}
			return _instance;
		}
	}

	protected void Awake()
	{
		if(_instance == null)
		{
			_instance = this as T;
		}
	}
	private static T _instance = null;
}
