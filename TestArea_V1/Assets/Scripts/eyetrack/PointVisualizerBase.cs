//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Base class for gaze/fixation point visualization.
/// </summary>
public class PointVisualizerBase : MonoBehaviour
{
    /// <summary>
    /// Draws a square GUI.Box at the location specified by the gazePoint.
    /// <para>
    /// Note: This method has to be called from the OnGUI() method!
    /// </para>
    /// </summary>
    /// <param name="gazePoint">The box location</param>
    /// <param name="size">Size of the sides of the square box, in pixels</param>
    /// <param name="color">Color of the box</param>
    /// <param name="title">Title of the box</param>
	/// 
	public GameObject hitObject;
	public List<Vector2> hitArray;
	public Vector2[] hitArray2;
	public int hitArrayMaxSize;
	public int hitIndex;
	public Vector2 lastFilteredPos;
	public Vector2 lastPos;
	
	public void DrawGUI(EyeXGazePoint gazePoint, float size, Color color)
    {

        var defaultStyle = GUI.skin.box;
        GUI.skin.box = CreateBoxStyle(color);

        if (gazePoint.IsWithinScreenBounds)
        {
            GUI.Box(new UnityEngine.Rect(gazePoint.GUI.x - size / 2.0f, gazePoint.GUI.y - size / 2.0f, size, size), "n");
        }

        GUI.skin.box = defaultStyle;


		//--------------------------------JL---------------------------------------------
		/*
		Vector2 screenSpace = gazePoint.Screen;
		//Vector2 filteredscreenSpace = getJLFilterValueSnappy(screenSpace);
		Vector2 filteredscreenSpace = getJLFilterValue(screenSpace);
		
		Ray ray = Camera.main.ScreenPointToRay(filteredscreenSpace);

		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 500.0F)) 
		{
			GameObject GO = GameObject.Find("Sphere");
			if(GO)
			{

				GO.transform.position = hit.point;

				Debug.Log(ray.origin.ToString() + " " + hit.point.ToString());
			}
		}
		*/
		//-----------------------------------------------------------------------------
    }
	void Start()
	{
		hitArrayMaxSize=20;
		hitIndex=0;
		hitArray = new List<Vector2>();
		lastFilteredPos = new Vector2();
		lastPos = new Vector2();
	}

	private Vector2 getJLFilterValueSnappy(Vector2 newHit)
	{
		//clear array if massive move has beet been made
		if(Vector2.Distance(newHit, lastPos)>20)
		{
			lastPos = newHit;
		}
		return lastPos;
	}

	private Vector2 getJLFilterValue(Vector2 newHit)
	{
		int i 	= 0;
		float x = 0;
		float y	= 0;
		Vector2 retVector;

		//always store last hit, may not exist in hit array becouse of filterd out
		lastPos = newHit;

		//clear array if massive move has beet been made
		if(Vector2.Distance(newHit, lastFilteredPos)>100 && Vector2.Distance(newHit, lastPos)<30)
		{
			hitArray.Clear();
		}
		//insert new value , remove oldest if reached limit of array.
		if (hitArray.Count < hitArrayMaxSize)
		{
			hitArray.Add(newHit);
		}
		else
		{
			hitArray.RemoveAt(0);
			hitArray.Add(newHit);
		}

		//get the average of all hitpoints
		foreach(Vector2 hit in hitArray)
		{
			x += hit.x;
			y += hit.y;
			i++;
		}

		retVector.x = x / i;
		retVector.y = y / i;

		lastFilteredPos = retVector;

		return retVector;
	}
	
	private GUIStyle CreateBoxStyle(Color color)
    {
        var style = new GUIStyle(GUI.skin.box);

        var texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        style.normal.background = texture;

        style.border = new RectOffset(0, 0, 0, 0);

        return style;
    }
}
