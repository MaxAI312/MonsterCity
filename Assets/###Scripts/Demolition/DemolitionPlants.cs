using UnityEngine;

public class DemolitionPlants : MonoBehaviour
{
    [SerializeField] private float _durationBeforeDestroy = 0.5f;
    
    private ParticleSystem _particle;
    
    private void Start()
    {
        _particle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider colliderBody)
    {
        if (colliderBody.gameObject.TryGetComponent(out Monster monster))
        {
            _particle.Play();

            Destroy(gameObject, _durationBeforeDestroy);
        }
    }
}