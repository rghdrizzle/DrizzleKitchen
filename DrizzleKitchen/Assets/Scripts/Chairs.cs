using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chairs : MonoBehaviour
{
    public static Chairs Instance;
    [SerializeField] private Transform[] seats;
    private Queue<Transform> chairPoints = new Queue<Transform>();

    private void Awake(){
        Instance = this;
        
    }
    private void Update(){
        for(int i=0;i<seats.Length;i++){
                    chairPoints.Enqueue(seats[i]);   
            
                    }
    }

    public Transform GetChair(){
        return chairPoints.Dequeue();
    }

    public void ResetChair(Transform chair){
        chairPoints.Enqueue(chair);
    }
}