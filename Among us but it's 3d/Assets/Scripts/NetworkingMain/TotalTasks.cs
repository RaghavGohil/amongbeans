using UnityEngine;
using UnityEngine.UI;

public class TotalTasks : MonoBehaviour
{
    
    //~GameObjects:
    public GameObject taskSlider;
    private GameObject[] playersInGame;

    private void Start()
    {

        playersInGame = GameObject.FindGameObjectsWithTag("Player");

        taskSlider = gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("Task Panel").transform.Find("Slider").gameObject;
        taskSlider.GetComponent<Slider>().maxValue = (playersInGame.Length-gameObject.GetComponent<Assigner>().numberOfImposters) * 5;

    }

    private void Update()
    {

        

    }



}
