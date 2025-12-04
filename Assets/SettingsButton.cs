using UnityEngine;

using UnityEngine;
using UnityEngine.UI;

public class ActivateUIElement : MonoBehaviour
{
    // UI элемент, который нужно активировать
    public GameObject uiElement;

    // Кнопка, которая будет активировать UI элемент
    public Button activationButton;

    void Start()
    {
        // Назначаем метод ActivateElement как обработчик нажатия кнопки
        if (activationButton != null)
        {
            activationButton.onClick.AddListener(ActivateElement);
        }
        else
        {
            Debug.LogWarning("Кнопка не назначена!");
        }
    }

    // Этот метод вызывается по нажатию кнопки
    public void ActivateElement()
    {
        if (uiElement != null)
        {
            uiElement.SetActive(true);
        }
        else
        {
            Debug.LogWarning("UI элемент не назначен!");
        }
    }
}
