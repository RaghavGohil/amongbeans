using UnityEngine;
using UnityEngine.UI;



public class ChangeTemperature : MonoBehaviour
{

    //Integer:
    private int randomInteger;
    private int mainInteger;

    //UI:
    private Text mainText;
    private Text randomText;

    //Bools:
    private bool taskIsOn;
    private bool passBool;
    
    //string;
    private string placeName;

    //Byte;
    private byte E_TaskEvent = 3;

    //RaiseEvent:
    private RaiseEventOptions options;

    // Start is called before the first frame update
    private void Start()
    {
        passBool = true;
        placeName = "";

        options = new RaiseEventOptions() //Options for raiseevent.
        {

            CachingOption = EventCaching.DoNotCache,
            Receivers = ReceiverGroup.All,

        };

        gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("ChangeTemperature").transform.Find("firpanel").transform.Find("Button").gameObject.GetComponent<Button>().onClick.AddListener(delegate { ButtonUp(); });
        gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("ChangeTemperature").transform.Find("firpanel").transform.Find("Button2").gameObject.GetComponent<Button>().onClick.AddListener(delegate { ButtonDown(); });
        mainInteger = 0;
        taskIsOn = false;
        mainText = gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("ChangeTemperature").transform.Find("firpanel").transform.Find("Text").gameObject.GetComponent<Text>();
        randomText = gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("ChangeTemperature").transform.Find("secpanel").transform.Find("Text").gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        
        mainText.text = mainInteger.ToString();
        randomText.text = randomInteger.ToString();

        if((mainInteger == randomInteger)&& taskIsOn)
        {

            PhotonNetwork.RaiseEvent(E_TaskEvent , 1 , true ,options);
            gameObject.GetComponent<SoundHandler>().PlaySFX("TaskComplete");
            gameObject.GetComponent<Player_Movement>().playerCanMove = true;
            gameObject.GetComponent<Player_Movement>().playerCanJump = true;
            gameObject.GetComponent<Player_CameraLook>().canRotateCamera = true;
            Cursor.visible = false;
            gameObject.GetComponent<Player_CameraLook>().cursorLocked = true;
            gameObject.GetComponent<AssignRandomTasks>().taskShowing = false;
            Reset();
            taskIsOn = false;
        }

        if(!taskIsOn)
        {

            gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("ChangeTemperature").gameObject.SetActive(false);
            

        }
        
        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing && placeName == "lowerengine" && passBool)
        {

            gameObject.GetComponent<AssignRandomTasks>().lowerEngineTemperature = true;
            print("placeName");
            passBool = false;

        }

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing && placeName == "upperengine" && passBool)
        {

            gameObject.GetComponent<AssignRandomTasks>().upperEngineTemperature = true;
            print("placeName");
            passBool = false;

        }

    }

    public void Reset()
    {

        passBool = true;
        placeName = "";
        randomInteger = 0;
        mainInteger = 0;
        taskIsOn = false;

    }

    public void StartTask_ChangeTemperature(string place)
    {

        placeName = place;

        taskIsOn = true;
        gameObject.GetComponent<Player_Movement>().playerCanMove = false;
        gameObject.GetComponent<Player_Movement>().playerCanJump = false;
        gameObject.GetComponent<Player_CameraLook>().canRotateCamera = false;
        Cursor.visible = true;
        gameObject.GetComponent<Player_CameraLook>().cursorLocked = false;
        gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("ChangeTemperature").gameObject.SetActive(true);
        randomInteger = (int)Random.Range(-20,20);

    }

    public void ButtonUp()
    {

        mainInteger += 1;

    }

    public void ButtonDown()
    {

        mainInteger -= 1;

    }

}
