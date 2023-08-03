using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HoleManager : MonoBehaviour
{

    private float circleCapacity;
    [SerializeField] private Image circleImg;
    [SerializeField] private Transform holeTransform;
    [SerializeField] private TextMeshProUGUI timer_txt;


    [Serializable] public class HoleSizeClass{
        public Vector3 size;
    }

    HoleSizeClass _holeSizeClass = new HoleSizeClass();

    void Start()
    {
        StartCoroutine(timer(20));

        if(PlayerPrefs.HasKey("size")){
            string GetToJson = PlayerPrefs.GetString("size");
            holeTransform.localScale = JsonUtility.FromJson<HoleSizeClass>(GetToJson).size;
        }
    }

    private void progressBarCircle(int number){
        circleCapacity = 1f/number;

        circleImg.fillAmount += circleCapacity;

        if(circleImg.fillAmount.Equals(1f)){
            holeTransform.localScale += new Vector3(0.3f,0,0.3f);

            circleImg.fillAmount =0f;

            _holeSizeClass.size = holeTransform.localScale;

            string SetToJson = JsonUtility.ToJson(_holeSizeClass);

            PlayerPrefs.SetString("size",SetToJson);
        }
    } 

    private IEnumerator timer(int time){
        int remainTime = time;
        while (remainTime >= 0f)
        {
            timer_txt.text = "00:"+remainTime;
            remainTime--;

            yield return new WaitForSecondsRealtime(1f);
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("ammo")){
            progressBarCircle(20);

            other.gameObject.SetActive(false);
        }
    }
}
