using UnityEngine;
using Zenject;

namespace UI.Scripts.Core
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UIConfig _uiConfig;
        [SerializeField] private Joystick _joystick;
        private WindowController _currentScreen;
        private DiContainer _container;

        public Joystick Joystick => _joystick;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }
        public void Show<T>() where T: WindowController
        {
            if(_currentScreen?.GetType() == typeof(T)) return;
            
            if (_currentScreen is not null)
            {
                _currentScreen.Hide();
            }
            var window = _container.InstantiatePrefab(_uiConfig.GetWindowByType<T>(),transform).GetComponent<T>();
            _currentScreen = window;
            _currentScreen.Show();
        }
        
    }
}
