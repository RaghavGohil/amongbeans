using UnityEngine;
using UnityEngine.UI; //Get the UI.

public class Minimap_Controller : MonoBehaviour
{
    //Script is for moving camera to the position of playerGameObject
    [SerializeField]
    public GameObject minimapCamera;
    public GameObject[] playersInGame;
    //public GameObject playerMarkerGameObject; // Get playerMarker

    //~Floats:
    [SerializeField]
    private float minimapCameraYOffset; // Set y offset.

    //~PhotonView:
    private PhotonView view;

    private void Start()
    {

      playersInGame = GameObject.FindGameObjectsWithTag("Player");

      //assign view:
      view = GetComponent<PhotonView>();

      AssignCameras();

      minimapCameraYOffset = 36.63f; // Set y offset.

      //playerMarkerGameObject = GameObject.FindWithTag("Marker");

      if(!view.isMine)
      {
        DisableOtherCameras();
      }
      
    }

    private void FixedUpdate()
    {

      //Calculate cameraVector
      Vector3 moveMinimapCamera = new Vector3(gameObject.transform.position.x,minimapCameraYOffset,gameObject.transform.position.z);
      //moveCam on calculation:
      minimapCamera.transform.position = moveMinimapCamera;

      //Calculation:
      //Vector3 rotateGameObject = new Vector3(0f,0f,gameObject.transform.eulerAngles.y+180);
      //playerMarker rotation;
      //playerMarkerGameObject.transform.rotation = Quaternion.Euler(-rotateGameObject);

    }

    private void DisableOtherCameras()
    {

        gameObject.transform.Find("PlayerMinimapCamera").gameObject.SetActive(false);

    }


    private void AssignCameras()
    {
      GetComponent<Minimap_Controller>().minimapCamera = gameObject.transform.Find("PlayerMinimapCamera").gameObject;
    }
    
}
