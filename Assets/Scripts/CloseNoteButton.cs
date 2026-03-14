using UnityEngine;

public class CloseNoteButton : MonoBehaviour
{
    [SerializeField]public AudioClip closeSound;
    
    public void Close()
    {
        if (closeSound)
        {
            AudioManager.Manager.PlaySfx(closeSound);
        }
        
        // Apaga el LetterContainer
        gameObject.SetActive(false);
        
        // Reanudar tiempo
        Time.timeScale = 1f;
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}