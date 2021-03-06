using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static float scorePoint;
    // Start is called before the first frame update
    [SerializeField] GameObject particle;
    [SerializeField] GameObject spawnerObstacle;

    [SerializeField] GameObject overallText;
    [SerializeField] GameObject bestText;

    void Start()
    {
        GlobalEventManager.ResetPoint.AddListener(ResetScorePoint);
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Coin"))
        {
            scorePoint++;
            GlobalEventManager.OnCoinPicked.Invoke();
            Destroy(collider2D.gameObject);
            print(scorePoint);
        }
        if (collider2D.CompareTag("Enemy"))
        {
            particle.SetActive(true);
            PlayerPrefs.SetFloat("MaxScore", PlayerPrefs.GetFloat("MaxScore") +  scorePoint);
            PlayerPrefs.Save();
            spawnerObstacle.SetActive(false);
            GameManager.inGame = false;
            GlobalEventManager.LoseEvent.Invoke();
            gameObject.SetActive(false);
            overallText.GetComponent<Animator>().SetTrigger("Show");
            bestText.GetComponent<Animator>().SetTrigger("Show");
        }
    }
    
    private void ResetScorePoint()
    {
        scorePoint = 0;
    }
}
