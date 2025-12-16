using UnityEngine;
using UnityEngine.UI;
using System;

public class UpgradeItemUI : MonoBehaviour
{
    public Text titleText;
    public Text priceText;
    public Button actionButton;

    // public Init с 3 аргументами
    public void Init(string title, int price, Action onClick)
    {
        titleText.text = title;
        priceText.text = price.ToString();

        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(() => onClick());
    }
}
