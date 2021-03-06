using UnityEngine;
using System.Collections;


public class ThirdPersonNetworkVik : Photon.MonoBehaviour
{
    ThirdPersonCameraNET cameraScript;
    ThirdPersonControllerNET controllerScript;
    private bool appliedInitialUpdate;
	public Camera cam;
	GameManagerVik gameMan;
	//Game game;
	public int kills = 0; //should be switched over to customproperty on player
	public int deaths = 0; //should be switched over to customproperty on player


    void Awake()
    {
		//cam = transform.Find("Camera").GetComponent<Camera>();
		gameMan = GameObject.Find ("Code").GetComponent<GameManagerVik>();
		//game = gameMan.gameObject.GetComponent<Game>();
        cameraScript = GetComponent<ThirdPersonCameraNET>();
        controllerScript = GetComponent<ThirdPersonControllerNET>();

    }
    void Start()
    {
        //TODO: Bugfix to allow .isMine and .owner from AWAKE!
        if (photonView.isMine)
        {
            //MINE: local player, simply enable the local scripts
            cameraScript.enabled = true;
            controllerScript.enabled = true;
			cam.gameObject.SetActive (true);
			//GetComponent<MouseLook>().enabled = true;
			//cam.gameObject.GetComponent<MouseLook>().enabled = true;
			gameMan.mainCamObj.SetActive (false);
            //Camera.main.transform.parent = transform;
            //Camera.main.transform.localPosition = new Vector3(0, 2, -10);
            //Camera.main.transform.localEulerAngles = new Vector3(10, 0, 0);
			cam.transform.localPosition = new Vector3(0, 2, -10);
			cam.transform.localEulerAngles = new Vector3(10, 0, 0);
			
		}
        else
        {           
            cameraScript.enabled = false;
            controllerScript.enabled = true;
			//GetComponent<MouseLook>().enabled = false;//this is being set somewhere else but it wasnt working so I added this.
			//transform.Find("Camera").GetComponent<MouseLook>().enabled = false;

        }
        //controllerScript.SetIsRemotePlayer(!photonView.isMine);

        gameObject.name = gameObject.name + photonView.viewID;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //We own this player: send the others our data
           // stream.SendNext((int)controllerScript._characterState);
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(GetComponent<Rigidbody>().velocity); 
			//stream.SendNext(controllerScript.dashing);

        }
        else
        {
            //Network player, receive data
            //controllerScript._characterState = (CharacterState)(int)stream.ReceiveNext();
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
            GetComponent<Rigidbody>().velocity = (Vector3)stream.ReceiveNext();
			//controllerScript.dashing = (bool)stream.ReceiveNext();

            if (!appliedInitialUpdate)
            {
                appliedInitialUpdate = true;
                transform.position = correctPlayerPos;
                transform.rotation = correctPlayerRot;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }

    private Vector3 correctPlayerPos = Vector3.zero; //We lerp towards this
    private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this

    void Update()
    {
        if (!photonView.isMine)
        {
            //Update remote player (smooth this, this looks good, at the cost of some accuracy)
            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 5);
        }
		//if(kills >= game.winKill) {
		//	string winner = GetComponent<PhotonView>().owner.name;
		//	//Object[] winner = new Object[1];
		//	//winner[0] = GetComponent<PhotonView>().name;
		//	game.gameObject.GetComponent<PhotonView>().RPC ("gameover", PhotonTargets.All, winner);
		//}
    }

    void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        //We know there should be instantiation data..get our bools from this PhotonView!
        object[] objs = photonView.instantiationData; //The instantiate data..
        bool[] mybools = (bool[])objs[0];   //Our bools!

        //disable the axe and shield meshrenderers based on the instantiate data
        MeshRenderer[] rens = GetComponentsInChildren<MeshRenderer>();
		ExitGames.Client.Photon.Hashtable color = new ExitGames.Client.Photon.Hashtable() {{"c", PlayerPrefs.GetInt ("color")}};
		GetComponent<PhotonView> ().owner.SetCustomProperties(color);
		//transform.Find ("krab_death/Cube_006").GetComponent<ColorSwitch> ().colorSwap (PlayerPrefs.GetInt ("color"));
        //rens[0].enabled = mybools[0];//Axe
        //rens[1].enabled = mybools[1];//Shield
    }

}