using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileController : MonoBehaviour
{
    private GameManager gameManager;
    private Animator animator;


    void Start()
    {
        gameManager = GameManager.Instance;
        animator = gameObject.GetComponent<Animator>();

   
    }
    void OnBecameInvisible()
    {
        gameManager.pool.Enqueue(gameObject);
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    private void OnBecameVisible()
    {
        animator.SetTrigger("fired");
    }

    void OnCollisionEnter2D(Collision2D col) {
        StartCoroutine(DestroyOver());
    }


    private IEnumerator DestroyOver() {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }
}
