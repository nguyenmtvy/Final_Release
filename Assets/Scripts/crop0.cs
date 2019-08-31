using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crop0 : MonoBehaviour
{
    // Start is called before the first frame update
    public float time = 9.0f;
    public float timeLeft;
    public float adoptTime;
    public bool cropAvailable;
    GameObject timeBar;
    GameObject backgroundBar;
    Animator cropAnimator;
    public int levelID;
    private void Awake() {
        adoptTime = time;
    }
    void Start()
    {
        cropAnimator = GetComponent<Animator>();
        cropAvailable = false;
        timeLeft = time;
        timeBar = this.gameObject.transform.GetChild(1).gameObject;
        backgroundBar = this.gameObject.transform.GetChild(0).gameObject;
        timeBar.transform.localScale = new Vector3(0f, backgroundBar.transform.localScale.y, backgroundBar.transform.localScale.z);
        StartCoroutine (decreaseTimeBar());
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft == 0) {
            backgroundBar.SetActive(false);
            timeBar.SetActive(false);
            cropAvailable = true;
            cropAnimator.SetBool("cropAvailable", cropAvailable);
        }
    }
    private IEnumerator decreaseTimeBar() {
        adoptTime = time - adoptTime;
        cropAnimator.Play("growing", 0, adoptTime/time);
        for (float i=adoptTime; i<=time; i++){
            timeLeft = time - i;
            timeBar.transform.localScale = new Vector3(backgroundBar.transform.localScale.x*i/time, backgroundBar.transform.localScale.y, backgroundBar.transform.localScale.z);
            yield return new WaitForSeconds (1.0f);
        }
    }
}