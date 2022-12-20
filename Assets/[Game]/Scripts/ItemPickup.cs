using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType
    {
        ExtraBomb,
        BlastRadius,
        SpeedIncrease
    }
    public ItemType type;

    private void OnItemPickup(GameObject player)
    {
        switch (type)
        {
            case ItemType.BlastRadius:
            Debug.Log("inswitch");
            player.GetComponent<BombController>().explosionRadius++;
            break;
            case ItemType.ExtraBomb:
            Debug.Log("inswitch");
            player.GetComponent<BombController>().AddBomb();
            break;
            case ItemType.SpeedIncrease:
            Debug.Log("inswitch");
            player.GetComponent<MovementController>().speed++;
            break;
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnItemPickup(other.gameObject);
        }
    }

}
