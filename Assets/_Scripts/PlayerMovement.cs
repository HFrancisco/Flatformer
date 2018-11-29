using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

	public GameObject winPanel;
	public GameObject losePanel;

	private bool isStart;
	private bool isGameOver;
	private bool isWon;
	private bool canMove;
	private bool onGround;
	private Rigidbody rb;
	[SerializeField] private float speed;

	private float time;
	private Scene currScene;

	private Vector3 jump;
	[SerializeField] private float jumpForce = 2.0f;

	// Use this for initialization
	void Start () {

		currScene = SceneManager.GetActiveScene();
		if (currScene.name.Equals("Level1")) {
			time = 0.0f;
		} else {
			time = PlayerPrefs.GetFloat ("Time", 0);
		}

		winPanel.SetActive (false);
		losePanel.SetActive (false);

		canMove = true;
		isWon = false;
		isGameOver = false;
		isStart = false;
		onGround = true;
		rb = GetComponent<Rigidbody> ();

		jump = new Vector3 (0.0f, 5.0f, 0.0f);

	}
	
	// Update is called once per frame
	void Update () {

		time = time + Time.deltaTime;

        if (isWon){
            PlayerPrefs.SetString("Finish", "True");
			PlayerPrefs.SetFloat ("Time", time);
			PlayerPrefs.Save();
            canMove = false;
            winPanel.SetActive(true);
            // Show Win Panel
        }

		if (isStart && !isWon) {
			if (this.gameObject.transform.position.y <= 0.05f) {
                PlayerPrefs.SetString("Finish", "False");
                PlayerPrefs.Save();
                canMove = false;
				losePanel.SetActive (true);
				// Show Lose Panel
			}
		}
	}

	void FixedUpdate(){
		
		if (canMove) {

			Vector3 movement = new Vector3 (Input.acceleration.x, 0.0f, 0.0f);
			rb.velocity = movement * speed;

			if(Input.GetKeyDown(KeyCode.A)){
				this.transform.Translate (-Vector3.right * speed * Time.deltaTime);
			} else if(Input.GetKeyDown(KeyCode.D)){
				this.transform.Translate (Vector3.right * speed * Time.deltaTime);
			}

			// Jump
			if (onGround) {

				if (Input.GetKeyDown (KeyCode.X)) {

					Vector3 jumpMove = new Vector3();
					jumpMove = new Vector3 (2f, 25f, 0.0f);
					//rb.velocity = jumpMove * speed;
					rb.AddForce(jumpMove, ForceMode.Impulse);

					onGround = false;	

				} else if (Input.GetKeyDown (KeyCode.Z)) {

					Vector3 jumpMove = new Vector3();
					jumpMove = new Vector3 (-2f, 25f, 0.0f);
					//rb.velocity = jumpMove * speed;
					rb.AddForce(jumpMove, ForceMode.Impulse);

					onGround = false;	

				} 


				if (Input.touchCount > 0)
				{
					Touch touch = Input.GetTouch (0);

					if (touch.phase == TouchPhase.Began) {
						onGround = false;

						Vector3 jumpMove = new Vector3();

						if (Input.acceleration.x > 0) {
							jumpMove = new Vector3 (15f, 60f, 0.0f);
						} else if (Input.acceleration.x < 0) {
							jumpMove = new Vector3 (-15f, 60f, 0.0f);
						}

						rb.AddForce(jumpMove, ForceMode.Impulse);
					}
				}
			}
		}
	}

	void OnCollisionStay(Collision col){

		if (col.gameObject.CompareTag ("Platform")) {
			isStart = true;
			onGround = true;
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.CompareTag ("Goal")) {
			Debug.Log ("Goal");
			onGround = true;
			canMove = false;
			isWon = true;
		}
	}

	void OnCollisionExit(Collision col){

		if(col.gameObject.CompareTag("Platform")){
			onGround = false;
		}
	}

	public void LoadEndScene(){

		Scene currScene = SceneManager.GetActiveScene ();
		string currSceneName = currScene.name;

		if(currSceneName.Equals("Level1"))
			SceneManager.LoadScene("Level2");
		else if(currSceneName.Equals("Level2"))
			SceneManager.LoadScene("Level3");
		else if(currSceneName.Equals("Level3"))
			SceneManager.LoadScene("Menu");
		

	}

    public void LoadMainScence()
    {
        SceneManager.LoadScene("Menu");
    }
}
