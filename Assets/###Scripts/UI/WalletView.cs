using UnityEngine;
using TMPro;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textValue;
    [SerializeField] private Wallet _walletMoney;

    // [Inject]
    // private void Constructor(Wallet wallet)
    // {
    //     _wallet = wallet;
    // }

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

    // private void OnBallanceDown(int currentBallance)
    // {
    //     StartCoroutine(Stacking(currentBallance));
    // }

    // private IEnumerator Stacking(int newBallance)
    // {
    //     while (newBallance < _currentBallance)
    //     {
    //         _currentBallance--;
    //         _textValue.text = _currentBallance.ToString();
    //
    //         yield return new WaitForSeconds(0.05f);
    //     }
    // }

    // private void OnBalanceDown(int amount)
    // {
    //     _currentBallance -= amount;
    //     _textValue.text = _currentBallance.ToString();
    // }

    // private void OnBalanceUp(int amount)
    // {
    //     _currentBallance += amount;
    //
    //     _textValue.text = _currentBallance.ToString();
    // }

    private void OnChangedAmount(int amount)
    {
        _textValue.text = _walletMoney.Amount.ToString();
    }
}