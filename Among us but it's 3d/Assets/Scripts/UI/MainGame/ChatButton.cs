using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatButton : MonoBehaviour
{

    private bool toggle;

    private void Start()
    {

        toggle = false;

    }

    public void ChatButtonStart()
    {

        toggle = !toggle;
        gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Voting").transform.Find("Chat").transform.Find("Panel").gameObject.SetActive(toggle);

    }

    private void Update()
    {

        if(gameObject.GetComponent<Player_Data_Script>().playerStatusEnum == Player_Data_Script.Player_status.Dead)
        {

            gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Voting").transform.Find("Chat").transform.Find("Panel").gameObject.SetActive(false);

        }

    }
}
