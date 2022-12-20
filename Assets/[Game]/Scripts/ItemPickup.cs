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
        //check which item the player picks up and change the values after picking up destroy the item
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

    void OnTriggerEnter2D(Collider2D other) //if the item collide with collide with tag which is player execute OnItemPickup function
    {
        if (other.CompareTag("Player"))
        {
            OnItemPickup(other.gameObject);
        }
    }

}
