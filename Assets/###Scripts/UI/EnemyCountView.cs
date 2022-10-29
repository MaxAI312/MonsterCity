using TMPro;
using UnityEngine;

public class EnemyCountView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textValue;
    [SerializeField] private GameObject _ui;

    private LevelObserver _levelObserver = null;

    private void Awake()
    {
        _levelObserver = FindObjectOfType<LevelObserver>();
    }

    private void OnEnable()
    {
        _levelObserver.KilledNpc += OnChanged;
    }

    private void OnDisable()
    {
        _levelObserver.KilledNpc -= OnChanged;
    }

    private void OnChanged(int enemyCount)
    {
        _textValue.text = enemyCount.ToString();
        _ui.gameObject.SetActive(true);
    }
}