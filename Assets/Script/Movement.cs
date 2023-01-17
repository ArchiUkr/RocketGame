using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{


    [SerializeField] float rocketPower = 100f;
    [SerializeField] float rocketRotation = 1f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    Rigidbody rb;
    AudioSource audioSource;

    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RocketMovement();
        RocketPower();
    }

    void RocketPower()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Startthrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    void RocketMovement()
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
        {
            StopRotatating();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    void Startthrusting()
    {
        rb.AddRelativeForce(Vector3.up * rocketPower * Time.deltaTime);
        //Debug.Log("Pressed Space - Fly");
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    

    private void StopRotatating()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }

    private void RotateRight()
    {
        ApplyRotation(-rocketRotation);
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(rocketRotation);
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }

    public void ApplyRotation(float rotationRocketFrame)
    {
        rb.freezeRotation = true; // freze rotation . can manually rotate
        transform.Rotate(Vector3.forward * rotationRocketFrame * Time.deltaTime);

        rb.freezeRotation = false; // unfreze

    }
}
