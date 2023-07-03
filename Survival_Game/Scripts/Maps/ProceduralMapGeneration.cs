using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMapGeneration : MonoBehaviour
{
    [SerializeField] GameObject floor;
    [SerializeField] GameObject wall;
    [Header("�� ����")]
    [SerializeField] GameObject[] walls = new GameObject[4];
    [SerializeField] float wall_z_length = 10f;
    [Header("Floor ����")]
    [SerializeField] [Range(2, 10)] int maxWidthValue;
    [SerializeField] [Range(2, 10)] int maxHeightvalue;
    [SerializeField] int RoomHeight = 2;
    GameObject room;
    GameObject [] allRooms = new GameObject[5];
    Vector2[] lengthHolder = new Vector2[5];

    int indexHolder = 0;
    int x_length;
    int y_length;
    void Start()
    {
        AllRoomGenerating();
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ReGeneratingAll();
        }
    }
    // ���������� ���� �����ϴ� �Լ�
    private void RoomGenerating(ref GameObject parentOB)
    {
        parentOB = new GameObject();
        RoomInit(parentOB);
        FloorInit(parentOB);
        WallInit(parentOB);
    }

    // �� ��ü���� �ڽ� ��ü�� �ϴ� �� �θ� ��ü ����
    private void RoomInit(GameObject parentOB)
    {
        parentOB.transform.parent = this.transform;
    }

    // �ٴ��� ������ ������� ����
    private void FloorInit(GameObject parentOB)
    {
        x_length = Random.Range(2, maxWidthValue);
        y_length = Random.Range(2, maxHeightvalue);

        GameObject floorOb = Instantiate(floor, Vector3.zero, Quaternion.identity);
        floorOb.transform.localScale = new Vector3(x_length, RoomHeight, y_length);
        floorOb.transform.parent = parentOB.transform;
    }

    // ������ �ٴ� ����� �������� �� ��ü ����
    private void WallInit(GameObject parentOB)
    {
        for (int i = 0; i < 4; i++)
        {
            walls[i] = Instantiate(wall, parentOB.transform.position, Quaternion.identity, parentOB.transform);

            if (i < 2)
            {
                walls[i].transform.localScale = new Vector3(1, 2.7f, y_length * 10);
            }
            else
            {
                walls[i].transform.localScale = new Vector3(1, 2.7f, x_length * 10);
                walls[i].transform.Rotate(0, 90, 0);
            }

        }
        float x_clamp = x_length * 5 - 0.5f;
        float y_clamp = y_length * 5 - 0.5f;
        lengthHolder[indexHolder++] = new Vector2(x_clamp, y_clamp);


        // Right Wall
        walls[0].transform.position = new Vector3(x_clamp, 1.2f, 0);

        // Left Wall
        walls[1].transform.position = new Vector3(-x_clamp, 1.2f, 0);

        // Top Wall
        walls[2].transform.position = new Vector3(0, 1.2f, y_clamp);


        // Bottom Wall
        walls[3].transform.position = new Vector3(0, 1.2f, -y_clamp);

    }

    // 4�������� ���� �� ����
    private void AllRoomGenerating()
    {
        
        for (int i = 0; i < allRooms.Length; i++)
        {
            RoomGenerating(ref allRooms[i]);
        }

        // ���̽�
        allRooms[0].transform.position = new Vector3(0, 0, 0);

        // ����
        allRooms[1].transform.position = new Vector3(lengthHolder[0].x + lengthHolder[1].x, 0, 0);

        // ����
        allRooms[2].transform.position = new Vector3(-(lengthHolder[0].x + lengthHolder[2].x), 0, 0);

        // ��
        allRooms[3].transform.position = new Vector3(0, 0, lengthHolder[0].y + lengthHolder[3].y);

        // �Ʒ�
        allRooms[4].transform.position = new Vector3(0, 0, -(lengthHolder[0].y + lengthHolder[4].y));
    }

    private void ReGeneratingAll()
    {
        for (int i = 0; i < allRooms.Length; i++)
        {
            Destroy(allRooms[i].gameObject);
        }
        indexHolder = 0;
        AllRoomGenerating();
    }

    

    
}
