using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        DataManager.instance.LoadData();
        transform.position = DataManager.instance.player.playerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float _x_direction = Input.GetAxisRaw("Horizontal");
        float _z_direction = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(_x_direction, 0, _z_direction).normalized * Time.deltaTime;
        transform.position += direction * speed;
    }
}
