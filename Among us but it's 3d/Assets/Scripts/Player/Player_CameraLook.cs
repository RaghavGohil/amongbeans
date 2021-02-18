using UnityEngine; //Unity Main
using UnityEngine.SceneManagement;

public class Player_CameraLook : MonoBehaviour
{

    //~GameObjects:
    public GameObject playerCamera; //Get Camera
    public GameObject playerGameObject; //Get Player

    //~Floats:
    [SerializeField]
    private float sensitivity; // sens field
    private Quaternion mouseInY;
    //~Bools:
    public bool cursorLocked; // Set to false.
    public bool canRotateCamera; //disable if needed.

    //~PhotonView;
    private PhotonView view;

    private void Start()
    {

        //Photon view compo:

        view = GetComponent<PhotonView>();
        canRotateCamera = true;

        Cursor.visible = false;

        //Init sensitivity in editor;
        if(view.isMine)
        {
            cursorLocked = true;
        }

        mouseInY = playerCamera.transform.localRotation;
    }

    private void Update()
    {

        if(view.isMine)
        {   
            //Calculate rotation for camera.
            if(canRotateCamera)
            {   
                mouseInY.x += -(Input.GetAxis("Mouse Y"));
                mouseInY.x = Mathf.Clamp(mouseInY.x , -90, 90);
                playerCamera.transform.localRotation = Quaternion.Euler(mouseInY.x * sensitivity, 0f, 0f);
                playerGameObject.transform.Rotate(new Vector3(0f, (Input.GetAxis("Mouse X")) * sensitivity, 0f)); //Rotate transform
            }
            //Get input for cursor management.
            //if(Input.GetKeyDown(KeyCode.P))
            //{
            //    cursorHidden = !cursorHidden;
            //    cursorLocked = !cursorLocked;
            //}
        }

        DisableOtherCamerasOnPlay();//Disable cameras.
        DisableAudioListeners(); //Disable listeners.
        //EnableMouseOperation(); //Enable Operation On other scenes.
    }

    private void LateUpdate()
    {

        //if(view.isMine)
        //{
        //    if (cursorHidden == false && cursorLocked == true) //Locking Cursor press : "P"
        //    {
        //        Cursor.visible = cursorHidden;
        //        Cursor.lockState = CursorLockMode.Locked;
        //    }

        //    if(cursorHidden == true && cursorLocked == false)
        //    {
        //        Cursor.visible = cursorHidden;
        //        Cursor.lockState = CursorLockMode.Confined;
        //    }
        //}

    }

    private void DisableOtherCamerasOnPlay()
    {

        if(!view.isMine)
        {
            playerGameObject.transform.Find ("Main Camera").gameObject.SetActive(false);
            //playerCamera.transform.LookAt(playerGameObject.transform);
        }

    }

    private void DisableAudioListeners()
    {

        if(!view.isMine)
        {
            playerGameObject.transform.Find ("Main Camera").gameObject.GetComponent<AudioListener>().enabled = false;
            //playerCamera.transform.LookAt(playerGameObject.transform);
        }

    }

    private void EnableMouseOperation()
    {
        if(cursorLocked)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.Confined;

    }

}
