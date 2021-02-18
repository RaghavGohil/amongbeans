using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DownloadTask : MonoBehaviour
{

    //UI:
    private Slider downloadSlider;

    //Bool;
    private bool isDownloading;
    private bool passBool;

    //Byte;
    private byte E_TaskEvent = 3;

    //strings:
    private string placeName;

    //Coroutine:
    private Coroutine task;

    //RaiseEvent:
    private RaiseEventOptions options;

    // Start is called before the first frame update
    private void Start()
    {
        
        placeName = "";
        task = null;
        options = new RaiseEventOptions() //Options for raiseevent.
        {

            CachingOption = EventCaching.DoNotCache,
            Receivers = ReceiverGroup.All,

        };


        downloadSlider = gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Download").transform.Find("Panel").transform.Find("DownloadSlider").gameObject.GetComponent<Slider>();
        downloadSlider.value = 0f;

    }

    // Update is called once per frame
    private void Update()
    {
        
        if(isDownloading)
        {

            downloadSlider.value += (1*Time.deltaTime )/ 15;

        }

        if(isDownloading == false)
        {

            gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Download").gameObject.SetActive(false);
            Reset();

        }

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing && placeName == "navigation" && passBool)
        {

            StopCoroutine(task);

            gameObject.GetComponent<AssignRandomTasks>().navigationDownload = true;

            passBool = false;

        }

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing && placeName == "cafeteria" && passBool)
        {

            StopCoroutine(task);

            gameObject.GetComponent<AssignRandomTasks>().cafeteriaDownload = true;

            passBool = false;

        }

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing && placeName == "admin" && passBool)
        {

            StopCoroutine(task);

            gameObject.GetComponent<AssignRandomTasks>().adminDownload = true;

            passBool = false;

        }

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing && placeName == "weapons" && passBool)
        {

            StopCoroutine(task);

            gameObject.GetComponent<AssignRandomTasks>().weaponsDownload = true;

            passBool = false;

        }

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing && placeName == "electrical" && passBool)
        {

            StopCoroutine(task);

            gameObject.GetComponent<AssignRandomTasks>().electricalDownload = true;

            passBool = false;

        }

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing && placeName == "comms" && passBool)
        {

            StopCoroutine(task);

            gameObject.GetComponent<AssignRandomTasks>().commsDownload = true;

            passBool = false;

        }
        
    }

    public void Reset()//Reset datas.
    {

        isDownloading = false;
        placeName = "";
        passBool = true;
        downloadSlider.value = 0f;

    }

    public void StartTask_Download(string place) //Start Download
    {
        gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Download").gameObject.SetActive(true);
        task = StartCoroutine("WaitForTask");

        placeName = place;

    }

    public IEnumerator WaitForTask()
    {
        isDownloading = true;
        gameObject.GetComponent<Player_Movement>().playerCanMove = false;
        gameObject.GetComponent<Player_Movement>().playerCanJump = false;
        gameObject.GetComponent<Player_CameraLook>().canRotateCamera = false;
        Cursor.visible = true;
        gameObject.GetComponent<Player_CameraLook>().cursorLocked = false;
        yield return new WaitForSeconds(15f);
        isDownloading = false;
        PhotonNetwork.RaiseEvent(E_TaskEvent , 1 , true ,options);
        gameObject.GetComponent<SoundHandler>().PlaySFX("TaskComplete");
        gameObject.GetComponent<Player_Movement>().playerCanMove = true;
        gameObject.GetComponent<Player_Movement>().playerCanJump = true;
        gameObject.GetComponent<Player_CameraLook>().canRotateCamera = true;
        Cursor.visible = false;
        gameObject.GetComponent<Player_CameraLook>().cursorLocked = true;
        gameObject.GetComponent<AssignRandomTasks>().taskShowing = false;

    }
}
