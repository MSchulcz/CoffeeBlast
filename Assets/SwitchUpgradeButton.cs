using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    [Header("Upgrade settings")]
    [SerializeField] private int requiredCoins;

    [Header("Target")]
    [SerializeField] private ObjectStateSwitcher objectStateSwitcher;

    public void OnButtonClick()
    {
        if (ProgressController.Coins < requiredCoins)
        {
            Debug.Log("Недостаточно монет");
            return;
        }

        ProgressController.Coins -= requiredCoins;
        objectStateSwitcher.Switch();
    }
}
