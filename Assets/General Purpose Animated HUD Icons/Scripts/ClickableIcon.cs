using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableIcon : MonoBehaviour
{
    public string boolName;
    Animator stateMachine;
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        stateMachine.SetBool(boolName, !stateMachine.GetBool(boolName));
    }

    public void ToggleBool(string boolName)
    {
        stateMachine.SetBool(boolName, !stateMachine.GetBool(boolName));
    }
}
