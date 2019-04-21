using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemShard : GemAnimator, ICollectable{
    public void collect()
    {
        gameObject.SetActive(false);  
    }
}
