using UnityEngine;

public class ReboundObstacle : MonoBehaviour
{
    public float speed = 8f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Vector3 dir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        dir.Normalize();

        rb.linearVelocity = dir * speed;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = rb.linearVelocity.normalized * speed;
    }
}
