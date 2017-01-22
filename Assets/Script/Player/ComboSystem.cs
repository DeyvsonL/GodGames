using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ComboSystem
{

	public KeyCode[] keys;
	public float[] inBetweenTimes;
	public int index;
	public float inBetweenTime;
	public float lastKeyPressTime;

	public ComboSystem (KeyCode[] _k)
	{
		index = 0;
		lastKeyPressTime = 0.0f;
		keys = _k;
		inBetweenTimes = new float[_k.Length];
		for (int i = 0; i < _k.Length; i++) {
			inBetweenTimes [i] = 1.0f;
		}
	}

	public ComboSystem (KeyCode[] _k, float inBetweenTime)
	{
		index = 0;
		lastKeyPressTime = 0.0f;
		keys = _k;
		inBetweenTimes = new float[_k.Length];
		for (int i = 0; i < _k.Length; i++) {
			inBetweenTimes [i] = inBetweenTime;
		}
	}

	public ComboSystem (KeyCode[] _k, float[] _inBetweenTimes)
	{
		inBetweenTimes = _inBetweenTimes;
		index = 0;
		lastKeyPressTime = 0.0f;
		keys = _k;
	}

	public bool Check (KeyCode key)
	{
		
		Debug.Log (key.CompareTo (keys [index]));

		if (Time.time > lastKeyPressTime + inBetweenTimes [index]) {
			index = 0;
			lastKeyPressTime = Time.time;

		} else if (key.CompareTo (keys [index]) == 0) {
			lastKeyPressTime = Time.time;
			index++;
		}

		if (index >= keys.Length) {
			index = 0;
			return true;

		}

		return false;
	}

	public bool Check ()
	{
		if (Time.time > lastKeyPressTime + inBetweenTimes [index]) {
			index = 0;
			lastKeyPressTime = Time.time;
		} else if (Input.GetKeyDown (keys [index])) {
			lastKeyPressTime = Time.time;
			index++;
		}

		if (index >= keys.Length) {
			index = 0;
			return true;

		}

		return false;
	}
}