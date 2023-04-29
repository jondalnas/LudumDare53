using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Surfing {
	public class PlayerController : MonoBehaviour {
		public BarController balanceBar;

		private float _balance;
		private float _balanceMove;

		public float balanceSpeed = 0.33f;

		void Update() {
			_balance += _balanceMove * balanceSpeed * Time.deltaTime;

			balanceBar.Value = _balance;
		}

		public void OnMove(InputValue value) {
			_balanceMove = value.Get<Vector2>().x;
		}
	}
}