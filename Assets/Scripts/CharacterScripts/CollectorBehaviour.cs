using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorBehaviour : MonoBehaviour {

    PlayerAudioController audio;
    void Awake()
    {
        audio = GetComponent<PlayerAudioController>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (collision.gameObject.GetComponent<ICollectable>() != null)
        {
            collision.gameObject.GetComponent<ICollectable>().collect();
            if(tag == "Gem")
            {
                audio.playGem();
            }else if(tag == "GemShard")
            {
                audio.playGemShard();
            }
        }
    }
}
