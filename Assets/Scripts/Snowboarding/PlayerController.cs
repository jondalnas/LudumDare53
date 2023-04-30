using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Snowboarding {
	public class PlayerController : MonoBehaviour {
		public float playerSpeed = 144;
		private float _playerMove, _playerPos;
		public float maxPosition = 54;

		void Update() {
            _playerPos += _playerMove * playerSpeed * Time.deltaTime;
			
            if (_playerPos < -maxPosition) {
                _playerPos = -maxPosition;
            } else if (_playerPos > maxPosition) {
                _playerPos = maxPosition;
            }
            
            transform.position = new Vector3(_playerPos, transform.position.y, transform.position.z);
		}

		public void OnMove(InputValue input) {
			_playerMove = input.Get<Vector2>().x;
		}
	}
}