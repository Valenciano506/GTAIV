using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float force = 2;
    public Rigidbody2D rb;
    [SerializeField]
    private float maxSpeed = 4;

    private float elapsedTime = 0f;
    [SerializeField]
    private UIDocument uiDoc;

    private Label scoreLabel;
    [SerializeField]
    private ParticleSystem explosionEffect;
    [SerializeField]
    private AudioSource audioSource;

    void Start()
    {
        scoreLabel = uiDoc.rootVisualElement.Q<Label>();
    }

    void UpdateScore()
    {
        elapsedTime += Time.deltaTime;
        scoreLabel.text = "Score: " + Mathf.FloorToInt(elapsedTime);
    }

    void UpdateMovement()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            // Calculate the direction from the player to the mouse
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Vector2 dir = (mousePos - transform.position).normalized;
            transform.up = dir;
            rb.AddForce(dir * force);
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
    }

    void Update()
    {
        UpdateScore();
        UpdateMovement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        audioSource.Play();
        // Solo explota si choca con un obstáculo
        if (collision.gameObject.GetComponent<ObstacleController>() == null)
        {
            return;
        }

        // Punto exacto del choque (z = 0 para que se vea en 2D)
        Vector3 hitPoint = collision.GetContact(0).point;
        hitPoint.z = 0f;

        // Partículas justo en el punto de la explosión
        ParticleSystem fx = Instantiate(explosionEffect, hitPoint, Quaternion.identity);
        fx.Play();

        // Elimina el efecto cuando termine para no dejar objetos sueltos
        Destroy(fx.gameObject, fx.main.duration + fx.main.startLifetime.constantMax);

        // El jugador desaparece
        Destroy(gameObject);
    }
}