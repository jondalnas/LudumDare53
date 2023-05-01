using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skydiving {
	public class ObstacleController : MonoBehaviour {
		private GameController _game;

		private Animator _anim;

		private Collider2D _col;

        public float minSpeed = -10, maxSpeed = 120;
        private float _speed;

		private bool _checkCollision;

		void Start() {
			_game = FindAnyObjectByType<GameController>();

			_anim = GetComponent<Animator>();

			_col = GetComponentInChildren<BoxCollider2D>();

            _speed = Random.Range(minSpeed, maxSpeed);
        }

        void Update() {
            transform.position += Vector3.down * _speed * Time.deltaTime;

			if (!_checkCollision) return;

			ContactFilter2D filter = new();
			filter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
			filter.useLayerMask = true;

			Collider2D[] res = new Collider2D[1];
			if (Physics2D.OverlapCollider(_col, filter, res) > 0) {
				_game.Hit();
			}
        }

		public void StartCheckCollision() {
			_checkCollision = true;
		}

		public void StopCheckCollision() {
			_checkCollision = false;
		}

		public void Destroy() {
			Destroy(gameObject);
		}

		public void Pause() {
			_anim.speed = 0;
		}

		public void Resume() {
			_anim.speed = 1;
		}
	}
}