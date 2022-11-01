using UnityEngine;
using TMPro;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textValue;
    [SerializeField] private Wallet _walletMoney;
    
    private void Start()
    {
        _textValue.text = _walletMoney.Amount.ToString();
    }

    private void OnEnable()
    {
        _walletMoney.AmountChanged += OnChangedAmount;
    }

    private void OnDisable()
    {
        _walletMoney.AmountChanged -= OnChangedAmount;
    }

    private void OnChangedAmount(int amount)
    {
        _textValue.text = _walletMoney.Amount.ToString();
    }
}