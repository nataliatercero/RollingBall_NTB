using UnityEngine;

public class Note : Interactable
{
    [SerializeField]public GameObject letterContainer;
    [SerializeField]public AudioClip openSound;

    public override void OnInteract(Player player)
    {
        // Antes de pausar el juego
        if(openSound)
        {
            AudioManager.Manager.PlaySfx(openSound);
        } 
        // Mostrar la nota
        letterContainer.SetActive(true);

        // Pausar el juego
        Time.timeScale = 0f;

        // Liberar el ratón para poder dar a la X
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
