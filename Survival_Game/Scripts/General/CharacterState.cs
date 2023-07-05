using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterState : MonoBehaviour
{
    [SerializeField] public float maxHealth = 100f;   
    [SerializeField] public float currentHealth;

    [SerializeField] public float maxEnergy = 100f;
    [SerializeField] public float currentEnergy;

    [SerializeField] public float maxAttackDamage = 20;
    [SerializeField] public float currentAttackDamage;

    [SerializeField] public Image hpBar;
    [SerializeField] public Image energyBar;



    private void Awake()
    {
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        currentAttackDamage = maxAttackDamage;
        hpBar.fillAmount = 1;
        energyBar.fillAmount = 1;
    }

}
