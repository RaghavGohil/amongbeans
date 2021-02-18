using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NoEjection : MonoBehaviour
{
    

    private void Start()
    {

        noEjection();
        gameObject.GetComponent<SoundHandler>().PlaySFX("VoteOut");


    }

    public void noEjection()
    {

        StartCoroutine(startEjection("No one was yeeted(skipped).." , 3f));

    }

    /**public void Ejection(string namePlayer)
    {

        StartCoroutine(startEjection(("Player "+ namePlayer +" was ejected."), 5));

    }**/

    public IEnumerator startEjection(string subject , float seconds)
    {

        gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Eject").transform.Find("Text").gameObject.GetComponent<Text>().text = "";
        gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Eject").gameObject.SetActive(true);
        

        for(int i = 0;i < subject.Length;i++)
        {

            yield return new WaitForSeconds(0.11f);
            gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Eject").transform.Find("Text").gameObject.GetComponent<Text>().text += subject[i];

        }

        yield return new WaitForSeconds(seconds);

        gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Eject").gameObject.SetActive(false);

        Destroy(this);
    }

}
