using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ImposterNameAssign : MonoBehaviour
{
    private GameObject[] playersInGame;


    private void Start()
    {

        playersInGame = GameObject.FindGameObjectsWithTag("Player");


    }

    private void LateUpdate()
    {

        if(gameObject.GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Imposter)
        {

            for(int i = 0; i<playersInGame.Length ;i++)
            {

                if(playersInGame[i].GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Imposter)
                {

                    playersInGame[i].transform.Find("Name").gameObject.GetComponent<TextMeshPro>().color = new Color(1,0,0,1);

                }

            }

        }

    }

}
