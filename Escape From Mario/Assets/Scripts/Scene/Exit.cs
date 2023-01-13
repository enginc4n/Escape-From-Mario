using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Exit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LoadNextLevel());
        }
    }
    IEnumerator LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        yield return new WaitForSecondsRealtime(0.75f);
        FindObjectOfType<ScenePersist>().ResetScenePersist();

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            FindObjectOfType<GameManager>().ResetGameSession();
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);

    }
}
