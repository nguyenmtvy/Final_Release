using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class character : MonoBehaviour
{
    public btn_setting audiosetting;
    public float speed;
    public float movementSpeed;
    bool moveHorizontal = false;
    bool moveVertical = false;
    float changeAmount = 0.1f;
    public scene_manager manager;
    public GameObject respawnPoint;
    public GameObject mfarming_ground;
    private Animator playerAnimator;
    public AudioSource audioSource;
    public AudioController audioController;
    public bool globalMute;

    public int gold = 0;
    [SerializeField] public TextMeshProUGUI goldStr;

    // Update is called once per frame
    private void Awake() {
        playerAnimator = GetComponent<Animator>();
        manager = FindObjectOfType<scene_manager>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
        audioController = FindObjectOfType<AudioController>();
    }
    private void Start() {
        audioSource.volume = 0.5f;
        audioSource.mute = true;
    }
    void Update()
    {
        goldStr.text = gold.ToString();
        if (moveHorizontal){
            movementSpeed = 0;
            GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeed, GetComponent<Rigidbody2D>().velocity.y);
            moveHorizontal = false;
        }
        if (moveVertical){
            movementSpeed = 0;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, movementSpeed);
            moveHorizontal = false;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            moveHorizontal = true;
            movementSpeed = -speed;
            GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeed, GetComponent<Rigidbody2D>().velocity.y);
            transform.localScale = new Vector2(-0.5f, 0.5f);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveHorizontal = true;
            movementSpeed = speed;
            GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeed, GetComponent<Rigidbody2D>().velocity.y);
            transform.localScale = new Vector2(0.5f, 0.5f);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveVertical = true;
            movementSpeed = speed;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveVertical = true;
            movementSpeed = -speed;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, movementSpeed);
        }
        playerAnimator.SetFloat("Speed", Mathf.Abs (movementSpeed));
        if (Input.GetKeyDown((KeyCode.S)) ||  Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) ||Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
            if (globalMute == false){
                audioSource.mute = false;
                //muteUnmute();
            }
        }
        if (Input.GetKeyUp((KeyCode.S)) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)){
            if (globalMute == false){
                //muteUnmute();
                audioSource.mute = true;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col){
        //Debug.Log("character enter trigger");
        if (col.gameObject.tag == "farming_ground"){
            mfarming_ground = col.gameObject;
        }
        if (col.gameObject.tag == "sea"){
            manager.respawn();
        }
    }
    public void pullSetting(){
        Debug.Log("player pulling setting");
        try {
            audioSource.volume = this.audiosetting.volumeValue;
            audioSource.mute = this.audiosetting.mute;
            globalMute = this.audiosetting.mute;
        } catch{
            Debug.Log("audio player pull error");
        }
    }
    public void volumeUp(){
        audioSource.volume += changeAmount;
    }
    public void volumeDown(){
        audioSource.volume -= changeAmount;
    }
    public void globalMuteOrNot(){
        audioSource.mute = globalMute;
    }
    public void muteAudio(){
        audioSource.mute = !audioSource.mute;
    }
}