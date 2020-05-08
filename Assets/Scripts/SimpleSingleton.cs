using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SimpleSingleton<T> : MonoBehaviour where T : MonoBehaviour {
	private static T _instance;
	public static T Instance{
		get{
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<T> ();
				if (_instance == null) {
					_instance = new GameObject (typeof(T).ToString ()).AddComponent<T> ();
				}
			}

			return _instance;
		}
	}
}
