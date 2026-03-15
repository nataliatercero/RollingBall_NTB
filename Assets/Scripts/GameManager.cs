using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public GameObject gameOverPanel;
    public Animator fadeAnimator;
    
    public bool hasSavedData = false;
    public Material savedMaterial;
    public float savedSpeed = 10f;
    public float savedJumpForce = 7f;
    public float savedMass = 1f;
    public int savedMaxJumps = 2;
    public PhysicsMaterial savedPhysicMaterial; 
    public AudioClip savedBounceSound;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sobrevive entre niveles
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Pausamos el juego
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
        // Recarga el nivel actual con el fade
        StartCoroutine(LoadSceneRoutine(SceneManager.GetActiveScene().buildIndex));
    }
    
    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        // Carga el siguiente nivel en el orden
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadSceneRoutine(nextScene));
    }
    
    public void StartGame()
    {
        // Del Menú al Nivel 1
        StartCoroutine(LoadSceneRoutine(1));
    }
    
    // Corrutina para el fade
    private IEnumerator LoadSceneRoutine(int sceneIndex)
    {
        // Animación de oscurecer
        fadeAnimator.SetTrigger("FadeOut");
        
        // Espera 1 segundo (lo que dura la animación)
        yield return new WaitForSecondsRealtime(1f);

        // Carga la escena
        SceneManager.LoadScene(sceneIndex);

        // Animación de aclarar
        fadeAnimator.SetTrigger("FadeIn");
    }
    
    public void SavePlayerState(Material mat, float speed, float jump, float mass, int maxJ, PhysicsMaterial physMat, AudioClip bounceSound)
    {
        savedMaterial = mat;
        savedSpeed = speed;
        savedJumpForce = jump;
        savedMass = mass;
        savedMaxJumps = maxJ;
        savedPhysicMaterial = physMat;
        savedBounceSound = bounceSound;
        hasSavedData = true;
    }
}
