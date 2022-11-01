using UnityEngine;

public class WeaponMeshDisabler : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    
    //Animation is a handler
    public void DisableMesh()
    {
        _skinnedMeshRenderer.enabled = false;
    }
}
