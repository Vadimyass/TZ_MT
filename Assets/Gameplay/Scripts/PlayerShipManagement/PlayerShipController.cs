using System.Collections;
using System.Collections.Generic;
using Gameplay.Scripts.DataManagement;
using Gameplay.Scripts.Signals;
using Pools;
using UI;
using UI.Scripts;
using UI.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Gameplay.Scripts.PlayerShipManagement
{
    public class PlayerShipController : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform shootPoint;

        private Joystick _joystick;
        private float _speed = 5f;
        private ObjectPool<Bullet> _bulletPool;
        private float _fireRate = 0.5f;
        private int _health;
        private Vector2 _screenBounds;
        private float _nextFireTime;
        private UIManager _uiManager;
        private SignalBus _signalBus;

        private const int StartHealthPool = 3;

        [Inject]
        private void Construct(UIManager uiManager, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _uiManager = uiManager;
        }

        void Start()
        {
            _screenBounds =
                Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,
                    Camera.main.transform.position.z));
            _bulletPool = new ObjectPool<Bullet>(_bulletPrefab);
            _joystick = _uiManager.Joystick;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        void Update()
        {
            if (_joystick is null) return;
            Move();
            Shoot();
        }

        public void ResetHp()
        {
            _health = StartHealthPool;
            _signalBus.Fire(new HealthChangedSignal(_health));
        }

        void Move()
        {
            Vector2 direction = _joystick.InputVector;
            Vector3 move = new Vector3(direction.x, direction.y, 0) * _speed * Time.deltaTime;
            transform.position += move;

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -_screenBounds.x, _screenBounds.x),
                Mathf.Clamp(transform.position.y, -_screenBounds.y, _screenBounds.y),
                transform.position.z
            );
        }

        public void TakeDamage(int value)
        {
            if (_health - value >= 0)
            {
                _health -= value;
                _signalBus.Fire(new HealthChangedSignal(_health));
            }

            if (_health <= 0)
            {
                Death();
            }
        }

        private void Death()
        {
            _uiManager.Show<LoseScreen>();

        }

        void Shoot()
        {
            if (Time.time > _nextFireTime)
            {
                var bullet = _bulletPool.Get();
                bullet.SetPosition(shootPoint.position);
                bullet.Init(() => { _bulletPool.Return(bullet); });
                _nextFireTime = Time.time + _fireRate;
            }
        }
    }
}
