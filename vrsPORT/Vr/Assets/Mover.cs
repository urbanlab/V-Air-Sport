﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
    CharacterController _characterController;

	// Use this for initialization
	void Start () {
        _characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 speed = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            speed += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            speed += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, Time.deltaTime * 45f, 0));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, Time.deltaTime * -45f, 0));
        }
        _characterController.SimpleMove(transform.InverseTransformVector(speed));
	}
}
