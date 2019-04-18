using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRespawn{

    
    Vector3[] respawnPoints { get; set;}
    void respawn();
    void getRespawnPoints();
}
