using UnityEngine;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector2 _leftUp;
    [SerializeField] private Vector2 _rightDown;
    [SerializeField] private float _speedZoom = 0.01f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _canSpawnText;

    private float _zoomMin = 10;
    private float _zoomMax = 35;
    private float _startZoomValue = 28;
    private float _maxNumberOfTouch = 2;
    private float _minLimitX = -0.1f;
    private float _maxLimitX = 0.1f;
    private float _minLimitY = -0.1f;
    private float _maxLimitY = 0.1f;
    private float _maxDistanceRaycast = 1000f;
    private float _durationMovingX = 0.5f;
    private float _durationMovingZ = 0.5f;
    private Camera _camera;
    private Vector3 _touchValue;
    private bool _isMooving;

    private const string MouseWhell = "Mouse ScrollWheel";

    private Coroutine _showMessage;

    public event Action<Vector3> Clicked;

    private void Start()
    {
        _camera = GetComponent<Camera>();

        _camera.orthographicSize = _startZoomValue;
    }

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonUp(0) && _isMooving == false)
                if (TryFindTouchPoint(out RaycastHit hit))
                    Clicked?.Invoke(hit.point);
            
            if (Input.GetMouseButtonDown(0))
                _touchValue = _camera.ScreenToWorldPoint(Input.mousePosition);

            if (Input.touchCount == _maxNumberOfTouch)
                MakeZoom(CalculateValueToZoom() * _speedZoom);

            if (Input.GetMouseButton(0))
            {
                Vector3 direction = (CalculateValueTOMoveDirection());

                _isMooving = (direction.x > _maxLimitX || direction.x < _minLimitX || direction.z > _maxLimitY || direction.z < _minLimitY);

                MakeMove(CalculateValueTOMoveDirection());
            }

            MakeZoom(Input.GetAxis(MouseWhell));
        }
    }

    private bool TryFindTouchPoint(out RaycastHit raycastHit)
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        return Physics.Raycast(ray, out raycastHit, _maxDistanceRaycast, _layerMask);
    }

    private Vector3 CalculateValueTOMoveDirection() => _touchValue - _camera.ScreenToWorldPoint(Input.mousePosition);

    private float CalculateValueToZoom()
    {
        Touch firstTouch = Input.GetTouch(0);
        Touch secondTouch = Input.GetTouch(1);

        Vector2 touchZeroLastPosition = firstTouch.position - firstTouch.deltaPosition;
        Vector2 touchOneLastPosition = secondTouch.position - secondTouch.deltaPosition;

        float distanceTouch = (touchZeroLastPosition - touchOneLastPosition).magnitude;
        float currentDistanceTouch = (firstTouch.position - secondTouch.position).magnitude;
        float difference = currentDistanceTouch - distanceTouch;
        return difference;
    }

    private void MakeMove(Vector3 direction)
    {
        Vector3 cameraPosition = _camera.transform.position;

        if (cameraPosition.x + direction.x >= _rightDown.x)
            direction.x = 0;
        if (cameraPosition.x + direction.x <= _leftUp.x)
            direction.x = 0;
        if (cameraPosition.z + direction.z >= _leftUp.y)
            direction.z = 0;
        if (cameraPosition.z + direction.z <= _rightDown.y)
            direction.z = 0;

        _camera.transform.DOLocalMoveX(direction.x, _durationMovingX).SetRelative().SetEase(Ease.Linear);

        _camera.transform.DOLocalMoveZ(direction.z, _durationMovingZ).SetRelative().SetEase(Ease.Linear);
    }

    private void MakeZoom(float newZoom)
    {
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize - newZoom, _zoomMin, _zoomMax);
    }
}