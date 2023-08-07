using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public Transform bossBloodSlider;
    [SerializeField] private Transform target;
    public Animator bossAnimator;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        bossAnimator = GetComponent<Animator>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            if(bossBloodSlider.gameObject.activeSelf)
            {
                bossBloodSlider.position = new Vector3(bossBloodSlider.position.x,bossBloodSlider.position.y,transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position,target.position,Time.deltaTime*1.5f);
            }
           
        }
        else{
            bossBloodSlider.gameObject.SetActive(false);
        }
    }

}
