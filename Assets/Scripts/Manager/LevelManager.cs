using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject ChoseObject;

    void Awake()
    {
        if (ChoseObject != null)
        {
            ChoseObject.SetActive(false);
        }
        
        if (animator != null)
        {
            animator.enabled = false;
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        if (animator != null)
        {
            animator.enabled = true;
            animator.SetTrigger("StartTransition");
            yield return new WaitForSeconds(1);
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (Player.Instance != null)
        {
            Player.Instance.transform.position = new Vector3(0, -1f, Player.Instance.transform.position.z);
        }
    }
}