using UnityEngine;

public class MovablePair : MonoBehaviour
{
    private Camera _mainCamera;
    private float _cameraZDistance;
    private Vector3 _initialPosition;
    private bool _connected;

    private const string _portTag = "Port";
    private const float _dragResponseThreshold = 2;

    void Start()
    {
        _mainCamera = Camera.main;
        _cameraZDistance = _mainCamera.WorldToScreenPoint(transform.position).z; //Z Axis of the game object for screen view
    }

    void OnMouseDrag()
    {
        Vector3 ScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cameraZDistance); //Z Axis added to screen point
        Vector3 NewWorldPosition = _mainCamera.ScreenToWorldPoint(ScreenPosition); //Screen point converted to world point

        if (!_connected)
        {
            transform.position = NewWorldPosition;
        }

        else if (Vector3.Distance(a: transform.position, b: NewWorldPosition) > _dragResponseThreshold)
        {
            _connected = false;
        }
    }

    private void OnMouseUp()
    {
        if (!_connected)
        {
            ResetPosition();
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetInitialPosition(Vector3 NewPosition)
    {
        _initialPosition = NewPosition;
        transform.position = _initialPosition;
    }

    private void ResetPosition()
    {
        transform.position = _initialPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_portTag))
        {
            _connected = true;
            transform.position = other.transform.position;
        }
    }
}

