using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pools
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private Queue<T> _objects = new Queue<T>();
        public int ActiveObjectCount => _objects.Count();

        public ObjectPool(T prefab, int initialSize = 10)
        {
            _prefab = prefab;
            for (int i = 0; i < initialSize; i++)
            {
                T obj = Object.Instantiate(_prefab);
                obj.gameObject.SetActive(false);
                _objects.Enqueue(obj);
            }
        }

        public T Get()
        {
            if (_objects.Count > 0)
            {
                var obj = _objects.Dequeue();
                obj.gameObject.SetActive(true);
                return obj;
            }
            
            return Object.Instantiate(_prefab);
        }

        public void Return(T obj)
        {
            if(_objects.Contains(obj)) return;
            
            obj.gameObject.SetActive(false);
            _objects.Enqueue(obj);
        }

    }
}