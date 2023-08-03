using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public class HoleSizeClass{
        public Vector3 size;
    }

    private HoleSizeClass holeSizeData = new HoleSizeClass();

    public HoleSizeClass HoleSizeData => holeSizeData;

    private void Awake(){
        if(Instance == null){
            Instance =this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
}
