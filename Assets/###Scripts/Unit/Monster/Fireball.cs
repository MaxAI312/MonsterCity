using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private float _flyDuration;

    private Transform _target;
    private Vector3 _startPosition;
    private float _timer;
    private float _maxTimerValue = 1f;

    private void Awake()
    {
        enabled = false;
        _timer = 0;
    }

    private void Update()
    {
        _timer += Time.deltaTime / _flyDuration;

        var targetPoint = new Vector3(_target.transform.position.x, _startPosition.y, _target.transform.position.z);
        
        transform.position = Vector3.Lerp(_startPosition, targetPoint, _timer);
        if(_timer >= _maxTimerValue)
            ShowDisappear();
    }

    public void Initialization(Transform target)
    {
        _target = target;
        _startPosition = transform.position;
        enabled = true;
    }

    private void ShowDisappear()
    {
        Instantiate(_explosionEffect,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}