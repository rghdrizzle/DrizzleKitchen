using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance{ get; private set;}
    [SerializeField]private AudioClipSO audioClipSO;
    private void Awake(){
        Instance= this;
    }
    private void Start(){
        DeliveryManager.Instance.OnRecipeSuccess+= DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed+= DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut+= CuttingCounter_OnAnyCut;
        Player.Instance.OnPick+= Player_OnPick;
        BaseCounter.OnAnyObjectPlaced+= BaseCounter_OnAnyObjectPlaced;
        TrashCounter.OnAnyTrash+= TrashCounter_OnAnyTrash;
    }
    private void TrashCounter_OnAnyTrash(object sender,System.EventArgs e){
       TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipSO.trash,trashCounter.transform.position);
    }
    private void BaseCounter_OnAnyObjectPlaced(object sender,System.EventArgs e){
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipSO.objectDrop,baseCounter.transform.position);
    }    
    private void Player_OnPick(object sender, System.EventArgs e){
        PlaySound(audioClipSO.objectPick,Player.Instance.transform.position);
    }
    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e){
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipSO.chop,cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e){
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipSO.deliverySuccess, deliveryCounter.transform.position);
    }
    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e){
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipSO.deliveryFail, deliveryCounter.transform.position);
    }
    private void PlaySound(AudioClip audioClip,Vector3 position,float volume = 1f){
        AudioSource.PlayClipAtPoint(audioClip,position,volume);
    }

    private void PlaySound(AudioClip[] audioClipArray,Vector3 position,float volume = 1f){
        
       PlaySound(audioClipArray[Random.Range(0,audioClipArray.Length)],position,volume);
    }
    public void PlayFootstepSound(Vector3 position,float volume){
        PlaySound(audioClipSO.footsteps,position,volume);
    }
}
