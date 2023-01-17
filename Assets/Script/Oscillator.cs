using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;
    [SerializeField]  Vector3 movementVector;
     float  movementFactor;
    [SerializeField] float period = 2f;


    void Start()
    {
        startingPos = transform.position;

    }
     void Update()
    {
        if(period <= Mathf.Epsilon) { return; }
       
        float cycles = Time.time / period; // continually growing over time

        const float tau = Mathf.PI * 2;  // constant value of 6
        float rawSinWave = Mathf.Sin(cycles * tau); // going  -2 to 2

        movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1 so its cleaner

        Vector3 offSet = movementVector * movementFactor;
        transform.position = startingPos + offSet;
    }
}
