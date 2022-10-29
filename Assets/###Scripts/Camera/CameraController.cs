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
    private Camera _camera;
    private Vector3 _touchValue;
    private bool _isMooving;

    private const string MouseWhell = "Mouse ScrollWheel";

    private Coroutine _showMessage;

    public event Action<Vector3> Clicked;
    //public Vector3 PointClick { get; private set; }

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
                {
                    Clicked?.Invoke(hit.point);
                    //PointClick
                }
                else
                {
                    // _canSpawnText.gameObject.SetActive(true);
                    //
                    // _showMessage = StartCoroutine(_showOnTimer());
                }


            if (Input.GetMouseButtonDown(0))
                _touchValue = _camera.ScreenToWorldPoint(Input.mousePosition);

            if (Input.touchCount == 2)
                MakeZoom(CalculateValueToZoom() * _speedZoom);

            if (Input.GetMouseButton(0))
            {
                Vector3 direction = (CalculateValueTOMoveDirection());

                _isMooving = (direction.x > 0.1f || direction.x < -0.1f || direction.z > 0.1f || direction.z < -0.1f);

                MakeMove(CalculateValueTOMoveDirection());
            }

            MakeZoom(Input.GetAxis(MouseWhell));
        }
    }

    private IEnumerator _showOnTimer()
    {
        yield return new WaitForSeconds(1);
        _canSpawnText.gameObject.SetActive(false);
    }

    private bool TryFindTouchPoint(out RaycastHit raycastHit)
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        return Physics.Raycast(ray, out raycastHit, 1000f, _layerMask);
    }

    private Vector3 CalculateValueTOMoveDirection() => _touchValue - _camera.ScreenToWorldPoint(Input.mousePosition);

    private float CalculateValueToZoom()
    {
        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        Vector2 touchZeroLastPosition = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOneLastPosition = touchOne.position - touchOne.deltaPosition;

        float distanceTouch = (touchZeroLastPosition - touchOneLastPosition).magnitude;
        float currentDistanceTouch = (touchZero.position - touchOne.position).magnitude;
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

        _camera.transform.DOLocalMoveX(direction.x, 0.5f).SetRelative().SetEase(Ease.Linear);

        _camera.transform.DOLocalMoveZ(direction.z, 0.5f).SetRelative().SetEase(Ease.Linear);
    }

    private void MakeZoom(float newZoom)
    {
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize - newZoom, _zoomMin, _zoomMax);
    }
}