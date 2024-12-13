using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 StartingPosition;
   [SerializeField]  Vector3 MovementVector;
  // [SerializeField] [Range(0,1)] float MovementFactor;
    float MovementFactor;
   [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        StartingPosition = transform.position; // This is the current position
   }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) {return;} // Removes the NaN Error message
        {float cycles = Time.time / period;  // Continually growing over time



         // Const is a value that doesn't change
        // It represents the circle constant 
        const float tau = Mathf.PI * 2; // Constant value of 6.283 (3.14 * 2)

        float rawSinWave = Mathf.Sin(cycles * tau); // Recalculated to go from 0 to 1 so its cleaner

         MovementFactor = (rawSinWave + 1f) / 2f;
        }
       

        Vector3 offset = MovementVector * MovementFactor; // First Calculate the Offset.
        transform.position = StartingPosition + offset; // After that change the transform.position to the new position
    }
}
