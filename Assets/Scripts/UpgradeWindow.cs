using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Управляет окном апгрейдов: показывает список апгрейдов для нескольких объектов мебели.
/// </summary>
public class UpgradeWindow : MonoBehaviour
{
    [Header("UI References")]
    public UpgradeItemUI itemPrefab;       // Префаб элемента списка
    public Transform contentRoot;          // Контейнер для элементов списка
    public CameraController cameraController;  // Скрипт камеры
    public FurnitureChoiceHUD choiceHUD;       // HUD выбора варианта мебели

    [Header("Upgrades List")]
    public UpgradeData[] upgradesList; // Массив апгрейдов (каждый содержит ссылку на объект мебели)

    /// <summary>
    /// Открыть окно апгрейдов
    /// </summary>
    public void OpenWindow()
    {
        gameObject.SetActive(true);

        // Очистка старых элементов
        foreach (Transform child in contentRoot)
            Destroy(child.gameObject);

        // Создание новых элементов
        foreach (var upgrade in upgradesList)
        {
            var item = Instantiate(itemPrefab, contentRoot);

            // Берём объект мебели из UpgradeData
            FurnitureController furniture = upgrade.furniture;

            // Инициализация элемента с названием, ценой и действием при клике
            item.Init(upgrade.title, upgrade.price, () =>
            {
                // Закрываем окно апгрейдов
                gameObject.SetActive(false);

                // Фокусируем камеру на выбранной мебели
                if (cameraController != null && furniture != null)
                    cameraController.FocusOn(furniture.CameraFocusPoint);

                // Открываем HUD выбора варианта апгрейда
                if (choiceHUD != null && furniture != null)
                    choiceHUD.Open(furniture);
            });
        }
    }
}
