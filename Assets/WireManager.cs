using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class WireManager : MonoBehaviour
{
    public List<Color> _wireColors = new List<Color>();
    public List<Wire> LWire = new List<Wire>();
    public List<Wire> RWire = new List<Wire>();
    private List<Color> OpenToColor;
    private List<int> LeftWireIndex;
    private List<int> RightWireIndex;
    public Wire DraggedWire;
    public Wire ChosenWire;
    bool iseasy = false;
    bool ishard = false;
    public GameObject Menu;
    public GameObject WinScreen;
    float currentTime = 0;
    float startingTime = 0;
    public Text countdowntext;
    private bool gamestart = false;
    public bool CompleteDuty = false;

    
    private void Start()
    {
        currentTime = startingTime;
        OpenToColor = new List<Color>(_wireColors);
        LeftWireIndex = new List<int>();
        RightWireIndex = new List<int>();
        WinScreen.SetActive(false);
        for (int i = 0; i < LWire.Count; i++)
        {
            LeftWireIndex.Add(i);
        }
        for (int i = 0; i < RWire.Count; i++)
        {
            RightWireIndex.Add(i);
        }
        while (OpenToColor.Count > 0 &&
               LeftWireIndex.Count > 0 &&
               RightWireIndex.Count > 0)
        {
            Color pickedColor =
             OpenToColor[Random.Range(0, OpenToColor.Count)];

            int pickedLeftWireIndex = Random.Range(0, LeftWireIndex.Count);
            int pickedRightWireIndex = Random.Range(0,  RightWireIndex.Count);
            LWire[LeftWireIndex[pickedLeftWireIndex]].SetColor(pickedColor); 
            RWire[RightWireIndex[pickedRightWireIndex]].SetColor(pickedColor);

            OpenToColor.Remove(pickedColor);
            LeftWireIndex.RemoveAt(pickedLeftWireIndex);
            RightWireIndex.RemoveAt(pickedRightWireIndex);
        }

        StartCoroutine(CheckTaskCompletion());
    }
    private void Update()
    {
        if (gamestart == true)
        {
           
            currentTime -= 1 * Time.deltaTime;
            countdowntext.text = currentTime.ToString("0");

            if (currentTime <= 0)
            {

                SceneManager.LoadScene("Eeep");
               
            }
        }
        else
        {
            currentTime = startingTime;
        }
    }
    public void IsEasy()
    {
        iseasy = true;
        ishard = false;
        Menu.SetActive(false);
       currentTime = 30;
        gamestart = true;
    }

    public void IsHard()
    {
        iseasy =false;
        ishard = false;
        currentTime = 10;
        gamestart = true;

        Menu.SetActive(false);
    }
    private IEnumerator CheckTaskCompletion()
    {
        while (!CompleteDuty)
        {
            int successfulWires = 0;
            int difficultyhampEasy = 2;
            int difficultyhampHard = 4;

           
            for (int i = 0; i < RWire.Count; i++)
            {
                if (RWire[i].Success) { successfulWires++; }
            }
            if (successfulWires >= RWire.Count)
            {
                Debug.Log("Complete");
                WinScreen.SetActive(true);
               
            }
            else
            {

                Debug.Log(successfulWires);

            }
            if (iseasy == true)
            {
                if (successfulWires >= difficultyhampEasy)
                {
                    Debug.Log("Complete");
                    WinScreen.SetActive(true);
                    gamestart = false;
                }
            }
            if (ishard == true)
            {
                if (successfulWires >= difficultyhampHard)
                {
                    Debug.Log("Complete");
                    WinScreen.SetActive(true);
                    gamestart = false;


                }
            }


            yield return new WaitForSeconds(0.5f);
        }
    }
}
