using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private float minSize = 0.3f;
    private float maxSize = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(size, size, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
