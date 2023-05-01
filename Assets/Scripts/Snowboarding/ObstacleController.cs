using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowboarding {
	public abstract class ObstacleController : MonoBehaviour {
		private GameController _game;

		private Animator _anim;

		private Collider2D _col;

		private AudioSource _aud;

		public virtual void Start() {
			_game = FindAnyObjectByType<GameController>();
			_game.currObstacles.Add(this);

			_anim = GetComponent<Animator>();

			_col = GetComponentInChildren<BoxCollider2D>();

			_aud = GetComponent<AudioSource>();
		}

		public void CheckCollision() {
			ContactFilter2D filter = new();
			filter.SetLayerMask(LayerMask.NameToLayer("Player"));
			filter.useLayerMask = true;

			Collider2D[] res = new Collider2D[1];
			if (Physics2D.OverlapCollider(_col, filter, res) > 0) {
				_game.Hit();
				_aud.Play();
			}
		}

		public void Destroy() {
			Destroy(gameObject);

			_game.currObstacles.Remove(this);
		}

		public void Pause() {
			_anim.speed = 0;
		}

		public void Resume() {
			_anim.speed = 1;
		}
	}
}