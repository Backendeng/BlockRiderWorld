using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Invector.vCharacterController;
public class PlayerSetup : MonoBehaviourPunCallbacks
{
    vThirdPersonController thirdPersonController;
    vThirdPersonInput thirdPersonInput;
    // Start is called before the first frame update
    void Start()
    {
        thirdPersonController = gameObject.GetComponent<vThirdPersonController>();
        thirdPersonInput = gameObject.GetComponent<vThirdPersonInput>();
        if (photonView.IsMine)
        {
            transform.GetComponent<vThirdPersonController>().enabled = true;
            transform.GetComponent<vThirdPersonInput>().enabled = true;
        }
        else
        {
            transform.GetComponent<vThirdPersonController>().enabled = false;
            transform.GetComponent<vThirdPersonInput>().enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}