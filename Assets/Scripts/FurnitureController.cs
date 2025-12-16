using UnityEngine;

public class FurnitureController : MonoBehaviour
{
    [Header("Furniture Variants")]
    public GameObject[] variants; // 0,1,2

    [Header("Camera Focus")]
    public Vector3 cameraOffset = new Vector3(0, 1.5f, 0);

    private int currentVariant;

    private string SaveKey => $"Furniture_{gameObject.name}_Variant";

    private void Awake()
    {
        currentVariant = PlayerPrefs.GetInt(SaveKey, 0);
        ApplyVariant(currentVariant);
    }

    public void ApplyVariant(int index)
    {
        if (index < 0 || index >= variants.Length)
        {
            Debug.LogError("Furniture variant index out of range");
            return;
        }

        for (int i = 0; i < variants.Length; i++)
        {
            variants[i].SetActive(i == index);
        }

        currentVariant = index;
        PlayerPrefs.SetInt(SaveKey, currentVariant);
        PlayerPrefs.Save();
    }

    public Vector3 CameraFocusPoint =>
        transform.position + cameraOffset;
}
