using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour
{
    [SerializeField] private GameObject page1;
    [SerializeField] private GameObject page2;
    [SerializeField] private GameObject page3;
    [SerializeField] private GameObject page4;

    private void Start()
    {
        page1.SetActive(true);
        page2.SetActive(false);
        page3.SetActive(false);
        page4.SetActive(false);
    }


    public void Page1()
    {
        page1.SetActive(true);
        page2.SetActive(false);
        page3.SetActive(false);
        page4.SetActive(false);
    }

    public void Page2()
    {
        page1.SetActive(false);
        page2.SetActive(true);
        page3.SetActive(false);
        page4.SetActive(false);
    }

    public void Page3()
    {
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(true);
        page4.SetActive(false);
    }

    public void Page4()
    {
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(false);
        page4.SetActive(true);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }


}

