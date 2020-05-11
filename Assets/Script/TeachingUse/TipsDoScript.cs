using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsDoScript : MonoBehaviour {

    bool HaveDoFungus = false;
    private void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.activeInHierarchy && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("The Player has Enter the Area of teaching");
            if (!HaveDoFungus)
            {
                MessageReceived[] receivers = GameObject.FindObjectsOfType<Fungus.MessageReceived>();
                if(receivers != null)
                {
                    foreach(var received in receivers)
                    {
                        received.OnSendFungusMessage("CantEnterSrea");
                    }
                }
                HaveDoFungus = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (this.gameObject.activeInHierarchy && other.gameObject.CompareTag("Player"))
        {
            HaveDoFungus = false;
        }

    }

}
