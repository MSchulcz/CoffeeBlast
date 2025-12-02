using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float zoomSpeed = 10f;
    public float minZoom = 5f;
    public float maxZoom = 50f;

    private Vector3 dragOrigin;
    private bool isDragging = false;

    void Update()
    {
        HandlePanning();
        HandleZoom();
    }

    void HandlePanning()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 difference = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
            Vector3 move = new Vector3(difference.x * panSpeed, difference.y * panSpeed, 0);
            transform.Translate(move, Space.World);
            dragOrigin = Input.mousePosition;
        }
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            Camera.main.orthographicSize -= scroll * zoomSpeed;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
        }
    }

    public void SetIsometricView()
    {
        // Set camera to orthographic
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = 10f;

        // Set isometric rotation (30 degrees on X, 45 on Y)
        transform.rotation = Quaternion.Euler(30f, 45f, 0f);

        // Position camera appropriately
        transform.position = new Vector3(0, 10, -10);
    }
}
