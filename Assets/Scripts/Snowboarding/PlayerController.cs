using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Snowboarding {
	public class PlayerController : MonoBehaviour {
		public float playerSpeed = 144;
		private float _playerMove, _playerPos;
		public float maxPosition = 54;

        private GameController _game;

        private Animator _anim;

        void Start() {
            _game = FindAnyObjectByType<GameController>();
            _anim = GetComponent<Animator>();
        }

		void Update() {
            if (!_game.IsInGame()) return;

            _playerPos += _playerMove * playerSpeed * Time.deltaTime;
			
            if (_playerPos < -maxPosition) {
                _playerPos = -maxPosition;
            } else if (_playerPos > maxPosition) {
                _playerPos = maxPosition;
            }
            
            transform.position = new Vector3(_playerPos, transform.position.y, transform.position.z);
		}

        public void Hit() {
            _anim.SetTrigger("Fall");
        }

        public void GetUp() {
            _game.GetUp();
        }

		public void OnMove(InputValue input) {
			_playerMove = input.Get<Vector2>().x;
		}
	}
}