using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainText : MonoBehaviour {

    public Text results;
    private int index = 0;
    public MainText()
    {

    }

  	// Use this for initialization
	void Start () {
        results.text = "What's up?";
    }
	
	// Update is called once per frame
	void Update () {
      
    }

    public void updateText(string passedText)
    {
        results.text = passedText;
    }
}
