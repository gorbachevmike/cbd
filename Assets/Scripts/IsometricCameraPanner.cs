using UnityEngine;

public class IsometricCameraPanner : MonoBehaviour
{
    public float panSpeed = 6f;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 panPosition = touch.deltaPosition;
                Vector3 position = new Vector3(panPosition.x, 0, panPosition.y) * (panSpeed * Time.deltaTime);
                transform.position += Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * position;
            }
        }
        
    }
}
