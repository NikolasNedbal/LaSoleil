using UnityEngine;

public class ChairLayering : MonoBehaviour
{
    public BoxCollider2D upperCol;
    public PolygonCollider2D lowerCol;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Coll enter");

        if (upperCol.IsTouching(collision))
        {
            Debug.Log("Upper Collider triggered");
            sr.sortingOrder = 50;
        }
        else if (lowerCol.IsTouching(collision))
        {
            Debug.Log("Lower Collider triggered");
            sr.sortingOrder = 0;
        }
    }
}
