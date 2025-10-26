using UnityEngine;

public class SimpleFollowPlayer : MonoBehaviour
{
    private Transform player;
    public Rigidbody rb;
    public float speed = 5f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 dir = player.position - rb.position;
            Vector3 nextPos = transform.position + dir * speed * Time.deltaTime;
            rb.MovePosition(nextPos);
        }
    }
}
