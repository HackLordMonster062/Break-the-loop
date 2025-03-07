using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
	public static T instance { get; private set; }
	protected virtual void Awake() {
		if (instance != null) {
			Destroy(gameObject);
			return;
		}

		instance = this as T;
		DontDestroyOnLoad(instance);
	}
}
