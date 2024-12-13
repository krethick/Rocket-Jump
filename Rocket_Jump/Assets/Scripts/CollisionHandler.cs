using UnityEngine.SceneManagement;
using UnityEngine;


public class CollisionHandler : MonoBehaviour
{
[SerializeField] float  levelLoadDelay = 1f; // Set the delay for 1 second
[SerializeField] AudioClip gameover;
[SerializeField] AudioClip gamesuccess;

[SerializeField] ParticleSystem particle_gameover;
[SerializeField] ParticleSystem particle_gamesuccess;


 AudioSource audioSource;

 bool isTransition = false; // Keep the initial transition state as false, currently we are playing the game and not bumping onto something
 bool CollisionDisabled = false;

 void Start()
 {
   audioSource = GetComponent<AudioSource>();
 }

 void Update()
 {
    NextLevelKey();  // When you press the key L,it goes to the next level
 }

 void NextLevelKey()
    {
      if (Input.GetKeyDown(KeyCode.L))
      {
         LoadNextLevel();
      }

      else if (Input.GetKeyDown(KeyCode.C)) // Once the button is clicked the CollisionDisabled will change from false to true
      {
         CollisionDisabled = !CollisionDisabled; //toggle collision i.e false = true and true = false
      }
    }
    void OnCollisionEnter(Collision other) 
    {
      if (isTransition || CollisionDisabled) // If the transition is false
      {
         return; // Return to normalcy
      }
      
        switch (other.gameObject.tag)
       {
          case "Friendly":
               Debug.Log("This is a friendly game object");
               break;
          case "Finish":
              //Debug.Log("Congrats the level is completed");
               StartSucessSequence(); // Ctrl + . Helps in generating the method 
               break;
          default: 
               StartCrashSequence();
               break;
       }
      
      
    }

    
    

    void StartCrashSequence()
    {
       // Add SFX Upon Crash 
       //audioSource = GetComponent<AudioSource>();
      isTransition = true; // We set transition true
       audioSource.Stop();
       audioSource.PlayOneShot(gameover);
       particle_gameover.Play(); // Triggers the gameover particle
       GetComponent<Movement>().enabled = false;
       Invoke("ReloadLevel", levelLoadDelay); // Invoke allows us to call a method so it executes after a delay of x seconds
       
    }

    void StartSucessSequence()
    {
      //audioSource = GetComponent<AudioSource>();
      // Add SFX Upon Crash 
      
      isTransition = true; // We set transition true
      audioSource.Stop();
      audioSource.PlayOneShot(gamesuccess);
      particle_gamesuccess.Play(); // Triggers the success particle
      GetComponent<Movement>().enabled = false;
      Invoke("LoadNextLevel", levelLoadDelay); // Invoke allows us to call a method so it executes after a delay of x seconds
      
    }

    void LoadNextLevel()
    {
        
       // This Loads to the next level
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1; // i.e index position is 0 and 1 so  1 + 1 = 2
        // If next scene index is 2 and if it is equal to the scene count in build go back to the first level
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
           nextSceneIndex = 0; // Goes back to level 1
        }
       SceneManager.LoadScene(nextSceneIndex);
       
    }

    void ReloadLevel()
    {
        // We can either use a index or string.
        //SceneManager.LoadScene(0);
        // This reloads the Level during game over
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

   

 
   
}
