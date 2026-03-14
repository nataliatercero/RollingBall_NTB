using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // Este método lo rellenará cada objeto con su propia lógica
    public abstract void OnInteract(Player player);
}
