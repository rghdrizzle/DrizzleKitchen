using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{   
    
    public float score;
    private void Start(){
        score =0f;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(score);
    }

    public float GetScore(){
        return score;
    }
}
