using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;

        if (cam == null)
        {
            Debug.LogError("Billboard: MainCamera not found!");
        }
    }

    private void LateUpdate()
    {
        if (cam == null) return;

        transform.LookAt(transform.position + cam.transform.forward);
    }
}
