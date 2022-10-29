using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 rotor = Vector3.up;
   
    void Update()
    {
        transform.eulerAngles += rotor * Time.deltaTime;
    }
}
