using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{

    private LabBarsAttached[] labBars;

    [Header("Basic Parameters")]
    public int maxLabourNeeded; //Automatically Gather
    public int currentLabour;
    public UnityEvent<int> onRequestLabour;
    public UnityEvent onAcceptance;


    private void Awake() {
        this.labBars = this.GetComponentsInChildren<LabBarsAttached>();
        this.maxLabourNeeded = labBars.Length;
        this.currentLabour = 0;
        disableLabBars();
    }

    private void disableLabBars(){
        for(int i =0; i<labBars.Length;i++){
            Image[] temp = labBars[i].GetComponentsInChildren<Image>();
            for(int j = 0; j<=temp.Length;j++){
                if(temp[j].name == "Fill"){
                    temp[j].fillAmount = 0;
                    break;
                }
            }
        }
    }

    private void enableLabBars(){
        for(int i =0; i<labBars.Length;i++){
            Image[] temp = labBars[i].GetComponentsInChildren<Image>();
            for(int j = 0; j<=temp.Length;j++){
                if(temp[j].name == "Fill"){
                    temp[j].fillAmount = 1;
                    break;
                }
            }
        }
    }
    public void buttonOnClickEvent(){
        if(this.currentLabour<maxLabourNeeded){
            onRequestLabour.Invoke(maxLabourNeeded);
            // Debug.Log("onRequestLabour Invoked at " + maxLabourNeeded);
        }
    }

    public void onAcceptanceBehaviour(){
        onAcceptance.Invoke();
        this.enableLabBars();
    }
    
}
