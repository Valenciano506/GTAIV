using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField]
    private float minSize = 0.01f;
    [SerializeField]
    private float maxSize = 0.05f;
    [SerializeField]
    private float minForce = 20f;
    [SerializeField]
    private float maxForce = 48f;
    [SerializeField]
    private float minTorque = 20f;
    [SerializeField]
    private float maxTorque = 48f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Asing a random size to each obstacle object
        float size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(size, size, 1);
        // Get the rigidbody component on our game object
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Transform tf = GetComponent<Transform>();
        Vector2 randomDirection = Random.insideUnitCircle;
        // Generate a random force
        float force = Random.Range(minForce, maxForce);
        rb.AddForce(randomDirection * force);
        // Apply a random torque for each obstacle
        float torque = Random.Range(minTorque, maxTorque);
        rb.AddTorque(-torque);

        Debug.Log(transform == tf);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
