using UnityEngine;

public class IsometricCameraController : MonoBehaviour
{
    private Camera _camera;
    
    [Header("Camera Settings")]
    public float cameraHeight = 15f;
    public float cameraDistance = 10f;
    public Vector3 cameraAngle = new Vector3(30f, 45f, 0f);
    
    [Header("Zoom Settings")]
    public float minZoom = 5f;
    public float maxZoom = 20f;
    public float zoomSpeed = 2f;

    void Awake()
    {
        _camera = GetComponent<Camera>();
    }
    
    void Start()
    {
        SetupCamera();
    }

    void Update()
    {
        HandleZoom();
    }

    void SetupCamera()
    {
        // Устанавливаем позицию камеры
        transform.position = new Vector3(0f, cameraHeight, -cameraDistance);
        
        // Устанавливаем изометрический угол
        transform.rotation = Quaternion.Euler(cameraAngle);
        
        // Делаем камеру ортографической
        if (_camera != null)
        {
            GetComponent<Camera>().orthographic = true;
            GetComponent<Camera>().orthographicSize = cameraHeight;
        }
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            cameraHeight -= scroll * zoomSpeed;
            cameraHeight = Mathf.Clamp(cameraHeight, minZoom, maxZoom);
            
            // Обновляем позицию и размер камеры
            transform.position = new Vector3(0f, cameraHeight, -cameraDistance);
            _camera.orthographicSize = cameraHeight;
        }
    }
}