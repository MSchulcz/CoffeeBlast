using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Match3
{
    public class PlayCloseButton : MonoBehaviour
    {
        // UI элемент, который нужно деактивировать
        public GameObject uiElement;

        // Кнопка для закрытия окна
        public Button closeButton;

        void Start()
        {
            // Назначаем метод CloseElement как обработчик нажатия кнопки
            if (closeButton != null)
            {
                closeButton.onClick.AddListener(CloseElement);
            }
            else
            {
                Debug.LogWarning("Кнопка закрытия не назначена!");
            }

            // Добавляем обработчик клика по экрану
            if (uiElement != null)
            {
                // Создаем пустой GameObject для обработки кликов по экрану
                GameObject clickHandler = new GameObject("ScreenClickHandler");
                clickHandler.transform.SetParent(uiElement.transform.parent);
                clickHandler.AddComponent<CanvasRenderer>();
                Image image = clickHandler.AddComponent<Image>();
                image.color = new Color(0, 0, 0, 0); // Полностью прозрачный
                image.raycastTarget = true;

                // Добавляем обработчик клика с проверкой на PlayPanel и PlayButton
                Button screenButton = clickHandler.AddComponent<Button>();
                screenButton.onClick.AddListener(() => {
                    // Получаем позицию клика
                    PointerEventData eventData = new PointerEventData(EventSystem.current);
                    eventData.position = Input.mousePosition;
                    List<RaycastResult> results = new List<RaycastResult>();
                    EventSystem.current.RaycastAll(eventData, results);

// Проверяем, был ли клик по PlayPanel или PlayButton
bool clickedOnPlayPanel = false;
bool clickedOnPlayButton = false;
bool clickedOnPlayCloseButton = false;
foreach (RaycastResult result in results)
{
    if (result.gameObject.name == "PlayPanel")
    {
        clickedOnPlayPanel = true;
    }
    else if (result.gameObject.name == "PlayButton")
    {
        clickedOnPlayButton = true;
    }
    else if (result.gameObject.name == "PlayCloseButton")
    {
        clickedOnPlayCloseButton = true;
    }
}


// Закрываем только если клик не был по PlayPanel или был по PlayButton или PlayCloseButton
if (!clickedOnPlayPanel || clickedOnPlayButton || clickedOnPlayCloseButton)
{
    CloseElement();
}

                });

                // Устанавливаем размеры и позицию
                RectTransform rt = clickHandler.GetComponent<RectTransform>();
                rt.anchorMin = Vector2.zero;
                rt.anchorMax = Vector2.one;
                rt.offsetMin = Vector2.zero;
                rt.offsetMax = Vector2.zero;
            }
            else
            {
                Debug.LogWarning("UI элемент не назначен!");
            }
        }

        // Этот метод вызывается по нажатию кнопки закрытия или клика по экрану
        public void CloseElement()
        {
            if (uiElement != null)
            {
                uiElement.SetActive(false);
            }
            else
            {
                Debug.LogWarning("UI элемент не назначен!");
            }
        }
    }
}
