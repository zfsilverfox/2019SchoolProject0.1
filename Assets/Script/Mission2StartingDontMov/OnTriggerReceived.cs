using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerReceived : MonoBehaviour {

    [HideInInspector]
    public bool HavingThankBefore = false;
    [HideInInspector]
    public bool HaveHealthBefore = false;


    public GameObject _RewardTxt;
    bool RewartTxtHasAppreadBf = false;



    void OnTriggerEnter(Collider other)
    {
    
        if(other.gameObject.CompareTag("Player")&& this.gameObject.CompareTag("NPCRescueTRIGGER") && this.gameObject.activeInHierarchy)
        {


            this.gameObject.SetActive(false);



            transform.LookAt(other.gameObject.transform.position);



            if (!HavingThankBefore)
            {
                MessageReceived[] received = FindObjectsOfType<MessageReceived>();
                if (received != null)
                {

                    foreach (var rs in received)
                    {
                        rs.OnSendFungusMessage("ThankYou");
                    }

                }
                HavingThankBefore = true;
            }
            if (!HaveHealthBefore)
            {
                HaveHealthBefore = true;
                other.gameObject.GetComponent<PlayerLifeStatement>().Health = 20;
                //if (_SphereColliderResucue != null)
                //    _SphereColliderResucue.SetActive(false);
            }

            if (!RewartTxtHasAppreadBf)
            {
                RewartTxtHasAppreadBf = true;

                if (_RewardTxt != null) _RewardTxt.SetActive(false);
            }




        }




    }




}
