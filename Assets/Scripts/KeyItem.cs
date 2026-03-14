using UnityEngine;

public class KeyItem : Interactable
{
    [SerializeField]public AudioClip pickupSound;
    public override void OnInteract(Player player)
    {
        if(pickupSound)
        {
            AudioManager.Manager.PlaySfx(pickupSound);
        } 
        // Ponemos a true la variable en PLayer
        player.hasKey = true;
        // Destruimos la llave cuando es recogida
        Destroy(gameObject);
    }
}
