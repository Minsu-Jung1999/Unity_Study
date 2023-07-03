using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Shoper : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tm;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
            
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            tm.gameObject.SetActive(true);
            PlayerInterection pl = other.GetComponent<PlayerInterection>();
            pl.ShopInterectionSwitcher(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            tm.gameObject.SetActive(false);
            PlayerInterection pl = other.GetComponent<PlayerInterection>();
            pl.ShopInterectionSwitcher(false);
        }
    }
}
