using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioSource music;
    
    // Start is called before the first frame update
    public void onMusic()
    {
        music.Play();
    }

    // Update is called once per frame
    public void offMusic()
    {
        music.Stop();
    }
}
