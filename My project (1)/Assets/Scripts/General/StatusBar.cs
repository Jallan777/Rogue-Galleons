using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public Image healthFill;
    public Image healthDelay;

    // private void Awake() {
    //     this.healthDelay = this.GetComponentsInChildren<Image>()[0];
    //     this.healthFill = this.GetComponentsInChildren<Image>()[1];
    // }

    public void onHealthChange(float percentage){

          healthFill.fillAmount = percentage;
    }

    private void Update() {
        if(healthDelay.fillAmount<healthFill.fillAmount){
            healthDelay.fillAmount = healthFill.fillAmount;
        }
        if(healthDelay.fillAmount != healthFill.fillAmount){
            healthDelay.fillAmount -= Time.deltaTime * 0.5f;
        }

        //PlayerPrefs.SetFloat("PlayerHealth", (healthFill.fillAmount * 500));
       //PlayerPrefs.Save();
    }
}
