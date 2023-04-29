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
		public float offBalanceSpeed = 0.33f;

		public float falloff = 0.2f;

		private Animator _anim;

		void Start() {
			_anim = GetComponent<Animator>();
		}

		void Update() {
			if (!game.InGame()) return;

			_balance += _balanceMove * balanceSpeed * Time.deltaTime;

			_balance += (_balance - game.targetBalance) * offBalanceSpeed * Time.deltaTime;

			if (Mathf.Abs(_balance - game.targetBalance) > falloff) {
				game.Loose();
			}

			if (_balance - game.targetBalance < -0.1f) {
				_anim.SetTrigger("LeanLeft");
			}else if (_balance - game.targetBalance > 0.1f) {
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
	}
}