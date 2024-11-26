using System;
using Gameplay.Scripts.DataManagement;
using UnityEngine;
using Zenject;

namespace Gameplay.Scripts.PlayerShipManagement
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _asteroidSprite;
        
        private int _currentHealthPool = 2;
    
        private Vector2 _screenBounds;
        private PlayerPrefsSaveManager _playerPrefsSaveManager;
        private Action _onDestroy;
        
        private const float _speed = 2f;
    
        [Inject]
        private void Construct(PlayerPrefsSaveManager playerPrefsSaveManager)
        {
            _playerPrefsSaveManager = playerPrefsSaveManager;
        }
    
        public void Initialize(Sprite sprite,Action onDestroy)
        {
            _asteroidSprite.sprite = sprite;
            _onDestroy = onDestroy;
            _screenBounds =
                Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        }
    
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    
        void Update()
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
    
            if (transform.position.y < -_screenBounds.y - 1f)
                ReturnToPool();
        }
    
        public void TakeDamage()
        {
            _currentHealthPool--;
            if (_currentHealthPool <= 0)
            {
                _playerPrefsSaveManager.PrefsData.SessionModel.AddScore(1);
                ReturnToPool();
            }
        }
    
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerShipController ship))
            {
                ReturnToPool();
                ship.TakeDamage(1);
            }
        }
    
        private void ReturnToPool()
        {
            _currentHealthPool = 2;
            _onDestroy?.Invoke();
        }
    }
}
