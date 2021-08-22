using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private int maxHealth;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;        
    }

    void GiveDamage(int amount)
    {
        currentHealth -= amount;
    }

    void AddHealth(int amount)
    {
        currentHealth += amount;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth<=0)
        {
            Destroy(gameObject);
        }

    }
}
