using System;
using UnityEngine;
using DG.Tweening;

public class Shop : MonoBehaviour
{
    [SerializeField] private Data _data;
    [SerializeField] private MonsterView[] _views;
    [SerializeField] private bool _debug;

    private Vector3 _scaleSelectedCard = new Vector3(1.3f, 1.3f, 1.3f);
    private Vector3 _scaleUnSelectedCard = new Vector3(1f, 1f, 1f);
    
    public event Action<IMonsterData> ItemSelected;

    private void Start()
    {
        EnableCardsByCompletedLevels();
    }

    private void OnEnable()
    {
        foreach (var view in _views)
        {
            view.ItemSelected += OnItemSelected;
            view.ViewSelected += OnSetGlow;
        }

        OnItemSelected(_views[0]);
    }

    private void OnDisable()
    {
        foreach (var view in _views)
        {
            view.ItemSelected -= OnItemSelected;
            view.ViewSelected -= OnSetGlow;
        }
    }

    private void OnItemSelected(IMonsterData data) => ItemSelected?.Invoke(data);

    private void OnSetGlow(MonsterView monsterView)
    {
        float timeToScale = 0.3f;
        
        foreach (var view in _views)
        {
            if (monsterView == view)
            {
                view.transform.DOScale(_scaleSelectedCard, timeToScale);

                view.SetGlow(true);
            }
            else
            {
                view.transform.DOScale(_scaleUnSelectedCard, timeToScale);

                view.SetGlow(false);
            }
        }
    }

    private void EnableCardsByCompletedLevels()
    {
        if (_debug)
        {
            foreach (var view in _views)
                view.gameObject.SetActive(true);
            return;
        }
        
        for (var i = 0; i < _views.Length; i++)
            _views[i].gameObject.SetActive(i < _data.GetDisplayedLevelNumber());
    }
}