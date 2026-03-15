using UnityEngine;
using TMPro;

public class PotionItem : Interactable
{
    public Material newMaterial; 
    public PhysicsMaterial newPhysics; 
    public AudioClip getSound;
    public AudioClip bounceSound;
    
    public GameObject screenText;
    
    public float newSpeed = 10f;
    public float newJumpForce = 7f; 
    public float newMass = 1f; 
    public int newMaxJumps = 1;
    
    public override void OnInteract(Player player)
    {
        if (screenText)
        {
            screenText.SetActive(true);
            // Le decimos al player que lo apague en 3 segundos (porque la poción se destruye)
            player.objectToClear = screenText;
            player.Invoke("ClearText", 3f);
        }
            
        // Aplicar las nuevas características a Player
        player.ApplyPotionStats(newMaterial, newSpeed, newJumpForce, newMass, newMaxJumps, bounceSound);

        // Le ponemos material físico si tiene
        Collider playerCollider = player.GetComponent<Collider>();
        if (playerCollider)
        {
            playerCollider.material = newPhysics;
        }

        // Sonido al obtener la poción
        if (AudioManager.instance && getSound)
        {
            AudioManager.instance.PlaySfx(getSound); 
        }

        // Destruir el frasco
        Destroy(gameObject);
    }
}
