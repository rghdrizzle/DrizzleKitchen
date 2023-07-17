using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField]private Image barimage;
    [SerializeField]private IHasProgress hasProgress;
    [SerializeField]private GameObject hasProgressGameObject;
    private void Start(){
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if(hasProgress == null){
            Debug.LogError("Game Object"+ hasProgressGameObject+"does not have a component that uses Ihasprogress as interface");
        }
        hasProgress.OnProgressChanged += hasProgress_OnProgressChanged;
        barimage.fillAmount = 0;
        Hide();
    }
    private void hasProgress_OnProgressChanged(object sender,IHasProgress.OnProgressChangedEventArgs e){
        barimage.fillAmount = e.progressNormalized;
        if(e.progressNormalized == 0f || e.progressNormalized==1f){
            Hide();
        }
        else{
            Show();
        }
    }
    private void Show(){
        gameObject.SetActive(true);
    }
    private void Hide(){
        gameObject.SetActive(false);
    }

}
