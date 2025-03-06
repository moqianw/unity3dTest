using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class video : MonoBehaviour
{

    public VideoPlayer player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        player.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
