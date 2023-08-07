using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    private void OnCollisionEnter(Collision other){
        if(!other.gameObject.CompareTag("Ground") && !other.gameObject.CompareTag("ammo"))
        {
            if(other.transform.root.gameObject.TryGetComponent(out BossManager boss))
            {
                if(!boss.bossBloodSlider.gameObject.activeSelf)
                {
                    boss.bossBloodSlider.gameObject.SetActive(true);
                }

                boss.bossBloodSlider.transform.GetChild(0).GetComponent<Slider>().value--;

                if(boss.bossBloodSlider.transform.GetChild(0).GetComponent<Slider>().value.Equals(0f))
                {
                    boss.bossAnimator.enabled = false;

                    boss.isDead = true;     
                }

            }
            gameObject.SetActive(false);
        }
    }
}
