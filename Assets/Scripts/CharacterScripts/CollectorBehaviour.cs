using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorBehaviour : MonoBehaviour {

    PlayerAudioController audioControl;
    void Awake()
    {
        audioControl = GetComponent<PlayerAudioController>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (collision.gameObject.GetComponent<ICollectable>() != null)
        {
            collision.gameObject.GetComponent<ICollectable>().collect();
            if(tag == "Gem")
            {
                audioControl.playGem();
            }else if(tag == "GemShard")
            {
                audioControl.playGemShard();
            }
        }
    }
}
