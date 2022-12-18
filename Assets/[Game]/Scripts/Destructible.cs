using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] float destructionTime = 1f;

    void Start()
    {
        Destroy(gameObject, destructionTime);
    }
}
