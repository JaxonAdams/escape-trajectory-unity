using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip landingSuccess;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem landingSuccessParticles;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) return;

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Bumped into a friendly object.");
                break;
            case "Finish":
                HandleLevelCompletion();
                break;
            default:
                HandleCrash();
                break;
        }
    }

    void HandleCrash()
    {
        isTransitioning = true;

        GetComponent<Movement>().enabled = false;

        audioSource.Stop();
        audioSource.PlayOneShot(crash);

        crashParticles.Play();

        Invoke("ReloadLevel", levelLoadDelay);
    }

    void HandleLevelCompletion()
    {
        isTransitioning = true;

        GetComponent<Movement>().enabled = false;

        audioSource.Stop();
        audioSource.PlayOneShot(landingSuccess);

        landingSuccessParticles.Play();

        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex);
    }

    void LoadNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = (currentLevelIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextLevel);
    }
}
