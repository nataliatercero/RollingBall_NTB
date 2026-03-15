using UnityEngine;

public class VisionPotion : PotionItem
{
    public GameObject steps;
    
    public override void OnInteract(Player player)
    {
        // Llamamos al comportamiento base (Cambia material, masa, sonidos, etc.)
        base.OnInteract(player);

        // Añadimos el efecto de mostrar las escaleras
        if (steps)
        {
            steps.SetActive(true);
        }
    }
}
