using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float force = 2;
    public Rigidbody2D rb;
    [SerializeField]
    private float maxSpeed = 4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            //Calculate the direction from the player to the mouse
            Debug.Log("The left buton is clicked");
            Debug.Log("The current mouse position is: " + Mouse.current.position.value);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Debug.Log("The world position of the mouse is: " + mousePos);
            Vector2 dir = (mousePos = gameObject.transform.position).normalized;
            Debug.Log("The direction to the mouse is: " + dir.sqrMagnitude);
            transform.up = dir;
            rb.AddForce(dir * force);
            if(rb.linearVelocity.magnitude > maxSpeed){ 
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Destroy(gameObject);

    }

}
