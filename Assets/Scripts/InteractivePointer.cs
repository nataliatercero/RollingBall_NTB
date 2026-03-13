using UnityEngine;
using UnityEngine.UI;

public class InteractivePointer : MonoBehaviour
{
    public Image pointer;
    public Color normalColor = Color.white;
    public Color interactColor = Color.red;
    
    public float interactDistance = 10f; // Distancia máxima para interactuar
    private Camera myCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        myCamera = GetComponent<Camera>();
    }

    void Start()
    {
        // Oculta el cursor original
        Cursor.visible = false;
        
        // Bloquea el cursor para que no se salga de la ventana
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        // Hacer que la imagen del puntero siga la posición del ratón
        if (pointer)
        {
            pointer.transform.position = Input.mousePosition;
        }

        // Lanzar un Rayo desde la cámara hacia la posición del ratón
        if (myCamera)
        {
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Lógica de detección
            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                // Comprobamos si el objeto tiene el Tag adecuado
                if (hit.collider.CompareTag("Interactive"))
                {
                    pointer.color = interactColor;

                    // Si hacemos clic izquierdo
                    if (Input.GetMouseButtonDown(0))
                    {
                        Interact(hit.collider.gameObject);
                    }
                }
                else
                {
                    pointer.color = normalColor;
                }
            }
            else
            {
                pointer.color = normalColor;
            }
        }
    }

    void Interact(GameObject objectInteract)
    {
        Debug.Log("Has interactuado con " + objectInteract.name);
        // Iré añadiendo cosas cuando tenga más objetos con los que interactuar
    }
}