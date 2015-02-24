using UnityEngine;
using System.Collections;

public class changeScene :MonoBehaviour 
{
	public void change(string toScene)
	{
		//load base content
		//Application.LoadLevelAdditive("map_base");

		//load specific scene
		Application.LoadLevel(toScene);

		//apply gui
		if (toScene != "mainmenu") //cant have 2 gui systems
		{
			//Application.LoadLevelAdditive("gui");
		}
	}
}
