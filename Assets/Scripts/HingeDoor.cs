using UnityEngine;

public class HingeDoor : Interactable
{
    private HingeJoint hinge;
    private JointMotor motor;
    private bool isOpening = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        hinge = GetComponent<HingeJoint>();
        GetComponent<Rigidbody>().isKinematic = true;
    }
    
    public override void OnInteract(Player player)
    {
        if (player.hasKey)
        {
            OpenDoor();
        }
        else
        {
            Debug.Log("Necesitas la llave para abrir esta celda.");
        }
    }

    void OpenDoor()
    {
        if (isOpening) return; 

        isOpening = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false; // Liberamos

        // Configuramos el motor para que se mueva sola
        hinge.useMotor = true;
        motor = hinge.motor;
        
        motor.targetVelocity = -100f; 
        motor.force = 20f; 
        hinge.motor = motor;
        
        Debug.Log("Abriendo...");

    }
}
