using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHendler : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip explosionRocket;
    [SerializeField] AudioClip successLevel;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem explosiveParticles;

    AudioSource audioSource;

    bool isTransitioning =false;
    bool collisionDisable = false;

     void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisable){ return;}

            switch (collision.gameObject.tag)
            {
                case "friendly":
                    Debug.Log("This thing is friendly");
                    break;

                case "Finish":
                    StartSuccesSequence();
                    Debug.Log("Finish");
                    break;

                default:
                    StartCrashSequence();
                    Debug.Log("Sorry y blew up");
                    break;

            }
        
    }

    void StartSuccesSequence()
    {
        isTransitioning = true;
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successLevel);
        Invoke("LoadNextLevel", levelLoadDelay);
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        // todo add SXF upon crash
        explosiveParticles.Play();
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(explosionRocket);
        Invoke("ReloadLevel", 1f);
    }

    /*void Finishdelay()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", 1f);
    }*/

    

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
      //  SceneManager.LoadScene(currentSceneIndex +1);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    
}
