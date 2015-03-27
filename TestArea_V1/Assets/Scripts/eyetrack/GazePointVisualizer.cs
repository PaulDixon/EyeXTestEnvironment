//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using Tobii.EyeX.Framework;
using UnityEngine;

/// <summary>
/// Visualizes the gaze point in the game window using a tiny GUI.Box.
/// </summary>
/// 								PointVisualizerBase
///									sceneTrackdata

public class GazePointVisualizer : PointVisualizerBase
{
    // A reference to the EyeX host instance, initialized on Awake. See EyeXHost.GetInstance().
    private EyeXHost _eyeXHost;
    private IEyeXDataProvider<EyeXGazePoint> _gazePointProvider;
	private EyeTrackOnly trackObserver_eye;
	private EyeMouse trackObserver_eyemouse;

	private sceneTrackdata ScenedataCapt;

#if UNITY_EDITOR
    private GazePointDataMode _oldGazePointMode;
#endif

    /// <summary>
    /// Choice of gaze point data stream to be visualized.
    /// </summary>
    public GazePointDataMode gazePointMode = GazePointDataMode.LightlyFiltered;
	
    /// <summary>
    /// The size of the visualizer point
    /// </summary>
    public float pointSize = 5;

    /// <summary>
    /// The color of the visualizer point
    /// </summary>
    public Color pointColor = Color.yellow;

    public void Awake()
    {


        _eyeXHost = EyeXHost.GetInstance();

#if UNITY_EDITOR
        _oldGazePointMode = gazePointMode;
		_gazePointProvider = _eyeXHost.GetGazePointDataProvider(gazePointMode);
#endif
    }
	public void OnLevelWasLoaded()
	{
		GameObject camScript = GameObject.Find ("Main Camera");
		if (camScript) 
		{
			trackObserver_eye = camScript.GetComponent<EyeTrackOnly>();
			trackObserver_eyemouse = camScript.GetComponent<EyeMouse>();
		}
		ScenedataCapt = new sceneTrackdata ();
	}

    public void OnEnable()
    {
        _gazePointProvider.Start();
    }

    public void OnDisable()
    {
        _gazePointProvider.Stop();
    }

    /// <summary>
    /// Draw a GUI.Box at the user's gaze point.
    /// </summary>
    public void OnGUI()
    {
#if UNITY_EDITOR
        if (_oldGazePointMode != gazePointMode)
        {
            _gazePointProvider.Stop();
            _oldGazePointMode = gazePointMode;
            _gazePointProvider = _eyeXHost.GetGazePointDataProvider(gazePointMode);
            _gazePointProvider.Start();
        }
#endif
		var gazePoint = _gazePointProvider.Last;
		//filter nan values.
		if (!System.Single.IsNaN(gazePoint.Screen.x)) {

			//update
			if(trackObserver_eye != null)
			{
				trackObserver_eye.updateEyeTrack(gazePoint);
				ScenedataCapt.storeTrackPoint (gazePoint);
				DrawGUI(gazePoint, pointSize, pointColor);
			}
			if(trackObserver_eyemouse != null)
			{
				trackObserver_eyemouse.updateEyeTrack(gazePoint);
				ScenedataCapt.storeTrackPoint (gazePoint);
				DrawGUI(gazePoint, pointSize, pointColor);
			}
		}
    }
}
