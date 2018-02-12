using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LossColider : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        LevelManager levelManager = new LevelManager();
        levelManager.LoadLevel("Loss");
    }
}
