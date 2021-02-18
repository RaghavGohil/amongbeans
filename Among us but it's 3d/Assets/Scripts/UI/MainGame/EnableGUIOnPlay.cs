using UnityEngine;
using UnityEngine.SceneManagement;

public class EnableGUIOnPlay : Photon.MonoBehaviour
{

    //~GameObjects:
    [SerializeField]
    private GameObject canvas;

    //~PhotonView:
    private PhotonView view;

    private void Start()
    {   

        view = GetComponent<PhotonView>();
        
        canvas = gameObject.transform.Find("Canvas").gameObject;

        if(SceneManager.GetActiveScene().name == "MainGame")
        {

            canvas.SetActive(true);

        }

        else{canvas.SetActive(false);}

    }

    private void LateUpdate()
    {

        if(!view.isMine)
        {
            DisableOtherCanvas();
        }

    }

    private void DisableOtherCanvas()
    {
        canvas.SetActive(false);
    }


}
