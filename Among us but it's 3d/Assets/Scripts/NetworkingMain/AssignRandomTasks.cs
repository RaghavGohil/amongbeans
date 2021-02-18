using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AssignRandomTasks : MonoBehaviour
{
    // Start is called before the first frame update
    //~Int:
    private int[] randomAssign = new int[5];
    private List<int> list;
    private int Rand;
    //~Strings:
    private string[] tasks;
    private string[] taskTags;
    //~GameObject:
    private GameObject taskText;
    private GameObject hand;
    private GameObject crosshair;
    private GameObject useCrewmate;
    //Bools:
    private bool[] taskBools;
    private bool keyDown; // keydown check.
    public bool taskShowing;
    public bool cafeteriaWiring;
    public bool securityWiring;
    public bool lowerEngineWiring;
    public bool upperEngineWiring;
    public bool reactorWiring;
    public bool shieldsWiring;
    public bool storageWiring;
    public bool adminWiring;
    public bool navigationWiring;
    public bool o2Wiring;
    public bool weaponsWiring;
    public bool medbay;
    public bool navigationDownload;
    public bool cafeteriaDownload;
    public bool adminDownload;
    public bool weaponsDownload;
    public bool electricalDownload;
    public bool commsDownload;
    public bool lowerEngineTemperature;
    public bool upperEngineTemperature;
    public bool startReactor;
    public bool startShields;
    //~Camera:
    private Camera camera;
    //~RayCastHit:
    private RaycastHit hit;
    //Byte;
    private byte E_TaskEvent = 3;
    //RaiseEvent:
    private RaiseEventOptions options;
    //PhotonView:
    private PhotonView view;
    private void Start()
    {


        view = GetComponent<PhotonView>();

        Set();

        if(!view.isMine)
        {

            this.enabled = false; //if not mine , disable.

        }

        options = new RaiseEventOptions() //Options for raiseevent.
        {

            CachingOption = EventCaching.DoNotCache,
            Receivers = ReceiverGroup.All,

        };

        taskShowing = false;
        crosshair = gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("CrosshairHolder").transform.Find("Crosshair").gameObject;
        hand = gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("CrosshairHolder").transform.Find("Hand").gameObject;
        useCrewmate = gameObject.transform.Find("Canvas").transform.Find("CrewmateUIElement").transform.Find("Use").gameObject;
        camera = gameObject.transform.Find("Main Camera").GetComponent<Camera>();

        taskText = gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("Task Panel").transform.Find("Panel").transform.Find("AssignerTasks").gameObject;
        taskText.GetComponent<Text>().text = ""; //Set null
        tasks = new string[]{"-Fix wiring at cafeteria" , "-Fix wiring at Security" , "-Fix wiring at UpperEngine" , "-Fix wiring at LowerEngine" , "-Fix wiring at Reactor" , "-Fix wiring at Storage" , "-Fix wiring at Admin" , "-Fix wiring at Shields" , "-Fix wiring at Navigation" , "-Fix wiring at O2" , "-Fix wiring at Weapons" ,"-Get a Medbay Scan (cause you sik)" , "-Download data at Navigation" , "-Download data at Cafeteria" , "-Download data at Admin" , "-Download data at Weapons" , "-Download data at Electrical" , "-Download data at Comms" , "-Change Lower Engine Temperature" , "-Change Upper Engine Temperature" , "-Start Reactor" , "-Start Shields"};


        list = new List<int>(new int[5]);
 
        for (int j = 0; j < 5; j++)
        {
            Rand = Random.Range(0,tasks.Length);
 
            while (list.Contains(Rand))
            {
                Rand = Random.Range(0,tasks.Length);
            }
 
            list[j] = Rand;
            randomAssign[j] = (int)list[j];
        }

        for(int i = 0 ; i < randomAssign.Length ; i++)
        {

            taskText.GetComponent<Text>().text += tasks[randomAssign[i]] + "\n";

        }
        

    }


    private bool FindExistence(int[] arr , int pointer) // Finds if the thing exists in the array or not.
    {


        for(int i = 0 ; i < arr.Length ; i++)
        {

            if(arr[i] == pointer)
            {

                return true;

            }

        }

        return false;

    }

    private void Set() //Set the values of tasks to true at start. 
    {
        keyDown = false;

        cafeteriaWiring = true;
        securityWiring = true;
        lowerEngineWiring = true;
        upperEngineWiring = true;
        reactorWiring = true;
        shieldsWiring = true;
        storageWiring = true;
        adminWiring = true;
        navigationWiring = true;
        o2Wiring = true;
        weaponsWiring = true;
        medbay = true;
        navigationDownload = true;
        cafeteriaDownload = true;
        adminDownload = true;
        weaponsDownload = true;
        electricalDownload = true;
        commsDownload = true;
        lowerEngineTemperature = true;
        upperEngineTemperature = true;
        startReactor = true;
        startShields = true;

        taskTags = new string[]{"CafeteriaWiring" , "SecurityWiring" , "UpperEngineWiring" , "LowerEngineWiring" , "ReactorWiring" , "StorageWiring" , "AdminWiring" , "ShieldsWiring" , "NavigationWiring" , "O2Wiring", "WeaponsWiring" , "Medbay" , "NavigationDownload" , "CafeteriaDownload" , "AdminDownload" , "WeaponsDownload" , "ElectricalDownload" , "CommsDownload" , "LowerEngineTemperature" , "UpperEngineTemperature" , "StartReactor" , "StartShields"}; 
        taskBools = new bool[]{cafeteriaWiring , securityWiring , upperEngineWiring , lowerEngineWiring , reactorWiring , shieldsWiring , storageWiring , adminWiring , navigationWiring , o2Wiring , weaponsWiring , medbay , navigationDownload , cafeteriaDownload , adminDownload , weaponsDownload , electricalDownload , commsDownload , lowerEngineTemperature , upperEngineTemperature , startReactor , startShields};
    }


    // Update is called once per frame
    private void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Tab))
        {

            keyDown = true;

        }

        if(Input.GetKeyUp(KeyCode.Tab))
        {

            keyDown = false;

        }

    }
    private void OnTriggerEnter(Collider other)
    {   

        for(int i = 0; i < taskTags.Length ; i++){
            if(other.gameObject.CompareTag(taskTags[i]))
            {

                hand.SetActive(true);
                crosshair.SetActive(false);
                useCrewmate.gameObject.GetComponent<RawImage>().color = new Color(1,1,1,1f);

            }
        }

    }
    private void OnTriggerStay(Collider other) //Handle tasks.
    {

        if(gameObject.GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Crewmate)
        {
            
            if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing == true)
            {
                
                gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Wiring").gameObject.SetActive(false);
                gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("ChangeTemperature").gameObject.SetActive(false);
                gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Download").gameObject.SetActive(false);
                gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Shields").gameObject.SetActive(false);
                gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("StartReactor").gameObject.SetActive(false);
                gameObject.GetComponent<AssignRandomTasks>().taskShowing = false;

                /**if(gameObject.GetComponent<DownloadTask>().cr_download)
                {

                    StopCoroutine(gameObject.GetComponent<DownloadTask>().WaitForTask());

                }

                if(gameObject.GetComponent<StartReactor>().cr_startreactor)
                {

                    StopCoroutine(gameObject.GetComponent<StartReactor>().WaitForTask());

                }**/
            }

            if(other.gameObject.CompareTag("CafeteriaWiring") && FindExistence(randomAssign , 0))
            {

                if(keyDown && cafeteriaWiring)
                {
                    gameObject.GetComponent<WIringTask>().Reset();
                    gameObject.GetComponent<WIringTask>().StartTask_Wiring("cafeteria");
                    if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Wiring").gameObject.active == true){
                        cafeteriaWiring = false;
                        taskShowing = true;
                    }
                }

            }
            
            if(other.gameObject.CompareTag("SecurityWiring") && FindExistence(randomAssign , 1))
            {

                if(keyDown && securityWiring)
                {
                    gameObject.GetComponent<WIringTask>().Reset();
                    gameObject.GetComponent<WIringTask>().StartTask_Wiring("security");
                    if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Wiring").gameObject.active == true){
                        securityWiring = false;
                        taskShowing = true;
                    }
                }

            }
            

            if(other.gameObject.CompareTag("UpperEngineWiring") && FindExistence(randomAssign , 2))
            {

                if(keyDown && upperEngineWiring)
                {
                    gameObject.GetComponent<WIringTask>().Reset();
                    gameObject.GetComponent<WIringTask>().StartTask_Wiring("upperengine");
                    if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Wiring").gameObject.active == true){
                        upperEngineWiring = false;
                        taskShowing = true;
                    }
                }

            }
            

            if(other.gameObject.CompareTag("LowerEngineWiring") && FindExistence(randomAssign , 3))
            {

                if(keyDown && lowerEngineWiring)
                {
                    gameObject.GetComponent<WIringTask>().Reset();
                    gameObject.GetComponent<WIringTask>().StartTask_Wiring("lowerengine");
                    if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Wiring").gameObject.active == true){
                        lowerEngineWiring = false;
                        taskShowing = true;
                    }
                }

            }

            if(other.gameObject.CompareTag("ReactorWiring") && FindExistence(randomAssign , 4))
            {

                if(keyDown && reactorWiring)
                {
                    gameObject.GetComponent<WIringTask>().Reset();
                    gameObject.GetComponent<WIringTask>().StartTask_Wiring("reactor");
                    if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Wiring").gameObject.active == true){
                        reactorWiring = false;
                        taskShowing = true;
                    }
                }

            }

            if(other.gameObject.CompareTag("StorageWiring") && FindExistence(randomAssign , 5))
            {

                if(keyDown && storageWiring)
                {
                    gameObject.GetComponent<WIringTask>().Reset();
                    gameObject.GetComponent<WIringTask>().StartTask_Wiring("storage");
                    if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Wiring").gameObject.active == true){
                        storageWiring = false;
                        taskShowing = true;
                    }
                }

            }

            if(other.gameObject.CompareTag("AdminWiring") && FindExistence(randomAssign , 6))
            {

                if(keyDown && adminWiring)
                {
                    gameObject.GetComponent<WIringTask>().Reset();
                    gameObject.GetComponent<WIringTask>().StartTask_Wiring("admin");
                    if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Wiring").gameObject.active == true){
                        adminWiring = false;
                        taskShowing = true;
                    }
                }

            }

            if(other.gameObject.CompareTag("ShieldsWiring") && FindExistence(randomAssign , 7))
            {

                if(keyDown && shieldsWiring)
                {
                    gameObject.GetComponent<WIringTask>().Reset();
                    gameObject.GetComponent<WIringTask>().StartTask_Wiring("shields");
                    if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Wiring").gameObject.active == true){
                        shieldsWiring = false;
                        taskShowing = true;
                    }
                }

            }

            if(other.gameObject.CompareTag("NavigationWiring") && FindExistence(randomAssign , 8))
            {

                if(keyDown && navigationWiring)
                {
                    gameObject.GetComponent<WIringTask>().Reset();
                    gameObject.GetComponent<WIringTask>().StartTask_Wiring("navigation");
                    if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Wiring").gameObject.active == true){
                        navigationWiring = false;
                        taskShowing = true;
                    }
                }

            }

            if(other.gameObject.CompareTag("O2Wiring") && FindExistence(randomAssign , 9))
            {

                if(keyDown && o2Wiring)
                {
                    gameObject.GetComponent<WIringTask>().Reset();
                    gameObject.GetComponent<WIringTask>().StartTask_Wiring("o2");
                    if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Wiring").gameObject.active == true){
                        o2Wiring = false;
                        taskShowing = true;
                    }
                }

            }

            if(other.gameObject.CompareTag("WeaponsWiring") && FindExistence(randomAssign , 10))
            {

                if(keyDown && weaponsWiring)
                {
                    gameObject.GetComponent<WIringTask>().Reset();
                    gameObject.GetComponent<WIringTask>().StartTask_Wiring("weapons");
                    if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Wiring").gameObject.active == true){
                        weaponsWiring = false;
                        taskShowing = true;
                    }
                }

            }

            if(other.gameObject.CompareTag("Medbay") && FindExistence(randomAssign , 11))
            {

                if(keyDown && medbay)
                {
                    PhotonNetwork.RaiseEvent(E_TaskEvent , 1 , true ,options);
                    gameObject.GetComponent<SoundHandler>().PlaySFX("TaskComplete");
                    medbay = false;
                }

            }

            if(other.gameObject.CompareTag("NavigationDownload") && FindExistence(randomAssign , 12))
            {

                if(keyDown && navigationDownload)
                {
                    gameObject.GetComponent<DownloadTask>().Reset();
                    gameObject.GetComponent<DownloadTask>().StartTask_Download("navigation");
                    if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Download").gameObject.active == true){
                        navigationDownload = false;
                        taskShowing = true;
                    }
                }

            }

            if(other.gameObject.CompareTag("CafeteriaDownload") && FindExistence(randomAssign , 13))
            {

                if(keyDown && cafeteriaDownload)
                {
                    gameObject.GetComponent<DownloadTask>().Reset();
                    gameObject.GetComponent<DownloadTask>().StartTask_Download("cafeteria");
                    if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Download").gameObject.active == true){
                        cafeteriaDownload = false;
                        taskShowing = true;
                    }
                }

            }

            if(other.gameObject.CompareTag("AdminDownload") && FindExistence(randomAssign , 14))
            {

                if(keyDown && adminDownload)
                {
                    gameObject.GetComponent<DownloadTask>().Reset();
                    gameObject.GetComponent<DownloadTask>().StartTask_Download("admin");
                    if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Download").gameObject.active == true){
                        adminDownload = false;
                        taskShowing = true;
                    }
                }

            }

            if(other.gameObject.CompareTag("WeaponsDownload") && FindExistence(randomAssign , 15))
            {

                if(keyDown && weaponsDownload)
                {
                    gameObject.GetComponent<DownloadTask>().Reset();
                    gameObject.GetComponent<DownloadTask>().StartTask_Download("weapons");
                    if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Download").gameObject.active == true){
                        weaponsDownload = false;
                        taskShowing = true;
                    }
                }

            }

            if(other.gameObject.CompareTag("ElectricalDownload") && FindExistence(randomAssign , 16))
            {

                if(keyDown && electricalDownload)
                {
                    gameObject.GetComponent<DownloadTask>().Reset();
                    gameObject.GetComponent<DownloadTask>().StartTask_Download("electrical");
                    if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Download").gameObject.active == true){
                        electricalDownload = false;
                        taskShowing = true;
                    }
                }

            }

            if(other.gameObject.CompareTag("CommsDownload") && FindExistence(randomAssign , 17))
            {

                if(keyDown && commsDownload)
                {
                    gameObject.GetComponent<DownloadTask>().Reset();
                    gameObject.GetComponent<DownloadTask>().StartTask_Download("comms");
                    if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Download").gameObject.active == true){
                        commsDownload = false;
                        taskShowing = true;
                    }
                }

            }

            if(other.gameObject.CompareTag("LowerEngineTemperature") && FindExistence(randomAssign , 18))
            {

                if(keyDown && lowerEngineTemperature)
                {
                    gameObject.GetComponent<ChangeTemperature>().Reset();
                    gameObject.GetComponent<ChangeTemperature>().StartTask_ChangeTemperature("lowerengine");
                    if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("ChangeTemperature").gameObject.active == true){
                        lowerEngineTemperature = false;
                        taskShowing = true;
                    }
                }

            }

            if(other.gameObject.CompareTag("UpperEngineTemperature") && FindExistence(randomAssign , 19))
            {

                if(keyDown && upperEngineTemperature)
                {
                    gameObject.GetComponent<ChangeTemperature>().Reset();
                    gameObject.GetComponent<ChangeTemperature>().StartTask_ChangeTemperature("upperengine");
                    if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("ChangeTemperature").gameObject.active == true){
                        upperEngineTemperature = false;
                        taskShowing = true;
                    }
                }

            }

            if(other.gameObject.CompareTag("StartReactor") && FindExistence(randomAssign , 20))
            {

                if(keyDown && startReactor)
                {
                    gameObject.GetComponent<StartReactor>().Reset();
                    gameObject.GetComponent<StartReactor>().StartTask_StartReactor("reactor");
                    if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("StartReactor").gameObject.active == true){
                        startReactor = false;
                        taskShowing = true;
                    }
                }

            }

            if(other.gameObject.CompareTag("StartShields") && FindExistence(randomAssign , 21))
            {

                if(keyDown && startShields)
                {
                    gameObject.GetComponent<SheildsTask>().Reset();
                    gameObject.GetComponent<SheildsTask>().StartTask_Sheilds("shields");
                    if(gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Shields").gameObject.active == true){
                        startShields = false;
                        taskShowing = true;

                    }
                }

            }

        }

    }

    private void OnTriggerExit(Collider other) //Trigger Exit Controller handles the ui for the player(use button)
    {   

        for(int i = 0; i < taskTags.Length ; i++){
            if(other.gameObject.CompareTag(taskTags[i]))
            {

                hand.SetActive(false);
                crosshair.SetActive(true);
                useCrewmate.gameObject.GetComponent<RawImage>().color = new Color(1,1,1,0.4f);
            }
        }

    }

    //Event handlers
    private void OnEnable()
    {

        PhotonNetwork.OnEventCall += NetworkingClient_EventRecieved;

    }

    private void OnDisable()
    {

        PhotonNetwork.OnEventCall -= NetworkingClient_EventRecieved;

    }

    private void NetworkingClient_EventRecieved(byte eventCode, object content, int senderId)
    {
        
        if(eventCode == E_TaskEvent)
        {

            gameObject.GetComponent<TotalTasks>().taskSlider.GetComponent<Slider>().value += (int)content;

        }

    }
}
