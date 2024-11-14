using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;

    void Awake(){
      rb = GetComponent<Rigidbody2D>();
    }
}
