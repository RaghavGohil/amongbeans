using UnityEngine;

public class StartScreen_UI_Handler : MonoBehaviour //UI PANEL HANDLER SCRIPT.
{

    //~Animators:
    private Animator anim; //Get the main component animator.
    
    private void Start()
    {

        anim = GetComponent<Animator>();

    }
    
    public void OnClickSwapOnlinePanel() //Swaps to the online panel.
    {

        anim.Play("Animation_UI_Online_To_OnlinePanel");

    }

    public void OnClickSwapHowToPlayPanel() //Swaps to the How to play panel.
    {

        anim.Play("Animation_UI_HowToPlay_To_HowToPlayPanel");

    }

    public void OnClickBack() //Swaps to the How to play panel.
    {

        anim.Play("Animation_UI_BackButton");

    }

    private void Update()
    {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

    }

}
