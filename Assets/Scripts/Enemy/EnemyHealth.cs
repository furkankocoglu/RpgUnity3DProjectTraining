using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector]
    public float currentHealth=default;
    Animator anim = default;
    public float maxHealth = 100f;
    [SerializeField]
    Image EnemyHealthBar = default;
    SphereCollider targetCollider = default;
    public int ExpAmount = 10;
    public static event Action<int> onDeath = default;
    private void Awake()
    {       
        if (this.gameObject.tag == "Boss")
        {
            maxHealth = UnityEngine.Random.Range(150, 201);
        }
        else if (this.gameObject.tag == "Enemy")
        {
            maxHealth = UnityEngine.Random.Range(75, 101);
        }
        currentHealth = maxHealth; 
        anim = GetComponent<Animator>();
        targetCollider = GetComponentInChildren<SphereCollider>();
    }
    public void TakeDamage(float amount)
    {
          
        currentHealth -= amount;
        EnemyHealthBar.fillAmount = currentHealth / maxHealth;
        if (currentHealth > 0)
        {
            anim.SetTrigger("Hit");
            if (this.gameObject.tag=="Boss")
            {
                AudioManager.instance.PlaySfx(6);
            }
            else if (this.gameObject.tag=="Enemy")
            {
                AudioManager.instance.PlaySfx(3);
            }
        }
        if (currentHealth<=0)
        {            
            Canvas canvas = EnemyHealthBar.gameObject.GetComponentInParent<Canvas>();
            onDeath(ExpAmount);
            if (targetCollider.gameObject.activeInHierarchy)
            {
                targetCollider.gameObject.SetActive(false);
            }            
            if (canvas.gameObject.activeInHierarchy)
            {
                canvas.gameObject.SetActive(false);
            }            
        }
    }
    
}
