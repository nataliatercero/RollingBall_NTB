using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 7f;
    
    private Rigidbody rb;
    private int jumpsRemaining;
    private const int MAX_JUMPS = 2; // Para doble salto
    private bool isGrounded;
    private float hInput;
    private float vInput;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpsRemaining = MAX_JUMPS;
    }

    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Si estamos en el suelo, reseteamos los saltos a MAX_JUMPS antes de saltar
            if (IsGrounded())
            {
                jumpsRemaining = MAX_JUMPS;
            }

            // Si aún nos quedan saltos, permitimos saltar
            if (jumpsRemaining > 0)
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        // Limpiamos la velocidad vertical para que el segundo salto siempre tenga la misma fuerza
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        
        // Aplicamos el impulso hacia arriba
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        
        // Restamos un salto disponible
        jumpsRemaining--;
    }

    void FixedUpdate()
    {
        Vector3 direction = new Vector3(hInput, 0, vInput).normalized;
        rb.AddForce(direction * speed, ForceMode.Force);
    }
    
    private bool IsGrounded()
    {
        // Lanzamos el rayo desde el centro hacia abajo
        // La distancia es la mitad de la escala (para abarcar solo el radio) + un pequeño margen (0.1f)
        float rayDistance = (transform.localScale.y / 2f) + 0.1f;
        return Physics.Raycast(transform.position, Vector3.down, rayDistance);
    }
}
