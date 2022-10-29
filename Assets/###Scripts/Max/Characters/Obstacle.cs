using RayFire;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private RayfireRigid _rayfire;
    private RayfireBomb _bomb;

    public ClampedAmount Health = new ClampedAmount(100, 0, 100);

    private void Awake()
    {
        _rayfire = GetComponent<RayfireRigid>();
        _bomb = GetComponent<RayfireBomb>();
    }


    public  void TryDestroy()
    {
        // if (Health.Amount == 0)
        {
            BlowUp();
    
            // LevelObserver.RemoveNpc(this);
            Destroy(gameObject, 1.5f);
        }
    }

    private void BlowUp()
    {
        _rayfire.Demolish();
    
        _bomb.Explode(0);
    }
}