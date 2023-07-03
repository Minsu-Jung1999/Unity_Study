using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterection : MonoBehaviour
{
    bool isShopAvailable = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isShopAvailable && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Shop is openning");
        }
    }

    public void ShopInterectionSwitcher(bool isAvailable)
    {
        isShopAvailable = isAvailable;
    }
}
