using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform _background;
    [SerializeField] private RectTransform _handle;
    private float _handleLimit = 1f;

    private Vector2 _inputVector;

    public Vector2 InputVector => _inputVector;

    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_background, eventData.position, eventData.pressEventCamera, out localPoint);
        
        float radius = _background.rect.width / 2f;
        
        _inputVector = localPoint.magnitude > radius ? localPoint.normalized : localPoint / radius;
        
        _handle.anchoredPosition = _inputVector * radius * _handleLimit;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _inputVector = Vector2.zero;
        _handle.anchoredPosition = Vector2.zero;
    }
}