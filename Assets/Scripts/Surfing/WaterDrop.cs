using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour {
	public float timeBetweenDrops = 1f;
	private float _dropTime;

	private bool _isDropReady = true;

	private Animator _anim;

	void Start() {
		_anim = GetComponent<Animator>();
	}

	void Update() {
		if (!_isDropReady) return;

		_dropTime -= Time.deltaTime;
		if (_dropTime > 0) return;

		_dropTime = timeBetweenDrops * Random.Range(0.9f, 1.1f);
		_anim.SetTrigger("Fall");
		_isDropReady = false;
	}

	public void FallingDone() {
		_isDropReady = true;
	}
}
