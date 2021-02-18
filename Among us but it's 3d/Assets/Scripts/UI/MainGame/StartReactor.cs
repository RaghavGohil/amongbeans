using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class StartReactor : MonoBehaviour
{
    //UI:
    private Slider downloadSlider;

    //Bool;
    private bool isDownloading;
    private bool passBool;

    //String:
    private string placeName;

    //Coroutine:
    private Coroutine task;

    //Byte;
    private byte E_TaskEvent = 3;

    //RaiseEvent:
    private RaiseEventOptions options;

    // Start is called before the first frame update
    private void Start()
    {
        placeName = "";
        task = null;
        passBool = true;
        options = new RaiseEventOptions() //Options for raiseevent.
        {

            CachingOption = EventCaching.DoNotCache,
            Receivers = ReceiverGroup.All,

        };
        downloadSlider = gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("StartReactor").transform.Find("Panel").transform.Find("DownloadSlider").gameObject.GetComponent<Slider>();
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

            gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("StartReactor").gameObject.SetActive(false);
            Reset();

        }

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing && placeName == "reactor" && passBool)
        {

            StopCoroutine(task);

            gameObject.GetComponent<AssignRandomTasks>().startReactor = true;

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

    public void StartTask_StartReactor(string place) //Start Download
    {   
        placeName = place;
        gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("StartReactor").gameObject.SetActive(true);
        task = StartCoroutine("WaitForTask");

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
