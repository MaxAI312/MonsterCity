using System;
using UnityEngine;
using UnityEngine.UI;

public class StarsView : MonoBehaviour
{
    [SerializeField] private Image[] _stars;
    [SerializeField] private StarsCounter _counter;
    [SerializeField] private Button _upgradeButton;

    private Wallet _wallet;
    private bool _isInitCard = false;
    private int _count;

    public event Action<int> Upgraded;


    private void Start()
    {
        _wallet = FindObjectOfType<Wallet>();

        InitCard();
    }

    private void OnEnable()
    {
        _upgradeButton.onClick.AddListener(TryUpgrade);
    }

    private void OnDisable()
    {
        _upgradeButton.onClick.RemoveListener(TryUpgrade);
    }

    private void InitCard()
    {
        _count++;
        _stars[_count - 1].gameObject.SetActive(true);

        Upgraded?.Invoke(_counter[_count - 1]);
        
        _isInitCard = true;
    }
    
    private void TryUpgrade()
    {
        if (_count >= _stars.Length) return;

        if (!_isInitCard) return;
        else
        {
            if (_wallet.TrySpend(200))
            {
                _count++;
                _stars[_count - 1].gameObject.SetActive(true);

                Upgraded?.Invoke(_counter[_count - 1]);
            }
        }
    }

    public void Show(int value)
    {
        _count = _counter.ToStars(value);

        for (int i = 0; i < _count; i++)
            _stars[i].gameObject.SetActive(true);
    }
}