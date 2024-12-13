using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Movement : MonoBehaviour
{

    // Parameters - for tuning, typically set in the editor
    // CACHE - e.g. references for redability and speed
    // STATE - private instance (member) variables

    [SerializeField] float mainThrust = 100;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem MainBooster;
    [SerializeField] ParticleSystem LeftBooster;
    [SerializeField] ParticleSystem RightBooster;

    
    Rigidbody rb;
    AudioSource audioSource;

   

    bool isAlive;
    // Start is called before the first frame update
    void Start()
    {
         // We are getting the rigidbody component.
        // Caching a reference to our component.
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      // Calling a method call
      ProcessThrusts();
      ProcessRotation();
      
    }

    void ProcessThrusts()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Adding Time.deltaTime to make it frame independent
            // mainthrust to move an object quickly 
            StartThrusting();// Checks if the rocket booster particle is not playing,If yes there will be a particle effect

        }
        else
        {
            StopThrusting();
        }
    }


    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
            StopRotating();
    }

   
    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!MainBooster.isPlaying) { MainBooster.Play(); }
    }


      void StopThrusting()
    {
        audioSource.Stop();
        MainBooster.Stop(); // Once the space button is released the particle effect gets stopped
    }
     void RotateLeft()
    {
        // Moves clockwise
        ApplyRotation(rotationThrust);
        if (!LeftBooster.isPlaying) // Checks if the rocket booster left particle is not playing, If yes there will be a particle effect
        {
            LeftBooster.Play(); // Play the Particle
        }
    }

     void RotateRight()
    {
        // Moves anti/counter clockwise
        ApplyRotation(-rotationThrust);
        if (!RightBooster.isPlaying) // Checks if the rocket booster right particle is not playing, If yes there will be a particle effect
        {
            RightBooster.Play(); // Play the Particle
        }
    }
     void StopRotating()
    {
        LeftBooster.Stop(); // If the key press is released Stop the particle effect
        RightBooster.Stop(); // If the key press is released Stop the particle effect
    }

    void ApplyRotation(float RotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * RotationThisFrame * Time.deltaTime); 
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over.
    }

 

}
