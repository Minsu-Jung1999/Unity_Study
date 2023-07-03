using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    [SerializeField] GameObject hp_bar = null;
    [SerializeField] Camera cam;
    [SerializeField] float hpBarHeigh;

    List<Transform> parentObject = new List<Transform>();
    List<GameObject> hpBars = new List<GameObject>();



    void Start()
    {
        cam = Camera.main;
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < objects.Length; i++)
        {
            parentObject.Add(objects[i].transform);
            GameObject hpBar = Instantiate(hp_bar, objects[i].transform.position, Quaternion.identity, transform);
            hpBars.Add(hpBar);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < parentObject.Count; i++)
        {
            hpBars[i].transform.position = cam.WorldToScreenPoint(parentObject[i].position + new Vector3(0, hpBarHeigh, 0));
        }
    }
}
