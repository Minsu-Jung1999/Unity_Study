using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    [SerializeField] private float defaultHealth = 100f;   
    [SerializeField] public float currentHealth;

    [SerializeField] private float defaultEnergy = 100f;
    [SerializeField] public float currentEnergy;

    [SerializeField] private float defaultAttackDamage = 20;
    [SerializeField] public float currentAttackDamage;


    private void Awake()
    {
        currentHealth = defaultHealth;
        currentEnergy = defaultEnergy;
        currentAttackDamage = defaultAttackDamage;
    }

    public float getHealth()
    {
        return currentHealth;
    }
    public float getDamage()
    {
        return currentAttackDamage;
    }
    public float getEnergy()
    {
        return currentEnergy;
    }

}
