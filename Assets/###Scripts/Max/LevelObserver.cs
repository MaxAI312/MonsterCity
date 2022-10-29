using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class LevelObserver : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;

    public Wallet Wallet => _wallet;

    private List<Target> _npcs = null;
    private List<Monster> _monsters = null;

    public int NpcCount { get; private set; }
    public int MonsterCount => _monsters.Count;

    public event Action<int> KilledNpc;
    public event Action AllKilledTargets;
    public event Action<int> KilledMonster;
    public event Action AllKilledMonsters;

    private void Awake()
    {
        _monsters = new List<Monster>();
        _npcs = new List<Target>();
    }

    public void Initialize()
    {
        var npcs = FindObjectsOfType<Npc>();
        var buildings = FindObjectsOfType<Building>();

        foreach (var npc in npcs)
            _npcs.Add(npc);

        foreach (var building in buildings)
            _npcs.Add(building);

        NpcCount = _npcs.Count;
    }

    public void RemoveUnit(Target unit)
    {
        if (unit is Building)
        {
            if(_npcs.Contains(unit))
                _npcs.Remove(unit);
            NpcCount = _npcs.Count;
            
            foreach (var monster in _monsters.Where(monster => monster.Target == null))
                monster.SetFindTargetState();
        }

        if (unit is Fence)
        {
            foreach (var monster in _monsters.Where(monster => monster.Target == null))
                monster.SetFindTargetState();
        }
        
        if (unit is Npc)
        {
            Npc npc = (Npc)unit;

            if (_npcs.Contains(unit))
            {
                _wallet.Fill(npc.Raward);
                _npcs.Remove(npc);
            }
            KilledNpc?.Invoke(_npcs.Count);
            NpcCount = _npcs.Count;
            //if (_npcs.Count <= 0)
            //{
            //    Debug.Log("AllKilledTargets");
            //    AllKilledTargets?.Invoke();
            //}    


            if (_npcs.Count > 0)
            {
                foreach (var monster in _monsters.Where(monster => monster.Target == null))
                    monster.SetFindTargetState();
            }
        }
        else if (unit is Monster)
        {
            Monster monster = (Monster)unit;
            _monsters.Remove(monster);

            if (_monsters.Count > 0)
            {
                foreach (var npc in _npcs.OfType<Npc>().Where(npc => npc.Target == null))
                    npc.SetFindTargetState();
            }
            else
            {
                foreach (var npc in _npcs.OfType<Npc>())
                {
                    npc.Animator.PlayIdle();
                    npc.NMeshAgent.enabled = false;
                }
            }
        }
        
        if(_wallet.Amount == 0 && _monsters.Count == 0)
            _wallet.Fill(50);
        
        if(_npcs.Count <= 0)
        {
            foreach (var t in _monsters)
            {
                t.Animator.PlayIdle();
                t.NMeshAgent.enabled = false;
            }
            Debug.Log("AllKilledTargets");
            AllKilledTargets?.Invoke();
        }
    }

    public void AddMonsterToCollection(Monster monster)
    {
        _monsters.Add(monster);

        // foreach (Npc target in _npcs)
        //     target.FindTargetState();
    }

    public Target FindNearestNpc(Monster monster)
    {
        float distanceToNearest = Mathf.Infinity;
        Target result = null;

        foreach (Target npc in _npcs)
        {
            float currentDistance = (npc.transform.position - monster.transform.position).magnitude;

            if (currentDistance < distanceToNearest)
            {
                result = npc;
                distanceToNearest = currentDistance;
            }
        }
        return result;
    }

























    //public void RemoveNpc(Npc npc)
    //{
    //    _wallet.Fill(npc.Raward);
    //    _npcs.Remove(npc);
    //    KilledNpc?.Invoke(_npcs.Count);
    //    NpcCount = _npcs.Count;

    //    foreach (Monster monster in _monsters)
    //        if (monster.IsAttack == false)
    //            monster.SetFindTargetState();
    //}

    //public void RemoveMonster(Monster monster)
    //{
    //    _monsters.Remove(monster);

    //    if (_monsters.Count > 0)
    //    {
    //        foreach (Npc npc in _npcs)
    //            if (npc.IsAttack == false)
    //                npc.SetFindTargetState();
    //    }
    //    //foreach (Npc npc in _npcs)
    //    //    if (npc.IsAttack == false)
    //    //        npc.SetFindTargetState();
    //    // foreach (Npc target in _npcs)
    //    //     target.FindTargetState();
    //}





























    //[SerializeField] private Wallet _wallet;

    //public Wallet Wallet => _wallet;

    //private List<Npc> _npcs = null;
    //private List<Monster> _monsters = null;

    //public int NpcCount { get; private set; }
    //public int MonsterCount => _monsters.Count;

    //public event Action<int> KilledNpc;

    //private void Awake()
    //{
    //    _monsters = new List<Monster>();
    //}

    //public void Initialize()
    //{
    //    _npcs = FindObjectsOfType<Npc>().ToList();
    //    NpcCount = _npcs.Count;
    //}

    //public void RemoveNpc(Npc npc)
    //{
    //    _wallet.Fill(npc.Raward);
    //    _npcs.Remove(npc);
    //    KilledNpc?.Invoke(_npcs.Count);
    //    NpcCount = _npcs.Count;

    //    foreach (Monster monster in _monsters)
    //        if (monster.IsAttack == false)
    //            monster.SetFindTargetState();
    //}

    //public void RemoveMonster(Monster monster)
    //{
    //    _monsters.Remove(monster);

    //    if (_monsters.Count > 0)
    //    {
    //        foreach (Npc npc in _npcs)
    //            if (npc.IsAttack == false)
    //                npc.SetFindTargetState();
    //    }
    //    //foreach (Npc npc in _npcs)
    //    //    if (npc.IsAttack == false)
    //    //        npc.SetFindTargetState();
    //    // foreach (Npc target in _npcs)
    //    //     target.FindTargetState();
    //}

    //public void AddMonsterToCollection(Monster monster)
    //{
    //    _monsters.Add(monster);

    //    // foreach (Npc target in _npcs)
    //    //     target.FindTargetState();
    //}

    //public Npc FindNearestNpc(Monster monster)
    //{
    //    float distanceToNearest = Mathf.Infinity;
    //    Npc result = null;

    //    foreach (Npc npc in _npcs)
    //    {
    //        float currentDistance = (npc.transform.position - monster.transform.position).magnitude;

    //        if (currentDistance < distanceToNearest)
    //        {
    //            result = npc;
    //            distanceToNearest = currentDistance;
    //        }
    //    }

    //    return result;
    //}
}