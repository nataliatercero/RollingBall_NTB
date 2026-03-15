using UnityEngine;

public class SpikePendule : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Comprobamos si lo que hemos tocado tiene el Tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.instance)
            {
                GameManager.instance.GameOver(); 
            }
        }
    }
}
