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

		void Update() {
			_balance += _balanceMove * balanceSpeed * Time.deltaTime;

			_balance += (_balance - game.targetBalance) * offBalanceSpeed * Time.deltaTime;

			balanceBar.Value = _balance;
		}

		public void OnMove(InputValue value) {
			_balanceMove = value.Get<Vector2>().x;
		}
	}
}