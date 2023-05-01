using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Surfing {
	public class PlayerController : MonoBehaviour {
		public BarController balanceBar;

		private float _balance = 0.5f;
		private float _balanceMove;

		public GameController game;

		public float balanceSpeed = 0.33f;
		public float offBalanceSpeed = 0.001f;

		public float falloff = 0.2f;

		private Animator _anim;

		public float onWaveMovement = 30;

		void Start() {
			_anim = GetComponent<Animator>();
		}

		void Update() {
			if (!game.InGame()) return;

			transform.position = new Vector3(onWaveMovement * (game.targetBalance - 0.5f), transform.position.y, transform.position.z);

			_balance += _balanceMove * balanceSpeed * Time.deltaTime;

			_balance += (_balance - game.targetBalance > 0 ? 1 : -1) * offBalanceSpeed * Time.deltaTime;

			if (_balance < 0 || _balance > 1) game.Loose();

			if (Mathf.Abs(_balance - game.targetBalance) > falloff) {
				game.Loose();
			}

			if (_balance - game.targetBalance < -falloff * 0.5f || _balance < falloff * 0.5f) {
				_anim.SetTrigger("LeanLeft");
			}else if (_balance - game.targetBalance > falloff * 0.5f || _balance > 1 - falloff * 0.5f) {
				_anim.SetTrigger("LeanRight");
			} else {
				_anim.SetTrigger("Stabilize");
			}

			balanceBar.Value = _balance;
		}

		public void OnMove(InputValue value) {
			_balanceMove = value.Get<Vector2>().x;
		}

		public void Reset() {
			_balance = 0.5f;
		}

		public void FallDown() {
			_anim.SetTrigger("Fall");
		}

		public void GotUp() {
			game.GotUp();
		}

		public void StartGame() {
			game.StartGame();
		}
	}
}