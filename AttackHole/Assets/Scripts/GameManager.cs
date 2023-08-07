using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform holeTransform;
    [SerializeField] private GameObject LookCamera;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private Transform firePlace;
    [SerializeField] private Transform Boss;
    [SerializeField] private GameObject[] bullets;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        if(PlayerPrefs.HasKey("size")){
            string GetToJson = PlayerPrefs.GetString("size");

            holeTransform.localScale = JsonUtility.FromJson<HoleManager.HoleSizeClass>(GetToJson).size;
        }

        if(PlayerPrefs.HasKey("RegularBullet"))
        {
            buttons[0].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("RegularBullet").ToString();

            buttons[0].SetActive(true);
        }    


        yield return new WaitForSecondsRealtime(1f);
        LookCamera.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(holeTransform.position.z < 1.5f){
              holeTransform.position = Vector3.MoveTowards(holeTransform.position,holeTransform.position + new Vector3(0f,0f,1f)
                , Time.deltaTime *2f);
        }
      
    }

    private void FireTheAmmo(GameObject ammoType,float minRandomX,float maxRandomY,float force,
        float minRandomVelocity,float maxRandomVelocity)
    {
        GameObject newBullet = Instantiate(ammoType,new Vector3(firePlace.position.x + Random.Range(minRandomX,maxRandomY),
            firePlace.position.y,firePlace.position.z),Quaternion.identity);

        Rigidbody ammoRb = newBullet.GetComponent<Rigidbody>();

        ammoRb.AddForce(transform.forward * force);

        ammoRb.velocity = new Vector3(0f,Random.Range(minRandomVelocity,maxRandomVelocity),0f);

        if(!Boss.GetComponent<Animator>().GetBool("move"))
        {
            Boss.GetComponent<Animator>().SetBool("move",true);
        }
    }
    public void Regular_Btn()
    {
        if(PlayerPrefs.GetInt("RegularBullet")%5 == 0)
        {
            for(int i =0 ; i<5;i++){
                FireTheAmmo(bullets[0],-0.35f,0.35f,500f,5f,8f);

                PlayerPrefs.SetInt("RegularBullet",PlayerPrefs.GetInt("RegularBullet")-1);
                buttons[0].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("RegularBullet").ToString();
            }  
        }
        else
        {
            FireTheAmmo(bullets[0],-0.2f,0.2f,250f,5f,8f);

            PlayerPrefs.SetInt("RegularBullet",PlayerPrefs.GetInt("RegularBullet")-1);
            buttons[0].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("RegularBullet").ToString();
        }
    }
}
