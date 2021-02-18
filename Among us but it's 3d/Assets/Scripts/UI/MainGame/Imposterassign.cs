using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Imposterassign : MonoBehaviour
{
    
    private GameObject[] playersInGame;
    private bool run;

    void Start()
    {
        run = true;
        playersInGame = GameObject.FindGameObjectsWithTag("Player");

    }

    void Update()
    {
        if(run && SceneManager.GetActiveScene().name == "MainGame")
        {
            
            StartCoroutine(Wait());

        }
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        for(int i = 0; i<playersInGame.Length;i++)
        {

            if(playersInGame[i].GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Imposter)
            {

                playersInGame[i].AddComponent<ImposterNameAssign>();

            }

        }

    }

}
