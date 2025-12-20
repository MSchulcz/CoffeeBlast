using UnityEngine;

public class ObjectStateSwitcher : MonoBehaviour
{
    [Header("Objects to switch")]
    [SerializeField] private GameObject activeObject;       // объект, который будет включен после апгрейда
    [SerializeField] private GameObject inactiveObject;     // объект, который будет выключен после апгрейда

    [Header("Optional extra object to deactivate")]
    [SerializeField] private GameObject extraObjectToDeactivate; // дополнительный объект, который деактивируется после апгрейда

    private bool isSwitched = false;

    private void Start()
    {
        // Устанавливаем начальное состояние
        if (activeObject != null)
            activeObject.SetActive(true);

        if (inactiveObject != null)
            inactiveObject.SetActive(false);
    }

    public void Switch()
    {
        if (isSwitched)
            return;

        // Меняем основное состояние
        if (activeObject != null)
            activeObject.SetActive(false);

        if (inactiveObject != null)
            inactiveObject.SetActive(true);

        // Деактивируем дополнительный объект, если назначен
        if (extraObjectToDeactivate != null)
            extraObjectToDeactivate.SetActive(false);

        isSwitched = true;
    }
}
