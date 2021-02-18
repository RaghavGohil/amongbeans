using System.Collections;
using UnityEngine;

public class RoleRevealAssign : MonoBehaviour
{
    //GameObjects:
    public GameObject imposterElement;
    public GameObject crewmateElement;

    //~Scripts:
    private Player_Data_Script playerDataScript;

    private void Start()
    {

        StartCoroutine(WaitForActivation());

        /**print("ROLE ASSIGN PLAYING");
        
        playerDataScript = gameObject.GetComponent<Player_Data_Script>();

        print(playerDataScript.playerTypeEnum);

        imposterElement = gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("Role").transform.Find("Imposter").gameObject;
        crewmateElement = gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("Role").transform.Find("Crewmate").gameObject;

        imposterElement.SetActive(false);
        crewmateElement.SetActive(false);

        if(playerDataScript.playerTypeEnum == Player_Data_Script.Player_type.Imposter)
        {

            imposterElement.SetActive(true);

        }
        else{crewmateElement.SetActive(true);}**/
        
    }

    private IEnumerator WaitForActivation()
    {

        yield return new WaitForSeconds(1f);

        print("ROLE ASSIGN PLAYING");
        
        playerDataScript = gameObject.GetComponent<Player_Data_Script>();

        print(playerDataScript.playerTypeEnum);

        imposterElement = gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("Role").transform.Find("Imposter").gameObject;
        crewmateElement = gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("Role").transform.Find("Crewmate").gameObject;

        imposterElement.SetActive(false);
        crewmateElement.SetActive(false);

        if(playerDataScript.playerTypeEnum == Player_Data_Script.Player_type.Imposter)
        {

            imposterElement.SetActive(true);

        }
        else if(playerDataScript.playerTypeEnum == Player_Data_Script.Player_type.Crewmate){crewmateElement.SetActive(true);}

    }

}
