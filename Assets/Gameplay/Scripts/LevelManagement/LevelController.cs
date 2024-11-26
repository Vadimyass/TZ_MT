using System.Collections;
using System.Collections.Generic;
using Gameplay.Scripts.DataManagement;
using Gameplay.Scripts.PlayerShipManagement;
using Pools;
using UI.Scripts;
using UI.Scripts.Core;
using UnityEngine;
using Zenject;
using Random = Unity.Mathematics.Random;

namespace Gameplay.Scripts.LevelManagement
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private PlayerShipController _playerShip;

        private ObjectPool<Asteroid> _asteroidPool;
        private List<Asteroid> _asteroids;
        private AsteroidSpriteConfig _asteroidSpriteConfig;

        private int _asteroidCount;
        private float _spawnRate;
        private int _spawnedAsteroidsCount;
        private LevelModel _levelModel;
        private Random _random;
        private UIManager _uiManager;
        private DiContainer _container;
        private PlayerPrefsSaveManager _playerPrefsSaveManager;
        private int _durationTime;
        private int CurrentLevelIndex => _playerPrefsSaveManager.PrefsData.GameplayModel.CurrentLevel;


        private const int InitialPoolCapacity = 10;


        [Inject]
        private void Construct(UIManager uiManager, DiContainer container,
            PlayerPrefsSaveManager playerPrefsSaveManager, AsteroidSpriteConfig asteroidSpriteConfig)
        {
            _asteroidSpriteConfig = asteroidSpriteConfig;
            _playerPrefsSaveManager = playerPrefsSaveManager;
            _container = container;
            _uiManager = uiManager;
        }

        public void Initialize(int level)
        {
            _asteroidPool ??= new ObjectPool<Asteroid>(_asteroidPrefab, InitialPoolCapacity);
            SetLevel(level);
            Show();
            StartCoroutine(SpawnAsteroids());
        }

        public void SetLevel(int level)
        {
            _playerPrefsSaveManager.PrefsData.GameplayModel.SetCurrentLevel(level);
            _playerPrefsSaveManager.PrefsData.SessionModel.CurrentSession.SetLevel(level);
            _levelModel = _levelConfig.GetLevelByIndex(level);
            _random = new Random((uint)_levelModel.Seed.GetHashCode());
            _asteroidCount = _random.NextInt(5, 10);
            _spawnRate = _random.NextFloat(0.8f, 1.2f);
            _playerShip.ResetHp();
            _asteroids = new();
        }

        public void Hide()
        {
            _playerShip.Hide();
            _uiManager.Joystick.Hide();
            gameObject.SetActive(false);
            StopSpawning();
            ReturnAllAsteroids();
        }

        private void ReturnAllAsteroids()
        {
            for (int i = 0; i < _asteroids.Count; i++)
            {
                _asteroidPool.Return(_asteroids[i]);
            }
        }

        public void Show()
        {
            _spawnedAsteroidsCount = 0;
            _durationTime = 0;
            _playerShip.Show();
            _uiManager.Joystick.Show();
            gameObject.SetActive(true);
            StartCoroutine(StartTimer());
        }

        public void RestartLevel()
        {
            _uiManager.Show<GameplayScreen>();
            Show();
            _playerShip.ResetHp();
            StartCoroutine(SpawnAsteroids());
        }

        public void StartNextLevel()
        {
            _uiManager.Show<GameplayScreen>();

            if (CurrentLevelIndex < 2)
            {
                SetLevel(CurrentLevelIndex + 1);
            }
            else
            {
                SetLevel(0);
            }

            Show();
            StartCoroutine(SpawnAsteroids());
        }

        private IEnumerator SpawnAsteroids()
        {
            for (int i = 0; i < _asteroidCount; i++)
            {
                var spawnPoint = _spawnPoints[_random.NextInt(0, _spawnPoints.Length - 1)];
                var asteroid = _asteroidPool.Get();
                _container.Inject(asteroid);
                asteroid.SetPosition(spawnPoint.position);
                _spawnedAsteroidsCount++;
                _asteroids.Add(asteroid);
                var spriteIndex = _random.NextInt(0, _asteroidSpriteConfig.SpritesCount - 1);
                var asteroidSprite = _asteroidSpriteConfig.GetSpriteByIndex(spriteIndex);

                asteroid.Initialize(asteroidSprite, () =>
                {
                    if (_spawnedAsteroidsCount >= _asteroidCount &&
                        _asteroidPool.ActiveObjectCount == InitialPoolCapacity - 1)
                    {
                        _uiManager.Show<WinScreen>();
                    }

                    _asteroidPool.Return(asteroid);
                });

                yield return new WaitForSeconds(_spawnRate);
            }
        }

        public IEnumerator StartTimer()
        {
            yield return new WaitForSecondsRealtime(1);
            _playerPrefsSaveManager.PrefsData.SessionModel.CurrentSession.AddDuration(1);
            StartCoroutine(StartTimer());
        }

        public void StopSpawning()
        {
            StopAllCoroutines();
        }
    }
}