using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public enum Mode{
        LookAt,
        LookAtInverted,
        CameraForwad,
        CameraForwadInvereted
    }
    [SerializeField]private Mode mode;
    private void LateUpdate(){
        switch(mode){
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 dirCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirCamera);
                break;
            case Mode.CameraForwad:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwadInvereted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
        
    }
}
