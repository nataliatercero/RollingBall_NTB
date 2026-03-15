using UnityEngine;

public class KeyItem : Interactable
{
    public AudioClip pickupSound;
    public override void OnInteract(Player player)
    {
        if(pickupSound)
        {
            AudioManager.Instance.PlaySfx(pickupSound);
        } 
        // Ponemos a true la variable en PLayer
        player.hasKey = true;
        // Destruimos la llave cuando es recogida
        Destroy(gameObject);
    }
}
