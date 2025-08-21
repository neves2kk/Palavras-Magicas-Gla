using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlBoardController : MonoBehaviour
{

    public GLBoard gboard;

   async void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        gboard = new GLBoard("F6ypBJQWSCseX9hKNrNxsA",SystemInfo.deviceUniqueIdentifier);
        await gboard.LOAD_USER_DATA();
        StartCoroutine(gboard.SEND_USER_DATA());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
