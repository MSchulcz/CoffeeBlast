using UnityEngine;

public class MainMenuCameraScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float zoomSpeed = 5f;
    public float minZoom = 2f;
    public float maxZoom = 10f;
    public float minX = -100f;
    public float maxX = 100f;
    public float minY = -100f;
    public float maxY = 100f;

    private Vector3 dragOrigin;
    private Camera cam;
    private float targetZoom;

    void Start()
    {
        cam = GetComponent<Camera>();
        targetZoom = cam.orthographicSize;
    }

    void Update()
    {
        // Camera movement with mouse drag
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = transform.position + difference;

            // Clamp camera position to prevent going out of bounds
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            transform.position = newPosition;
        }

        // Camera zoom with mouse wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= scroll * zoomSpeed;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
    }
}
