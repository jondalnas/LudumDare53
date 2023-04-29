using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Flying {
	public class PlayerController : MonoBehaviour {
		private float _position, _positionDelta;

		public GameController game;

		public float speed = 144;

		public float maxPosition = 54;

		private BoxCollider2D _col;

		void Start() {
			_col = GetComponent<BoxCollider2D>();
		}

		void Update() {
			_position += _positionDelta * speed * Time.deltaTime;

			if (_position < -maxPosition) {
				_position = -maxPosition;
			} else if (_position > maxPosition) {
				_position = maxPosition;
			}

			transform.position = new Vector3(transform.position.x, _position, transform.position.z);

			Collider2D[] hit = new Collider2D[1];
			if (Physics2D.OverlapCollider(_col, new ContactFilter2D().NoFilter(), hit) > 0) {
				hit[0].GetComponent<EnemyController>().Hit();
				game.HitEnemy();
			}
		}

		public void Hit() {

		}

		public void OnMove(InputValue value) {
			_positionDelta = value.Get<Vector2>().y;
		}
	}
}