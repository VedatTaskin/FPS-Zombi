using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    Animator animator;
    [SerializeField] private int maxHealth;
    private int currentHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;        
    }

    public void GiveDamage(int amount)
    {
        currentHealth -= amount;
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth<=0)
        {
            if (animator != null)
            {
                animator.SetTrigger("Death");
            }        
            Destroy(gameObject,3);
        }

    }
}
