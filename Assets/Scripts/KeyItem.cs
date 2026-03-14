using UnityEngine;

public class KeyItem : Interactable
{
    public override void OnInteract(Player player)
    {
        // Ponemos a true la variable en PLayer
        player.hasKey = true;
        // Destruimos la llave cuando es recogida
        Destroy(gameObject);
    }
}
