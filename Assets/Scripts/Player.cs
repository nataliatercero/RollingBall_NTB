using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private int maxJumps = 2;
    
    private Rigidbody rb;
    private MeshRenderer meshRenderer;
    public static Player Instance;
    private Vector3 initialPosition { get; set; }
    public GameObject objectToClear;
    
    public bool hasKey = false;
    
    private int jumpsRemaining;
    private bool isGrounded;
    
    
    private float hInput;
    private float vInput;
    
    private Transform camTransform;
    
    private AudioClip currentBounceSound;

    void Awake()
    {
        Instance = this;
        meshRenderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jumpsRemaining = maxJumps;
        
        if (Camera.main) 
        {
            camTransform = Camera.main.transform;
        }
        
        initialPosition = transform.position;

        // 2. Cargar los datos del GameManager (Textura y físicas de inicio de nivel)
        if (GameManager.instance && GameManager.instance.hasSavedData)
        {
            ApplyPotionStats(
                GameManager.instance.savedMaterial,
                GameManager.instance.savedSpeed,
                GameManager.instance.savedJumpForce,
                GameManager.instance.savedMass,
                GameManager.instance.savedMaxJumps,
                GameManager.instance.savedBounceSound
            );
            
            // Recuperamos el material físico de rebote (si lo teníamos)
            if (GameManager.instance.savedPhysicMaterial)
            {
                Collider myCollider = GetComponent<Collider>();
                if (myCollider)
                {
                    myCollider.material = GameManager.instance.savedPhysicMaterial;
                }
            }
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
                jumpsRemaining = maxJumps;
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
    
    // Cualquier poción llamará a esta función para transformar al jugador
    public void ApplyPotionStats(Material newMat, float newSpeed, float newJump, float newMass, int newMaxJumps)
    {
        if (newMat) meshRenderer.material = newMat;
        speed = newSpeed;
        jumpForce = newJump;
        rb.mass = newMass;
        maxJumps = newMaxJumps;
    }
    
    // Enviarle los datos actuales al GameManager al pasar de nivel
    public void SaveCurrentStateToManager()
    {
        if (GameManager.instance)
        {
            // Cogemos el material físico actual de nuestro collider
            Collider myCollider = GetComponent<Collider>();
            PhysicsMaterial currentPhysMat = null;
            
            if (myCollider)
            {
                currentPhysMat = myCollider.material;
            }

            // Se lo mandamos al GameManager
            GameManager.instance.SavePlayerState(
                meshRenderer.material, 
                speed, 
                jumpForce, 
                rb.mass, 
                maxJumps,
                currentPhysMat, 
                currentBounceSound 
            );
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // relativeVelocity mide la fuerza del golpe.
        // Le pongo > 3f para que no suene infinitamente solo por estar rodando por el suelo.
        if (currentBounceSound && collision.relativeVelocity.magnitude > 3f)
        {
            if (AudioManager.instance)
            {
                AudioManager.instance.PlaySfx(currentBounceSound);
            }
        }
    }
    
    public void ApplyPotionStats(Material newMat, float newSpeed, float newJump, float newMass, int newMaxJumps, AudioClip newBounceSound)
    {
        if (newMat)
        {
            meshRenderer.material = newMat;
        }
        speed = newSpeed;
        jumpForce = newJump;
        rb.mass = newMass;
        maxJumps = newMaxJumps;
        
        // Guardamos el sonido que nos ha dado la poción
        currentBounceSound = newBounceSound; 
    }
    
    public void ClearText()
    {
        if (objectToClear)
        {
            objectToClear.SetActive(false);
        }
    }
}
