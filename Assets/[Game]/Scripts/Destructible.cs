using UnityEngine;

public class Destructible : MonoBehaviour
{

    [SerializeField] float destructionTime = 1f;

    [Range(0f, 1f)] [SerializeField] float itemSpawnChance = .2f;
    
    [SerializeField] GameObject[] spawnableItems;
    void Start()
    {
        Destroy(gameObject, destructionTime);
    }

    void OnDestroy()
    {
        if (spawnableItems.Length > 0 && Random.value < itemSpawnChance)
        {
            int randomIndex = Random.Range(0, spawnableItems.Length);
            Instantiate(spawnableItems[randomIndex], transform.position, Quaternion.identity);
        }
    }

}
