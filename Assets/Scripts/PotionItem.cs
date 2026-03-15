using UnityEngine;

public class PotionItem : Interactable
{
    public Material newMaterial; 
    public PhysicsMaterial newPhysics; 
    public AudioClip getSound;
    public AudioClip bounceSound;
    
    public float newSpeed = 10f;
    public float newJumpForce = 7f; 
    public float newMass = 1f; 
    public int newMaxJumps = 1;
    
    public override void OnInteract(Player player)
    {
        // Aplicar las nuevas características a Player
        player.ApplyPotionStats(newMaterial, newSpeed, newJumpForce, newMass, newMaxJumps, bounceSound);

        // Le ponemos material físico si tiene
        Collider playerCollider = player.GetComponent<Collider>();
        if (playerCollider)
        {
            playerCollider.material = newPhysics;
        }

        // Sonido al obtener la poción
        if (AudioManager.Instance && getSound)
        {
            AudioManager.Instance.PlaySfx(getSound); 
        }

        // Destruir el frasco
        Destroy(gameObject);
    }
}
