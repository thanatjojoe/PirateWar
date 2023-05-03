using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void nextScene()
    {
        if (TurnBase.stage < 3)
        {
            SceneManager.LoadScene(TurnBase.stage);
        }
        
    }
    public void BuyRocket()
    {
        if (TurnBase.score >= 20)
        {
            TurnBase.score = TurnBase.score- 20;
            TurnBase.rocket++;
        }
    }
}
