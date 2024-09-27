using System.Collections.Generic;
using MyBase;
using UnityEngine;

public class CreateItemButtons : MonoBehaviour {
    
    private GlobalConfig GlobalConfig => ConfigManager.Instance.GetConfigByClassName("Global") as GlobalConfig;

    private Transform parents ;
    private void Start() {
        parents = GameObject.Find("JewelContent").GetComponent<Transform>();
    }
    

}
