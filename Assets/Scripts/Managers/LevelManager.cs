using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    int currentExp = default;
    int level = default;
    int expToNextLevel = default;
    public int GetLevel { get { return level+1; } }
    public static LevelManager instance=default;

    public Image ExpBar = default;
    public Text LevelText = default;

    public GameObject LevelUpVFX;
    Transform Player;
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);            
        }
        else
        {
            Destroy(gameObject);
        }
        level = 0;
        currentExp = 0;
        expToNextLevel = 100;
        ExpBar.fillAmount = 0f;
        UpdateLevelText();
        Player = GameObject.Find("Player").transform;
    }
   
    public void AddExp(int amount)
    {
        currentExp += amount;
        if (ExpBar==null)
        {
            ExpBar = GameObject.Find("ExpBar").GetComponent<Image>();
            LevelText = GameObject.Find("LevelFrame").GetComponentInChildren<Text>();
        }
        ExpBar.fillAmount = (float)currentExp / expToNextLevel;
        if (currentExp>=expToNextLevel)
        {
            level++;
            GameObject clone= Instantiate(LevelUpVFX,Player.position,Quaternion.identity);
            clone.transform.SetParent(Player);
            UpdateLevelText();
            currentExp -= expToNextLevel;
            ExpBar.fillAmount = 0f;
        }
    }
    void UpdateLevelText()
    {
        LevelText.text = GetLevel.ToString();
    }
    private void OnEnable()
    {
        EnemyHealth.onDeath += AddExp;
    }
    private void OnDisable()
    {
        EnemyHealth.onDeath -= AddExp;
    }
}
