using UnityEngine;

public class LavaPit : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        // Comprobamos si lo que ha caído es el jugador
        if (other.CompareTag("Player"))
        {
            // Simular densidad
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearDamping = 20f; // Como caer en miel (resistencia alta)
            }
            
            // Invooke sirve para llamar a algo despues de un tiempo determinado
            Invoke("LlamarGameOver", 1.5f);
        }
        
    }

    private void LlamarGameOver()
    {
        // Llamamos directamente al GameManager
        if (GameManager.instance)
        {
            GameManager.instance.GameOver(); 
        }
    }
}
