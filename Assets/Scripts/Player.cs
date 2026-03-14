using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 7f;
    
    private Rigidbody rb;
    public static Player Instance;
    
    public bool hasKey = false;
    
    private int jumpsRemaining;
    private const int MAX_JUMPS = 2; // Para doble salto
    private bool isGrounded;
    
    
    private float hInput;
    private float vInput;
    
    private Transform camTransform;

    void Awake()
    {
        Instance = this;
        // Para evitar el saltito del principio y que aparezca en medio de la sala
        transform.position = new Vector3(0.427f, 0.543f, -1.506f);
        rb = GetComponent<Rigidbody>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jumpsRemaining = MAX_JUMPS;
        
        if (Camera.main != null) 
        {
            camTransform = Camera.main.transform;
        }
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
        if (camTransform)
        {
            // Calculamos direcciones relativas a la cámara
            Vector3 forward = camTransform.forward;
            Vector3 right = camTransform.right;

            // "Aplanamos" para que no salte al presionar W
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            // Dirección final
            Vector3 direction = (forward * vInput + right * hInput).normalized;
            rb.AddForce(direction * speed, ForceMode.Force);
        }
    }
    
    private bool IsGrounded()
    {
        // Lanzamos el rayo desde el centro hacia abajo
        // La distancia es la mitad de la escala (para abarcar solo el radio) + un pequeño margen (0.1f)
        float rayDistance = (transform.localScale.y / 2f) + 0.1f;
        return Physics.Raycast(transform.position, Vector3.down, rayDistance);
    }
}
