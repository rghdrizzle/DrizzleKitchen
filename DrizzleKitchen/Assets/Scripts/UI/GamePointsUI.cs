using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePointsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI money;
    [SerializeField] private Score score;

    private void Update(){
        money.text = "$ " + score.GetScore().ToString();
    }


}
