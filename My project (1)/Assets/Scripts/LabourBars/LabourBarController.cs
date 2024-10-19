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

    public void onReceivingLabour(int amount){
        onLabourChange(currentLabour,amount,true);
        this.currentLabour += amount;
    }
    
    /// <summary>
    /// Change the labour displaying in the total labour bar
    /// </summary>
    /// <param name="current">current labour BEFORE adjustment</param>
    /// <param name="need">amount of adjustment</param>
    /// <param name="behaviour">true for add, false for substract.</param>
    private void onLabourChange(int current,int need, bool behaviour){
        // Debug.Log("adj: "+ adjustment);
        int done = 0;
        if(!behaviour){
            int adjustment = current -  (current - need);        
            for(int i = 0; i < labBars.Length; i++){
                if(labBars[i].GetComponentsInChildren<Image>()[0].fillAmount==1){
                    labBars[i].GetComponentsInChildren<Image>()[0].fillAmount = 0;
                    done ++;
                }
                if(done == adjustment){
                    break;
                }
            }
        }else{
            for(int i = 0; i < labBars.Length; i++){
                if(labBars[i].GetComponentsInChildren<Image>()[0].fillAmount==0){
                    labBars[i].GetComponentsInChildren<Image>()[0].fillAmount = 1;
                    done ++;
                }
                if(done == need){
                    break;
                }
            }
        }
    }

    public void onGettingRequest(int need){
        if(need <= currentLabour){
            // Debug.Log("compare passed");
            onLabourChange(currentLabour,need,false);
            currentLabour -= need; 
            onReturningCannonResult.Invoke(); // activate Cannon
        }else{
            
        }
    }

   

}
