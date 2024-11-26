using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Scripts.Core
{
    public class UIConfig : ScriptableObject
    {
        [SerializeField] private List<WindowController> _windowPrefabs;

        public WindowController GetWindowByType<T>() where T : WindowController
        {
            var window = _windowPrefabs.FirstOrDefault(x => x.GetType() == typeof(T));
            if (window is null)
            {
                Debug.LogError($"Window with type {typeof(T)} doesnt exist in UIConfig");
                return null;
            }

            return window;
        }
    }
}
