using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCardView : MonoBehaviour
{
    [SerializeField] private MonsterViewSettings _monsterViewSettings;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private Image _image;
    
    [Space] [SerializeField] private StarsView _freeCountText;
    [SerializeField] private StarsView _damage;
    [SerializeField] private StarsView _speed;

    [SerializeField] private Button _buyButton;
    [SerializeField] private GameObject _upgradeSettings;
    [SerializeField] private GameObject _glowEffects;
    [SerializeField] private Button _battleButton;

    private UpgradeCardOptions _cardOptions;
    private Wallet _wallet;
    private bool _isActive = false;
    
    public event Action<UpgradeCardOptions> Upgraded;

    private void Start()
    {
        _wallet = FindObjectOfType<Wallet>();
        
        OnBuyed(); // для первоначальной покупки всех монсттров, для тестов
        
    }

    private void Awake()
    {
        _name.text = _monsterViewSettings.Name;
        _image.sprite = _monsterViewSettings.Image;
        _cost.text = _monsterViewSettings.Cost.ToString();
        // взять обхект из системы сохранений

        // добавить отображение карточки если true
        _freeCountText.Show(_cardOptions.FreeLimit);
        _damage.Show(_cardOptions.Damage);
        _speed.Show(_cardOptions.Speed);
    }

    private void OnEnable()
    {
        _freeCountText.Upgraded += OnUpgradedFreeLimit;
        _damage.Upgraded += OnUpgrededDemage;
        _speed.Upgraded += OnUpgradedSpeed;
        
        _buyButton.onClick.AddListener(OnBuyed);
    }

    private void OnDisable()
    {
        _freeCountText.Upgraded -= OnUpgradedFreeLimit;
        _damage.Upgraded -= OnUpgrededDemage;
        _speed.Upgraded -= OnUpgradedSpeed;
        
        _buyButton.onClick.RemoveListener(OnBuyed);
    }

    private void OnBuyed()
    {
        if (_wallet.TrySpend(_monsterViewSettings.Cost))
        {
            _isActive = true;
            _upgradeSettings.gameObject.SetActive(true);
            _buyButton.gameObject.SetActive(false);
            _glowEffects.gameObject.SetActive(true);
            _battleButton.gameObject.SetActive(true);
        }
    }

    private void OnUpgradedFreeLimit(int limitValue)
    {
        _cardOptions.FreeLimit = limitValue;
        Upgraded?.Invoke(_cardOptions);
    }

    private void OnUpgradedSpeed(int speedValue)
    {
        _cardOptions.Speed = speedValue;
        Upgraded?.Invoke(_cardOptions);
    }

    private void OnUpgrededDemage(int damageValue)
    {
        _cardOptions.Damage = damageValue;
        Upgraded?.Invoke(_cardOptions);
    }
}