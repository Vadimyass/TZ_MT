using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform background; // Фон джойстика
    public RectTransform handle;     // Указатель джойстика
    public float handleLimit = 1f;   // Ограничение движения ручки (1 = граница круга)

    private Vector2 inputVector;

    public Vector2 InputVector => inputVector; // Для получения текущего направления

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
        RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out localPoint);

        // Вычисляем радиус фона джойстика
        float radius = background.rect.width / 2f; // Для круга берём ширину (или высоту, если они равны)

        // Нормализуем позицию в пределах радиуса
        inputVector = localPoint.magnitude > radius ? localPoint.normalized : localPoint / radius;

        // Перемещение ручки джойстика
        handle.anchoredPosition = inputVector * radius * handleLimit;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}