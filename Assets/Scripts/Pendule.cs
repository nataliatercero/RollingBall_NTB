using UnityEngine;

public class Pendule : MonoBehaviour
{
    public float speed = 2.0f;   
    public float width = 30.0f;
    

    // Update is called once per frame
    void Update()
    {
        // Uso Mathf.Sin para que el número suba y baje solo entre -1 y 1
        float movimiento = Mathf.Sin(Time.time * speed);

        // Multiplico ese movimiento por los grados (amplitud)
        float anguloFinal = movimiento * width;

        // Aplicamos en Z, ponemos 0 a X e Y para que no haga cosas raras
        transform.localEulerAngles = new Vector3(0, 0, anguloFinal); 
    }
}
