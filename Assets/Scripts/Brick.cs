﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brick : MonoBehaviour {
    public AudioClip crack;
    public Sprite[] hitSprites;
    public static int breakableCount = 0;
    public GameObject smoke;

    private int timesHit;
    private LevelManager levelManager;
    private bool isBreakable;

	// Use this for initialization
	void Start () {
        isBreakable = (this.tag == "breakable");
        if (isBreakable) {
            breakableCount++;
        }
        timesHit = 0;
        levelManager = GameObject.FindObjectOfType<LevelManager>();
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        AudioSource.PlayClipAtPoint(crack, transform.position, 0.8f);
        if (isBreakable) {
            HandleHits();
        }
    }

    private void HandleHits() {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits) {
            breakableCount--;
            levelManager.BrickDestroy();
            PuffSmoke();
            Destroy(gameObject);
        } else {
            LoadSprites();
        }
    }

    private void PuffSmoke() {
        GameObject smokePuff = Instantiate(smoke, transform.position, Quaternion.identity);
        smokePuff.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    private void LoadSprites() {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null) {
            this.GetComponent<SpriteRenderer>().sprite = hitSprites [spriteIndex];
        } else {
            Debug.LogError("Brick sprite missing");
        }
    }
}
