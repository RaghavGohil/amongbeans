using UnityEngine;
using TMPro;

public class PlayerNameController : MonoBehaviour
{

    private TextMeshPro NameText;
    private GameObject[] playersInGame;
    //private PhotonView v;

    private void Start()
    {

        NameText = gameObject.transform.Find("Name").gameObject.GetComponent<TextMeshPro>();
        playersInGame = GameObject.FindGameObjectsWithTag("Player");
        //v = GetComponent<PhotonView>();

    }

    private void LateUpdate()
    {

        NameText.text = gameObject.GetComponent<PhotonView>().owner.name;

    }
    


}
