using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    Transform player;
    [SerializeField] float speed = 5f;
    [SerializeField] float pickUpDistance = 1.5f;
    [SerializeField] float timeToLeave = 10f;

    private float distance;

    public Item item;
    public int count = 1;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameManager.Instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        timeToLeave -= Time.deltaTime;
        if(timeToLeave < 0) 
        {
            Destroy(gameObject); 
        }

        distance = Vector3.Distance(transform.position, player.position);
        if (distance > pickUpDistance) 
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        if(distance < 0.1f)
        {
            if (GameManager.Instance.invContainer != null)
            {
                GameManager.Instance.invContainer.Add(item, count);
            }
            else
            {
                Debug.Log("!");
            }
            Destroy(gameObject);
        }
    }
}
