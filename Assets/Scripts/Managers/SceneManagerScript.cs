using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    BossState bossState = default;
    public GameObject bossHealthBar = default;
    private void Awake()
    {
        bossState = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossState>();
    }
    private void Start()
    {
        AudioManager.instance.PlayGameMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossState.state==BossState.State.SLEEP || bossState.state==BossState.State.DEATH)
        {
            if (bossHealthBar !=null)
            {
                bossHealthBar.SetActive(false);
            }            
        }
        else
        {
            if (bossHealthBar != null)
            {
                bossHealthBar.SetActive(true);
            }
        }
        if (Boss.bossDeath == true)
        {
            Invoke("RestartScene",5);
        }
        else if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().currentHealth<=0)
        {
            Invoke("RestartScene", 5);
        }
    }
    void RestartScene()
    {
        SceneManager.LoadScene(0);
    }
}
