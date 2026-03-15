using UnityEngine;

public class HingeDoor : Interactable
{
    public AudioClip openSound;
    public AudioClip lockedSound;
    
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
            if(openSound)
            {
                AudioManager.instance.PlaySfx(openSound);
            }
            OpenDoor();
        }
        else
        {
            if(openSound)
            {
                AudioManager.instance.PlaySfx(lockedSound);
            }
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
    }
}
