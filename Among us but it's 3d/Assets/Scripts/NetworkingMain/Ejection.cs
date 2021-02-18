using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Ejection : MonoBehaviour
{   

    //~Strings:
    public string playerName = "";

    private void Start()
    {

        ejection(playerName);
        gameObject.GetComponent<SoundHandler>().PlaySFX("VoteOut");

    }

    /**public void noEjection()
    {

        StartCoroutine(startEjection("No one was ejected(skipped)." , 5));

    }**/

    public void ejection(string namePlayer)
    {

        StartCoroutine(startEjection(("Player named "+ namePlayer +" was yeeted.."), 4f));

    }

    public IEnumerator startEjection(string subject , float seconds)
    {

        gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Eject").transform.Find("Text").gameObject.GetComponent<Text>().text = "";
        gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Eject").gameObject.SetActive(true);

        for(int i = 0;i < subject.Length;i++)
        {

            yield return new WaitForSeconds(0.1f);
            gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Eject").transform.Find("Text").gameObject.GetComponent<Text>().text += subject[i];

        }

        yield return new WaitForSeconds(seconds);

        gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Eject").gameObject.SetActive(false);

        Destroy(this);
    }
}
