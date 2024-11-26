using System;
using UI.Scripts;
using UnityEngine;

namespace Gameplay.Scripts.PlayerShipManagement
{
    public class Bullet : MonoBehaviour
    {
        private float _speed = 10f;
        private Action _onDestroy;

        public void Init(Action onDestroy)
        {
            _onDestroy = onDestroy;
        }

        public void SetPosition(Vector2 vector2) => transform.position = vector2;

        void Update()
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
            if (Mathf.Abs(transform.position.y) > 10f) _onDestroy?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Asteroid asteroid))
            {
                asteroid.TakeDamage();
                _onDestroy?.Invoke();
            }
        }
    }
}