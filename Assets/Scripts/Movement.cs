using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float rotationThrust = 100f;
    [SerializeField] float mainThrust = 800f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            StartLeftRotation();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            StartRightRotation();
        }
        else
        {
            StopRotation();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;  // Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // Allowing physics system to take over again
    }

    void StartThrusting()
    {
        if (!audioSource.isPlaying) audioSource.PlayOneShot(mainEngine);
        if (!mainBoosterParticles.isPlaying) mainBoosterParticles.Play();
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainBoosterParticles.Stop();
    }

    void StartLeftRotation()
    {
        if (!rightBoosterParticles.isPlaying) rightBoosterParticles.Play();
        ApplyRotation(rotationThrust);
    }

    void StartRightRotation()
    {
        if (!leftBoosterParticles.isPlaying) leftBoosterParticles.Play();
        ApplyRotation(-rotationThrust);
    }

    void StopRotation()
    {
        rightBoosterParticles.Stop();
        leftBoosterParticles.Stop();
    }

}
