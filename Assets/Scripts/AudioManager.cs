using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxSource;
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        // Si ya existe uno, destruye el nuevo para que no se repitan
        if (instance == null)
        {
            instance = this;
            // Hace que el sonido no se corte al cambiar de escena
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // El método para llamarlo desde otros scripts
    public void PlaySfx(AudioClip clipToPlay)
    {
        if (clipToPlay)
        {
            // Permite que los sonidos se solapen sin cortarse
            sfxSource.PlayOneShot(clipToPlay);
        }
    }
}