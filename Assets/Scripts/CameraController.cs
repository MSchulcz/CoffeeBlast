using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [Header("Movement")]
    public float panSpeed = 20f;
    public float zoomSpeed = 10f;
    public float minZoom = 5f;
    public float maxZoom = 50f;

    [Header("Focus")]
    public float focusMoveTime = 1f;

    private Vector3 dragOrigin;
    private bool isDragging = false;

    private bool isLocked = false;

    private Vector3 savedPosition;
    private Quaternion savedRotation;
    private float savedZoom;

    void Update()
    {
        if (isLocked)
            return;

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

    // =========================
    // 🔒 ФОКУС НА МЕБЕЛЬ
    // =========================

    public void FocusOn(Vector3 focusPoint)
    {
        if (isLocked) return;

        isLocked = true;

        savedPosition = transform.position;
        savedRotation = transform.rotation;
        savedZoom = Camera.main.orthographicSize;

        StopAllCoroutines();
        StartCoroutine(MoveToFocus(focusPoint));
    }

    IEnumerator MoveToFocus(Vector3 target)
    {
        Vector3 startPos = transform.position;
        float startZoom = Camera.main.orthographicSize;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / focusMoveTime;
            transform.position = Vector3.Lerp(startPos, target, t);
            Camera.main.orthographicSize = Mathf.Lerp(startZoom, minZoom, t);
            yield return null;
        }
    }

    public void ReturnFromFocus()
    {
        StopAllCoroutines();
        StartCoroutine(ReturnRoutine());
    }

    IEnumerator ReturnRoutine()
    {
        Vector3 startPos = transform.position;
        float startZoom = Camera.main.orthographicSize;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / focusMoveTime;
            transform.position = Vector3.Lerp(startPos, savedPosition, t);
            Camera.main.orthographicSize = Mathf.Lerp(startZoom, savedZoom, t);
            yield return null;
        }

        transform.rotation = savedRotation;
        isLocked = false;
    }

    // =========================
    // Изометрия (оставляем)
    // =========================

    public void SetIsometricView()
    {
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = 10f;
        transform.rotation = Quaternion.Euler(30f, 45f, 0f);
        transform.position = new Vector3(0, 10, -10);
    }
}
