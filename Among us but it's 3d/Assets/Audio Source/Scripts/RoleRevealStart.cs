using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class RoleRevealStart : Photon.MonoBehaviour
{

    //~GameObjects:
    public GameObject[] a_source;
    private GameObject player;
    
    //~Animator:
    private Animator anim;

    private void Start()
    {


        player = GameObject.FindWithTag("Player");

        anim = player.GetComponent<Animator>();
        anim.Play("Animation_Role_Reveal_Imposter"); //Both imposter and crewmate.
        DisableAudios();
        StartCoroutine(WaitForActivation());

    }

    private void DisableAudios()
    {

        for(int i = 0 ; i < a_source.Length ; i++)
        {

            a_source[i].SetActive(false);

        }

    }

    private void LateUpdate()
    {

        if(player.GetComponent<WinOrLose>().stopSound == true)
        {

            DisableAudios();

        }

    }

    private IEnumerator WaitForActivation()
    {

        yield return new WaitForSeconds(2.5f);

        player.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("Role").gameObject.SetActive(false);

        for(int i = 0 ; i < a_source.Length ; i++)
        {

            a_source[i].SetActive(true);

        }

    }

}
