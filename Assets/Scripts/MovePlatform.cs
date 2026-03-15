using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Vector3 direction = Vector3.right; // Hacia donde se mueve
    public float distanceFromOrigin = 5f;     // Cuánto se aleja del inicio
    public float speed = 2f;
    
    private Vector3 initialPosition;
    
    void Start()
    {
        initialPosition = transform.position;
    }

    
    void FixedUpdate()
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
            // Evito que la plataforma deforme a la bola haciendo un Empty Object y añadiendo el RB y el Script a este (con escala 1, 1, 1)
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
