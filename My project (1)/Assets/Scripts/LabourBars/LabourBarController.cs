using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LabourBarController : MonoBehaviour
{
    private LabBarsAttached[] labBars;

    [Header("Basic Parameters")]
    public int currentLabour;
    public int totalLabour;

    public UnityEvent onReturningCannonResult;

    private void Awake() {
        this.labBars = this.GetComponentsInChildren<LabBarsAttached>();
        this.totalLabour = labBars.Length;
        this.currentLabour = totalLabour; //max on scene starts
    }

    private void onLabourChange(int current,int need){
        int adjustment = current -  (current - need);
        // Debug.Log("adj: "+ adjustment);
        int done = 0;
        for(int i = 0; i < labBars.Length; i++){
            if(labBars[i].GetComponentsInChildren<Image>()[0].fillAmount==1){
                Debug.Log(labBars[i].GetComponentsInChildren<Image>()[0].gameObject.name);
                labBars[i].GetComponentsInChildren<Image>()[0].fillAmount = 0;
                done ++;
                // Debug.Log("Done: "+ done);
            }
            if(done == adjustment){
                break;
            }
        }
    }

    public void onGettingRequest(int need){
        if(need <= currentLabour){
            // Debug.Log("compare passed");
            onLabourChange(currentLabour,need);
            currentLabour -= need; 
            onReturningCannonResult.Invoke(); // activate Cannon
        }else{
            
        }
    }

   

}
