using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AnimatedSpriteRenderer start;
    public AnimatedSpriteRenderer middle;
    public AnimatedSpriteRenderer end;

    public void SetActiveRenderer(AnimatedSpriteRenderer renderer) // there is 3 diffrent explosions sprite so set the expolisons which one is which
    {
        start.enabled = renderer == start;
        middle.enabled = renderer == middle;
        end.enabled = renderer == end; 
    }
    
    public void SetDirection(Vector2 direction) // set expolison angle
    {
        float angle = Mathf.Atan2(direction.y, direction.x);

        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg , Vector3.forward);
    }
    public void DestroyAfter(float seconds)
    {
        Destroy(gameObject, seconds);
    }
}
