using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private Shop _shop;
    [SerializeField] private Wallet _wallet;

    private LevelObserver _levelObserver;
    private IMonsterData _data;

    private MonsterView _currentCard;
    private ParticleSystem _spawnPatricle;
    private float _spawnEffectOffsetX = 0.2f;

    public CameraController CameraController => _cameraController;
    public Shop Shop => _shop;
    public Wallet Wallet => _wallet;

    private void Start()
    {
        _levelObserver = FindObjectOfType<LevelObserver>();
        _spawnPatricle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        _shop.ItemSelected += MonsterSelected;
    }

    private void OnDisable()
    {
        _shop.ItemSelected -= MonsterSelected;
    }

    public void MonsterSelected(IMonsterData data)
    {
        _data = data;
        _currentCard = (MonsterView)_data;
    }

    public void TrySpawn(Vector3 position)
    {
        if (_currentCard.IsProgressBarComplete == false) return;

        if (_wallet.TrySpend(_data.Cost))
        {
            Monster spawned = Instantiate(_data.Prefab, position, Quaternion.identity);
            Target nearestNpc = _levelObserver.FindNearestNpc(spawned);
            
            spawned.Target = nearestNpc;
            spawned.transform.LookAt(nearestNpc.transform);

            _levelObserver.AddMonsterToCollection(spawned);
            _currentCard.StartNewProgresBar();

            _spawnPatricle.transform.position = new Vector3(position.x, position.y + _spawnEffectOffsetX, position.z);
            _spawnPatricle.Play();
        }
    }
}