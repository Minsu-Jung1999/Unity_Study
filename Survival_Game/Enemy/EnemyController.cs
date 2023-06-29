using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] Renderer enemymaterial;
    [SerializeField] AudioSource hitSound;

    // Start is called before the first frame update
    void Start()
    {
        enemymaterial = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Katana")
        {
            HitDamage();
        }
        if(other.tag == "Player")
        {
            Debug.Log("Player is detacted");
        }
    }

    public void HitDamage()
    {
        animator.SetTrigger("hit");
        hitSound.Play();
        StartCoroutine("HitProcessing");
    }

    IEnumerator HitProcessing()
    {
        Color color = enemymaterial.material.color;
        enemymaterial.material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        enemymaterial.material.color = color;


    }
}
