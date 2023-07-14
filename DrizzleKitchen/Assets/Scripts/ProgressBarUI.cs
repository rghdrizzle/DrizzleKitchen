using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField]private Image barimage;
    [SerializeField]private CuttingCounter cuttingCounter;
    private void Start(){
        cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;
        barimage.fillAmount = 0;
        Hide();
    }
    private void CuttingCounter_OnProgressChanged(object sender,CuttingCounter.OnProgressChangedEventArgs e){
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
