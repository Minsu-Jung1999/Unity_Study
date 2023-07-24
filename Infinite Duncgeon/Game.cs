using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    public TextMeshProUGUI name_text;
    public TextMeshProUGUI level_text;
    public TextMeshProUGUI coin_text;

    private Button goldButton;
    private Button saveButton;


    // Start is called before the first frame update
    void Start()
    {
        DataManager.instance.LoadData();
        name_text.text = DataManager.instance.player.name;
        level_text.text = DataManager.instance.player.level.ToString();
        coin_text.text = DataManager.instance.player.coin.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGoldButtonClick()
    {
        DataManager.instance.player.coin+=100;
        coin_text.text = DataManager.instance.player.coin.ToString();
    }

    public void OnSaveButtonClick()
    {
        DataManager.instance.SaveData();
    }
}
