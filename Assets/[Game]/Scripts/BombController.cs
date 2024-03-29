using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bombPrefab;
    public float bombFuseTime = 3f;
    public int bombAmount = 2;
    public KeyCode inputKey = KeyCode.Space;
    private int bombsRemaning;

    [Header("Explosion")]
    
    [SerializeField] Explosion explosionPrefab;
    [SerializeField] LayerMask explosionLayerMask;
    [SerializeField] float explosionDuration = 1f;
    public int explosionRadius = 1;

    [Header("Destructible")]
    [SerializeField] Tilemap destructibleTiles;
    [SerializeField] Destructible destructiblePrefab;

    void OnEnable()
    {
        bombsRemaning = bombAmount;
    }

    void Update()
    {
        if (bombsRemaning > 0 && Input.GetKeyDown(inputKey))
        {
            StartCoroutine(PlaceBomb());
        }
    }

    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x); // mathf round is make the float numbers to int numbers so when we tyr to place bomb it will automaticly placed center of a tile
        position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bombsRemaning--;

        yield return new WaitForSeconds(bombFuseTime);

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x); 
        position.y = Mathf.Round(position.y);
        
        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        Destroy(bomb);
        bombsRemaning++;
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }

    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }
        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2, 0f, explosionLayerMask))
        {
            ClearDestructible(position);
            return;    
        }

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end); // "?" mean if ":" mean else
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, direction , length - 1 ); //make this function recursive it will execute explosion radius time. (it start 1, when you pick up blast radious item explosion radius increase 1 so it will execute 2 time ect.) 
    }

    private void ClearDestructible(Vector2 position) 
    {
        Vector3Int cell = destructibleTiles.WorldToCell(position);
        TileBase tile  = destructibleTiles.GetTile(cell);

        if (tile != null)
        {
            Instantiate(destructiblePrefab, position, Quaternion.identity);
            destructibleTiles.SetTile(cell, null);
        }
    }

    public void AddBomb()
    {
        bombAmount++;
        bombsRemaning++;
    }

}
