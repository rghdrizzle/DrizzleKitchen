using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button play;
    [SerializeField] private Button quit;

    private void Awake(){
        play.onClick.AddListener(()=> {
            //on click
            SceneManager.LoadScene(1);
        });
        quit.onClick.AddListener(()=> {
            //on click
            Application.Quit();
        });
    }
    
}
