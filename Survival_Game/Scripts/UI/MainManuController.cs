using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManuController : MonoBehaviour
{
    [SerializeField] Button startBtn;
    

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }
}
