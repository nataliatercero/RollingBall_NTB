using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Vector3 direction = Vector3.right; // Hacia donde se mueve
    public float distanceFromOrigin = 5f;     // Cuánto se aleja del inicio
    public float speed = 2f;
    
    private Vector3 initialPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculamos el movimiento ida y vuelta
        float factor = Mathf.Sin(Time.time * speed) * distanceFromOrigin;
        transform.position = initialPosition + (direction * factor);
    }

    // Hacer que la bola se mueva con la plataforma
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Hacemos que la bola sea "hija" de la plataforma
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Al saltar o irse, la bola deja de ser hija
            collision.gameObject.transform.SetParent(null);
        }
    }
}
