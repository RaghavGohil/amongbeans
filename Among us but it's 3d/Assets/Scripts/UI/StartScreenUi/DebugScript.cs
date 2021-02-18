using UnityEngine;
using UnityEngine.UI;

public class DebugScript : Photon.MonoBehaviour
{

    //~GameObjects:
    public GameObject container;
    
    //~UI:
    public Text graphicsCard;
    public Text ram;
    public Text gameVersion;
    public Text fps;
    public Text serverPing;

    //~Floats;
    private float deltaTime;

    //~Bool:
    private bool showMain;

    void Start()
    {
        
        DontDestroyOnLoad(gameObject);
        showMain = false;

    }

    void Update()
    {
        if(showMain)
        {

            container.SetActive(true);

        }
        else if(!showMain)
        {

            container.SetActive(false);

        }
        graphicsCard.text = "Device graphics card: "+SystemInfo.graphicsDeviceName.ToString();
        ram.text = "Device ram: "+SystemInfo.systemMemorySize.ToString();
        gameVersion.text = "Game Version: "+"v0.2";
        serverPing.text = "Network Ping: "+(PhotonNetwork.GetPing()).ToString();

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps1 = 1.0f / deltaTime;
        fps.text = "Frames Per Second(FPS): "+Mathf.Ceil (fps1).ToString();

        if(Input.GetKeyDown(KeyCode.F3))
        {

            showMain = !showMain;

        }
        


    }


}
