using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    public float maxHealth = 100;
    public float playerHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 100;
        healthBar.SetMaxHealth((int)maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
