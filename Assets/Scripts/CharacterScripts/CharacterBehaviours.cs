using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviours : MonoBehaviour {


    [SerializeField]
    private bool collisionNotifier;
    [SerializeField]
    private bool collector;


    /*WaterDeathBehaviour waterDeathBehaviour;
    TutorialBehaviour tb;*/
    private void Start()
    {
        if (collisionNotifier)
        {
            gameObject.AddComponent(typeof(CollisionNotifier));
        }
        if (collector)
        {
            gameObject.AddComponent(typeof(CollectorBehaviour));
        }
        
    }
}
