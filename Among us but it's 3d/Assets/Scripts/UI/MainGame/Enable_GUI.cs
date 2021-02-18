using UnityEngine; //Unity Main
using System.Collections;

public class Enable_GUI : MonoBehaviour
{
    
    //~GameObjects:
    public GameObject ImposterUIElement; //Get the ui stuff.
    public GameObject CrewmateUIElement; //Get the crewmate ui.

    private void Start()
    {   

        //SetUIForGame:
        ImposterUIElement = gameObject.transform.Find("Canvas").transform.Find("ImposterUIElement").gameObject;
        CrewmateUIElement = gameObject.transform.Find("Canvas").transform.Find("CrewmateUIElement").gameObject;
        //Settings GUI'S false:
        ImposterUIElement.SetActive(false); // On start assign. -> Don't show ui.
        CrewmateUIElement.SetActive(false);

        StartCoroutine(DisplayUIInGame());

    }

    private IEnumerator DisplayUIInGame() //Method -> display ui.
    {

        yield return new WaitForSeconds(4f);

        //Set GUI of crewmate and imposters
        for(int i = 0; i < gameObject.GetComponent<Assigner>().numberOfImposters; i++) //For i in gameobjects:
        {

            if(gameObject.GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Crewmate)
            {
                
                print("GUI : if(1)"+ gameObject.GetComponent<Assigner>().playersInGame[i].GetComponent<Player_Data_Script>().playerTypeEnum);
                //Set Crewmate UI:
                CrewmateUIElement.SetActive(true);

            }

            if(gameObject.GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Imposter)
            {

                //Set Imposter UI:
                ImposterUIElement.SetActive(true);

            }

        }

    }

}
