﻿// Guney Ozsan - Out There (Music Video) - Real time music video in demoscene style for Unity 3D.
// Copyright (C) 2017 Guney Ozsan

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
// ---------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    public Transform newSun;
    public Transform newPlanet;

    public Transform sun;
    static Transform planet;

    static Sequencer sequencer;
    static int compareBar;

    Gravity testScript;



    void Awake()
    {
        sun = Instantiate(newSun, new Vector3(0, 0, 13), Quaternion.identity);
        sun.localScale = new Vector3(5, 5, 5);
        sun.parent = transform;
        sun.tag = "Sun";

        //Camera.main.GetComponent(SmoothFollow).target = sun;
        //Instantiate (planet, sun.transform.position + Vector3(10, 0, 0), Quaternion.identity);

        /*
            for (var i : int = 0; i < 1000; i++) {
                planet = Instantiate (newPlanet, Vector3(2*i, 0, 0), Quaternion.identity);
                planet.parent = transform;
                planet.GetComponent(Attraction).target = sun;
                planet.rigidbody.velocity = Vector3(0,0,.2);
            }
        */
    }



    void Start()
    {
        sequencer = GetComponent<Sequencer>();
        compareBar = sequencer.fastForwardToBar;
    }



    void Update()
    {
        //sun.transform.RotateAround (Vector3.zero, Vector3.up, 20 * Time.deltaTime);

        sun.transform.Rotate(0, 50 * Time.deltaTime, 0);

        if (sequencer.currentRegionID == 2 && compareBar != sequencer.currentBar)
        {
            SwitchAnimation(0, 10, 0);
            compareBar = sequencer.currentBar;
        }
        else if (sequencer.currentRegionID == 3 && compareBar != sequencer.currentBar)
        {
            SwitchAnimation(2, 0, -70);
            compareBar = sequencer.currentBar;
        }
        else if (sequencer.currentRegionID == 4 && compareBar != sequencer.currentBar)
        {
            SwitchAnimation(2, 0, -70);
            compareBar = sequencer.currentBar;
        }
        else if (sequencer.currentRegionID >= 5 && sequencer.currentRegionID <= 8 && compareBar != sequencer.currentBar)
        {
            SwitchAnimation(0, -300, 0);
            compareBar = sequencer.currentBar;
        }
        else if (sequencer.currentRegionID >= 10 && sequencer.currentRegionID <= 24 && compareBar != sequencer.currentBar)
        {
            SwitchAnimation(1, -300, 0);
            compareBar = sequencer.currentBar;
        }
        else if (sequencer.currentRegionID == 9 || sequencer.currentRegionID == 25)
        {
            TurnOffAnimation(0);
            compareBar = sequencer.currentBar;
            sun.GetComponent<Collider>().enabled = false;
            sun.GetComponent<Renderer>().enabled = false;
            sun.GetComponent<Transform>().localScale = Vector3.Lerp(sun.GetComponent<Transform>().localScale, new Vector3(0.1f, 0.1f, 0.1f), Time.deltaTime);
        }
    }



    void SwitchAnimation(int switcher, int gravityForce, int antiGravityForce)
    {
        if (compareBar % 2 == switcher)
        {
            foreach (GameObject planet in GameObject.FindGameObjectsWithTag("Planet"))
            {
                testScript = planet.GetComponent<Gravity>();
                testScript.forceMultiplier = gravityForce;
            }
        }
        else
        {
            foreach (GameObject planet in GameObject.FindGameObjectsWithTag("Planet"))
            {
                testScript = planet.GetComponent<Gravity>();
                testScript.forceMultiplier = antiGravityForce;
            }
        }
    }



    void TurnOffAnimation(int antiGravityForce)
    {
        foreach (GameObject planet in GameObject.FindGameObjectsWithTag("Planet"))
        {
            testScript = planet.GetComponent<Gravity>();
            testScript.forceMultiplier = antiGravityForce;
        }
    }
}
