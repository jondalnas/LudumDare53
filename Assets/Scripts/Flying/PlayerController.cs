using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Flying {
	public class PlayerController : MonoBehaviour {
		private float _position, _positionDelta;

		public float speed = 144;

		public float maxPosition = 54;

		void Start() {

		}

		void Update() {
			_position += _positionDelta * speed * Time.deltaTime;

			if (_position < -maxPosition) {
				_position = -maxPosition;
			} else if (_position > maxPosition) {
				_position = maxPosition;
			}

			transform.position = new Vector3(transform.position.x, _position, transform.position.z);
		}

		public void OnMove(InputValue value) {
			_positionDelta = value.Get<Vector2>().y;
		}
	}
}