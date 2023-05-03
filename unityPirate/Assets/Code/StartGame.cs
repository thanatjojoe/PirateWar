using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject start;

    public void LoadScene()
    {
        SceneManager.LoadScene("1");
    }
}
