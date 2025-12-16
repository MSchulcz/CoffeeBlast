using UnityEngine;
using UnityEngine.UI;

public class FurnitureChoiceHUD : MonoBehaviour
{
    public Button[] variantButtons; // 3 кнопки
    private FurnitureController currentFurniture;

    public void Open(FurnitureController furniture)
    {
        currentFurniture = furniture;
        gameObject.SetActive(true);

        for (int i = 0; i < variantButtons.Length; i++)
        {
            int index = i; // обязательно отдельная переменная для замыкания
            variantButtons[i].onClick.RemoveAllListeners();
            variantButtons[i].onClick.AddListener(() => SelectVariant(index));
        }
    }

    // Этот метод обязательно public
    public void SelectVariant(int index)
    {
        if (currentFurniture == null) return;

        currentFurniture.ApplyVariant(index);

        gameObject.SetActive(false);
        Camera.main.GetComponent<CameraController>().ReturnFromFocus();
    }
}
