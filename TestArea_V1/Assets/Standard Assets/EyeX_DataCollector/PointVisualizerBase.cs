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
	private GazeFilters gazeFilters;

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

		Vector2 screenSpace = gazePoint.Screen;
		//Vector2 filteredscreenSpace = getJLFilterValueSnappy(screenSpace);
		Vector2 filteredscreenSpace = gazeFilters.smooth(screenSpace);
		
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
		//-----------------------------------------------------------------------------
    }
	public void DrawHit()
	{

	}

	void Start()
	{
		gazeFilters = new GazeFilters ();
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
