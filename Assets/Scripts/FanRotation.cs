using UnityEngine;

public class FanRotation : MonoBehaviour
{
    public float spinSpeed = 500f;
    private float zAngle = 0f;

    // Update is called once per frame
    void Update()
    {
        zAngle += spinSpeed * Time.deltaTime;
        transform.localEulerAngles = new Vector3(0, 0, zAngle);
    }
}
