using System.Collections;
using System.Collections.Generic;
using System.IO;    // ���� ������� ���� �����Ѵ�.
using UnityEngine;


/* ���� �ϴ� ���
 * 1. ������ ������ ����
 * 2. �����͸� ���̽����� ��ȯ
 * 3. ���̽��� �ܺο� ����
 * 
 * �ҷ����� ���
 * 1. �ܺο� ����� ���̽��� ������
 * 2. ���̽��� ������ ���·� ��ȯ
 * 3. �ҷ��� �����͸� ���
 */

/*
 * ���� �����ϰ� �� ���� ������ �����ϱ�
 * 
 */

public class PlayerData     // ������ ������ ����
{
    // name, level, coin, equiped weapon
    public string name = "HOGUDAYOO";
    public int level = 1;
    public int coin = 1000;
    public int weapon = -1;
}

public class DataManager : MonoBehaviour
{

    // �̱��� ����
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
        string data = JsonUtility.ToJson(player, true); // �����͸� ���̽����� ��ȯ
        File.WriteAllText(path + fileName, data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + fileName);    // ��� ������ �о� ����
        player = JsonUtility.FromJson<PlayerData>(data);             // �о�� ������ JSON�� �����ϱ�
    }
}
