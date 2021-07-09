using UnityEngine;
using UnityEngine.Audio;
using System;

public class Player_Movement : MonoBehaviour
{

    //~GameObjects:
    public GameObject playerGameObject;

    //~Animators:
    public Animator playerAnim; //Get player animatiors

    //~Floats:
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float jumpSpeed;

    //~Vectors:
    [SerializeField]
    private Vector3 movement;
    [SerializeField]
    private Vector3 jump;

    //~Bools:
    public bool playerCanJump;
    public bool playerCanMove;
    private bool isGrounded;


    //~PhotonView:
    private PhotonView view; //Sync movements.

    private void Start()
    {

        view  = GetComponent<PhotonView>(); //Get the photon view.


        //Init var playerSpeed in editor.
        //Init var jumpSpeed in editor.
        if(view.isMine){
            playerCanJump = true; //player can jump from start.
            playerCanMove = true; //player can move from start.
        }
    }

    private void Update()
    {

        if(view.isMine){

            //Feed Input:
            movement = new Vector3(Input.GetAxis("Horizontal"),0f, Input.GetAxis("Vertical"));

            //Play animation of player. [Will play idle animation when not pressing any key.]
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S)  || Input.GetKey(KeyCode.D))
            {
                playerAnim.SetBool("Animation_Player_Movement_Bool" , true);
                playerAnim.SetBool("Animation_Player_Bool" , false); //IdleAnim
            }

            else
            {
                
                playerAnim.SetBool("Animation_Player_Movement_Bool" , false);
                playerAnim.SetBool("Animation_Player_Bool" , true); //IdleAnim

            }

            if ( Input.GetButtonDown( "Horizontal" ) || Input.GetButtonDown( "Vertical" ) )
                gameObject.GetComponent<SoundHandler>().PlaySFX("Walk");
            else if (!Input.GetButton( "Horizontal" ) && !Input.GetButton( "Vertical" )) //Play Sounds
                gameObject.GetComponent<SoundHandler>().StopSFX("Walk");

            if ((playerCanJump == true && Input.GetKeyDown(KeyCode.Space)) && isGrounded)
            {

                Jump();//MethodCall

            }
        }

    }

    private void FixedUpdate()
    {
        if(view.isMine){
            //AddForceDown(); //MethodCall
            MovePlayer();//MethodCall
        }
    }

    private void AddForceDown()
    {
        //Adding force for proper collider detection.
        Vector3 forceDown = new Vector3(0f, -0.1f, 0f);//Collider Force.
        playerGameObject.GetComponent<Rigidbody>().AddForce(forceDown);
    }

    private void MovePlayer()
    {

        //Calculate movement
        if(playerCanMove)
            playerGameObject.transform.Translate(movement*Time.deltaTime*playerSpeed); //Show movement.

    }

    private void Jump()
    {

        //Calculate Jump
        jump = new Vector3(0f, jumpSpeed, 0f);
        playerGameObject.GetComponent<Rigidbody>().AddForce(jump, ForceMode.Impulse); //Show Jump

    }

    private void ResetJump()
    {
        playerCanJump = true;
        isGrounded = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(view.isMine){
            if (other.gameObject.CompareTag("Ground"))
            {
                ResetJump();
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if(view.isMine){
            if (other.gameObject.CompareTag("Ground"))
            {
                playerCanJump = false;
                isGrounded = false;
            }
        }
    }
}
