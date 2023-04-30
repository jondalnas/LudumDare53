using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
	private Vector2 _move, _pos;
	public float playerSpeed = 144;
	public Vector2 posBounds = new(160/2 - 10*2 - 8, 144/2 - 10*2 - 8);

	void Start() {
		_pos = transform.position;
	}

	void Update() {
		_pos += _move * playerSpeed * Time.deltaTime;

		_pos.x = Mathf.Clamp(_pos.x, -posBounds.x, posBounds.x);
		_pos.y = Mathf.Clamp(_pos.y, -posBounds.y, posBounds.y);

		transform.position = new Vector3(_pos.x, _pos.y, transform.position.z);
	}

	void OnMove(InputValue value) {
		_move = value.Get<Vector2>();
	}
}
