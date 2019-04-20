using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLevelProperty : MonoBehaviour {


    [SerializeField]
    private bool waterKills;
    [SerializeField]
    private bool tutorialBehaviour;


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
    }
}
