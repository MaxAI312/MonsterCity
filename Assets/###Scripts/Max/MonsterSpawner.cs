using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private Shop _shop;
    [SerializeField] private Wallet _wallet;


    // [SerializeField] private GameObject _TEST;
    // [SerializeField] private Goblin _goblin;

    private LevelObserver _levelObserver;
    private IMonsterData _data;

    private MonsterView _currentCard;
    private ParticleSystem _spawnPatricle;

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
        //_cameraController.Clicked += TrySpawn;
        _shop.ItemSelected += MonsterSelected;
    }

    private void OnDisable()
    {
        //_cameraController.Clicked -= TrySpawn;
        _shop.ItemSelected -= MonsterSelected;
    }

    public void MonsterSelected(IMonsterData data)
    {
        _data = data;
        _currentCard = (MonsterView)_data;
    }

    public void TrySpawn(Vector3 position)
    {
        // будет списываться бабло даже если спаунить нельзя
        if (_currentCard.IsProgressBarComplete == false) return;

        if (_wallet.TrySpend(_data.Cost))
        {
            //if (_currentCard.IsProgressBarComplete)
            //{
            //GameObject gameObject = Instantiate(_TEST, position, Quaternion.identity);
            Monster spawned = Instantiate(_data.Prefab, position, Quaternion.identity);

            // Monster spawned = Instantiate(_goblin, position, Quaternion.identity);
            Target nearestNpc = _levelObserver.FindNearestNpc(spawned);
            spawned.Target = nearestNpc;
            //Npc nearestNpc = _levelObserver.FindNearestUnit(spawned);
            spawned.transform.LookAt(nearestNpc.transform);
            //spawned.Init(_data.Options);
            _levelObserver.AddMonsterToCollection(spawned);

            _currentCard.StartNewProgresBar();

            _spawnPatricle.transform.position = new Vector3(position.x, position.y + 0.2f, position.z);
            _spawnPatricle.Play();

            //}
        }
    }
}