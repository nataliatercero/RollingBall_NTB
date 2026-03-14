using UnityEngine;

public class CloseNoteButton : MonoBehaviour
{
    public void Close()
    {
        // Apaga el LetterContainer
        gameObject.SetActive(false);
        
        // Reanudar tiempo
        Time.timeScale = 1f;
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}