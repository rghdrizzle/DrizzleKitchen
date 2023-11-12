using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
     [SerializeField] private Button resumeButton;
     [SerializeField] private Button mainMenuButton;

     private void Awake(){
        resumeButton.onClick.AddListener(()=> {
            GameManager.Instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(()=>{
            SceneManager.LoadScene(0);
        });
     }
     private void Start(){
        GameManager.Instance.OnGamePaused+= GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnPaused += GameManager_OnUnpaused;
        Hide();
    }
    private void GameManager_OnGamePaused(object sender , System.EventArgs e){
       Show();
    }
    private void GameManager_OnUnpaused(object sender , System.EventArgs e){
        Hide();
    }
    private void Show(){
        gameObject.SetActive(true);
        Cursor.lockState= CursorLockMode.None;
        Cursor.visible = true;
    }
    private void Hide(){
        Cursor.lockState= CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
    }
}

