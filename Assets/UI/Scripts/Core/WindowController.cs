using UnityEngine;

namespace UI.Scripts.Core
{
    public abstract class WindowController : MonoBehaviour
    {

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }
        
        public virtual void Hide()
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}