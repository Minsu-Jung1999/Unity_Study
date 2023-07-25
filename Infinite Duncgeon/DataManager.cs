using System.Collections;
using System.Collections.Generic;
using System.IO;    // 파일 입출력을 위해 선언한다.
using UnityEngine;


/* 저장 하는 방법
 * 1. 저장할 데이터 생성
 * 2. 데이터를 제이슨으로 변환
 * 3. 제이슨을 외부에 저장
 * 
 * 불러오는 방법
 * 1. 외부에 저장된 제이슨을 가져옴
 * 2. 제이슨을 데이터 형태로 변환
 * 3. 불러온 데이터를 사용
 */

/*
 * 씬을 구성하고 씬 별로 데이터 저장하기
 * 
 */

public class PlayerData     // 저장할 데이터 생성
{
    // name, level, coin, equiped weapon
    public string name = "HOGUDAYOO";
    public int level = 1;
    public int coin = 1000;
    public int weapon = -1;
    public Vector3 playerPosition = Vector3.zero;
}

public class DataManager : MonoBehaviour
{

    // 싱글톤 구현
    public static DataManager instance;

    public PlayerData player = new PlayerData();

    string path;
    string fileName = "/save";
    private void Awake()
    {
        #region SinglTon
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion

        path = Application.persistentDataPath;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData()
    {
        PlayerController playercontroller = FindAnyObjectByType<PlayerController>();
        if(playercontroller)
        {
            print("Found!");
            player.playerPosition = playercontroller.transform.position;
        }

        string data = JsonUtility.ToJson(player, true); // 데이터를 제이슨으로 변환
        File.WriteAllText(path + fileName, data);
        print(path);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + fileName);    // 모든 데이터 읽어 오기
        player = JsonUtility.FromJson<PlayerData>(data);             // 읽어온 데이터 JSON에 저장하기
    }
}
