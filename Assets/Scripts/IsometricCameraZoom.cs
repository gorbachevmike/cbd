using System;
using UnityEngine;

public class IsometricCameraZoom : MonoBehaviour
{
    public float zoomSpeed = 6;

    public float zoomSmoothness = 5;
    
    public float minZoom = 2f;
    public float maxZoom = 40;
    
    private float _currentZoom;
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        _currentZoom = Mathf.Clamp(_currentZoom - Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime, minZoom, maxZoom);
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _currentZoom, zoomSmoothness * Time.deltaTime);
    }
}
