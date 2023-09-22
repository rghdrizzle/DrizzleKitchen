using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameCountdownUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI countdown;

    private void Start(){
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }
    private void Update(){
        countdown.text = Mathf.Ceil(GameManager.Instance.GetCountdownTimer()).ToString();
    }
    private void GameManager_OnStateChanged(object sender , System.EventArgs e){
        if(GameManager.Instance.IsCountDownToStartActive()){
            Show();
        }
        else{
            Hide();
        }
    }
    private void Show(){
        gameObject.SetActive(true);
    }
    private void Hide(){
        gameObject.SetActive(false);
    }
}
