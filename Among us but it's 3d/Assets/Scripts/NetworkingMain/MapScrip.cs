using UnityEngine;
using UnityEngine.SceneManagement;

public class MapScrip : MonoBehaviour
{

    //~GameObject:
    private GameObject map;

    //~Bools:
    private bool toggle;


    private void Start()
    {

        toggle = false;
        map = gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Map").gameObject;
        map.SetActive(false);

    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.M) && SceneManager.GetActiveScene().name == "MainGame")
        {

            toggle = !toggle;

        }

        if(toggle)
        {

            gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("CrosshairHolder").gameObject.SetActive(false);

        }

        if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Voting").transform.Find("VotingPanel").gameObject.active == true)
        {

            map.SetActive(false);

        }

        if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Voting").transform.Find("VotingPanel").gameObject.active == false)
        {

            map.SetActive(toggle);

        }

    }
    
}
