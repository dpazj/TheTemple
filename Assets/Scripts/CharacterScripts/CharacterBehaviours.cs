using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviours : MonoBehaviour {


    [SerializeField]
    private bool waterKills;
    [SerializeField]
    private bool tutorialBehaviour;
    [SerializeField]
    private bool collector;


    /*WaterDeathBehaviour waterDeathBehaviour;
    TutorialBehaviour tb;*/
    private void Start()
    {
        if (waterKills)
        {
            gameObject.AddComponent(typeof(WaterDeathBehaviour));
        }
        if (tutorialBehaviour)
        {
            gameObject.AddComponent(typeof(TutorialBehaviour));
        }
        if (collector)
        {
            gameObject.AddComponent(typeof(CollectorBehaviour));
        }
        
    }
}
