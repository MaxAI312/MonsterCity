using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterView : MonoBehaviour, IMonsterData
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private TMP_Text _freeCountText;
    [SerializeField] private MonsterViewSettings _monsterViewSettings;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _sliderRoot;
    [SerializeField] private GameObject _glow;
    [SerializeField] private UpgradeCardView _upgradeCardView;

    private Slider _slider;
    private int _freeLimit;
    private float _startSpawnTime = 0.1f;
    private float _elapsedTime = 0;
    private float _currenTime;

    public UpgradeCardOptions Options { get; private set; }

    public int Cost => _monsterViewSettings.Cost;
    public Monster Prefab => _monsterViewSettings.Monster;
    public float SpawnTime => _monsterViewSettings.SpawnTime;
    public int FreeCount => Options.FreeLimit;
    public bool IsProgressBarComplete;

    public event Action<IMonsterData> ItemSelected;
    public event Action<MonsterView> ViewSelected;

    private void Start()
    {
        _freeLimit = FreeCount;
        _slider = GetComponentInChildren<Slider>();

        _image.sprite = _monsterViewSettings.Image;
        _cost.text = Cost.ToString();
        _freeCountText.text = FreeCount.ToString();

        IsProgressBarComplete = false;
    }


    private void Update()
    {
        if (IsProgressBarComplete == false)
            SetProgresBar();
    }

    private void SetProgresBar()
    {
        _sliderRoot.gameObject.SetActive(true);

        _elapsedTime += Time.deltaTime;
        _currenTime = (float)_elapsedTime / _startSpawnTime;
        _slider.value = _currenTime;
        if (_slider.value >= 1)
        {
            _elapsedTime = 0;

            IsProgressBarComplete = true;
            _sliderRoot.gameObject.SetActive(false);
        }
    }

    public void StartNewProgresBar()
    {
        _freeLimit--;

        if (_freeLimit <= 0)
        {
            _startSpawnTime = SpawnTime;

            _freeLimit = 0;
        }
        else if (_freeLimit > 0)
            _startSpawnTime = 0.1f;

        _freeCountText.text = _freeLimit.ToString();

        IsProgressBarComplete = false;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(SelectItem);

        _upgradeCardView.Upgraded += OnUpgraded;
    }

    private void OnUpgraded(UpgradeCardOptions obj)
    {
        Options = obj;
        _freeLimit = FreeCount;
        _freeCountText.text = FreeCount.ToString();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(SelectItem);
    }

    private void SelectItem()
    {
        ItemSelected?.Invoke(this);
        ViewSelected?.Invoke(this);
    }

    public void SetGlow(bool valueGlow)
    {
        _glow.gameObject.SetActive(valueGlow);
    }
}