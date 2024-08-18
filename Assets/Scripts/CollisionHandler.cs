using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip landingSuccess;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
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
        // TODO: add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crash);
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void HandleLevelCompletion()
    {
        // TODO: add particle effect upon success
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(landingSuccess);
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
