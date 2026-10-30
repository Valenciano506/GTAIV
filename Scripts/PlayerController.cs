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
    private AudioSource audioSource;   // Sonido de la explosión
    [SerializeField]
    [Range(0f, 1f)]
    private float explosionVolume = 1f;

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
        // Solo explota si choca con un obstáculo
        if (collision.gameObject.GetComponent<ObstacleController>() == null)
        {
            return;
        }

        // Punto exacto del choque (z = 0 para que se vea en 2D)
        Vector3 hitPoint = collision.GetContact(0).point;
        hitPoint.z = 0f;

        // 1) Partículas justo en el punto de la explosión
        ParticleSystem fx = Instantiate(explosionEffect, hitPoint, Quaternion.identity);
        fx.Play();
        Destroy(fx.gameObject, fx.main.duration + fx.main.startLifetime.constantMax);

        // 2) Sonido de la explosión (suena aunque el jugador se destruya)
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // 3) El jugador desaparece
        Destroy(gameObject, 0.2f);
    }
}