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
    [SerializeField]
    private ParticleSystem hitEffect; // Partículas del choque con la pared

    void Start()
    {
        // Asigna un tamaño aleatorio a cada obstáculo
        float size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(size, size, 1);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 randomDirection = Random.insideUnitCircle;

        // Fuerza aleatoria
        float force = Random.Range(minForce, maxForce);
        rb.AddForce(randomDirection * force);

        // Torque aleatorio
        float torque = Random.Range(minTorque, maxTorque);
        rb.AddTorque(-torque);

        rb.mass = size * 2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si choca con otro obstáculo o con el jugador, no hacemos nada
        if (collision.gameObject.GetComponent<ObstacleController>() != null ||
            collision.gameObject.GetComponent<PlayerController>() != null)
        {
            return;
        }

        // Lo demás se considera pared: partículas en el punto exacto del choque
        Vector3 hitPoint = collision.GetContact(0).point;
        hitPoint.z = 0f;

        ParticleSystem fx = Instantiate(hitEffect, hitPoint, Quaternion.identity);
        fx.Play();

        // Se borra el efecto al terminar; el obstáculo NO desaparece
        Destroy(fx.gameObject, fx.main.duration + fx.main.startLifetime.constantMax);
    }
}