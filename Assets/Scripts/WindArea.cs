using UnityEngine;

public class WindArea : MonoBehaviour
{
    public float windForce = 20f;
    private void OnTriggerStay(Collider other)
    {
        // Comprobamos si lo que ha entrado es el Player
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb)
            {
                // Aplicamos fuerza en la dirección "hacia adelante" del ventilador
                rb.AddForce(transform.forward * windForce, ForceMode.Force); // Force para que importe la masa de la pelota
            }
        }
    }
}
