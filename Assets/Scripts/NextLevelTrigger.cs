using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    private bool hasTriggered = false; // Para que no cargue el nivel más de una vez aunque sigas encima del suelo
    private void OnTriggerEnter(Collider other)
    {
        // Comprobamos si el que ha pisado el suelo es el Player y si no hemos activado ya el cambio
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; // Bloqueamos para que no vuelva a entrar aquí

            // Para que el jugador guarde su textura y físicas actuales en el GameManager
            if (Player.Instance != null)
            {
                Player.Instance.SaveCurrentStateToManager();
            }

            // Llamar al GameManager para que ponga el fade y pase de nivel
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoadNextLevel();
            }
        }
    }
}
