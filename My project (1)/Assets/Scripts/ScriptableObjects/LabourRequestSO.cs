using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LabourRequestSO : ScriptableObject
{
    public UnityAction<int> RequestLabour;
    
    // public bool OnEventRaised(bool result){
    //     if(result){

    //     }
    // }
}
