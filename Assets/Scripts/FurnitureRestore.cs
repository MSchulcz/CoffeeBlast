using UnityEngine;

public class FurnitureRestore : MonoBehaviour
{
    [Header("ID")]
    public string furnitureId;

    [Header("Furniture")]
    public GameObject oldFurniture;
    public GameObject newFurniture;

    [Header("Upgrade")]
    public int restorePrice = 100;
    public GameObject upgradeButtonCanvas;

    private bool isRestored;
    private string SaveKey => $"Furniture_{furnitureId}_Restored";

    private void Awake()
    {
        LoadState();
    }

    private void LoadState()
    {
        isRestored = PlayerPrefs.GetInt(SaveKey, 0) == 1;

        oldFurniture.SetActive(!isRestored);
        newFurniture.SetActive(isRestored);
        upgradeButtonCanvas.SetActive(!isRestored);
    }

    public void TryRestore()
    {
        if (ProgressController.Coins < restorePrice)
        {
            Debug.Log("Недостаточно монет");
            return;
        }

        ProgressController.Coins -= restorePrice;

        isRestored = true;
        PlayerPrefs.SetInt(SaveKey, 1);
        PlayerPrefs.Save();

        oldFurniture.SetActive(false);
        newFurniture.SetActive(true);
        upgradeButtonCanvas.SetActive(false);
    }

    private void OnMouseDown()
{
    Debug.Log("CLICK ON FURNITURE ROOT");
}

}
