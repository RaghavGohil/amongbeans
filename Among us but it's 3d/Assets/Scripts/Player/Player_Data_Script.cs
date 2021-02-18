using UnityEngine; //UnityEngine namespace.

public class Player_Data_Script : MonoBehaviour // Inherits the properties.
{
    //~PhotonView;
    //private PhotonView view;
    //~Enums:
    public enum Player_type
    {
        
        Player,
        Imposter, //Imposter set
        Crewmate, // Crewmate set
        
    }

    public enum Player_status
    {

        None, //Set none
        Alive, //Player is alive set.
        Dead, // Player is dead set.

    }

    //~Player_type
    public Player_type playerTypeEnum = Player_type.Player;
    public Player_status playerStatusEnum = Player_status.Alive;

    //~Color_Data:
    public string color = "";

    private void Start()
    {

        //view = GetComponent<PhotonView>();

    }

    private void LateUpdate()
    {
        color = gameObject.transform.Find("MainPlayer").gameObject.GetComponent<SkinnedMeshRenderer>().material.color.ToString();

    }

}