using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUi : MonoBehaviour
{
    void Start()
    {
        
        
    }
    void Update()
    {
        if(gameObject.name == "scoretxt")
        {
            GetComponent<Text>().text = "Score" + TurnBase.score;
        }
        if(gameObject.name == "rockettxt")
        {
            GetComponent<Text>().text = "Rocket Total:" + TurnBase.rocket;
        }
        
    }
}