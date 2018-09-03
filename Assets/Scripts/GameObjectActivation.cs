using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectActivation: MonoBehaviour {

    public void DeactiveGameObject ()
    {
        gameObject.SetActive(false);
    }
}
