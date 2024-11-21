using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject ChoseObject;
    [SerializeField] private int currentLevel = 1;

    public int CurrentLevel => currentLevel; 

    private void Awake() 
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }

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

    private IEnumerator LoadSceneAsync(string sceneName) // Load scene with transition
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

    public void SetLevel(int level)
    {
        currentLevel = level;
    }
}
