using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public btn_setting setting;
    float changeAmount = 0.1f;
    AudioSource backgroundMusic;
    //AudioSource walkingSound;
    //AudioSource walkingSource;
    character player;
    // Start is called before the first frame update
    private void Awake() {
        backgroundMusic = this.gameObject.GetComponent<AudioSource>();
        backgroundMusic.volume = 0.5f;
        player = FindObjectOfType<character>();
        //walkingSound = player.audioSource;
    }
    void Start()
    {
        //walkingSound = player.audioSource;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void volumeUp(){
        backgroundMusic.volume += changeAmount;
        //walkingSound.volume += changeAmount;
    }
    public void volumeDown(){
        //walkingSound.volume -= changeAmount;
        backgroundMusic.volume -= changeAmount;
    }
    public void muteUnmute(){
        backgroundMusic.mute = !backgroundMusic.mute;
        player.globalMute = backgroundMusic.mute;
        player.globalMuteOrNot();
        //Debug.Log(backgroundMusic.mute);
        //player.globalMute = backgroundMusic;
    }
    public void pullSetting(){
        try {
            backgroundMusic.volume = setting.volumeValue;
            backgroundMusic.mute = setting.mute;
        } catch{
            Debug.Log("audio controller pull error");
        }
    }
    public void pushSetting(){
        try{
            setting.volumeValue = backgroundMusic.volume;
            setting.mute = backgroundMusic.mute;
        }
        catch{
            Debug.Log("audio controller push error");
        }
    }
}
