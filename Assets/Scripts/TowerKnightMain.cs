using UnityEngine;
using System.Collections;

public class TowerKnightMain : MonoBehaviour
{
	
	BaseScene currentScene = null;
	private static TowerKnightMain instance;
	
	public static TowerKnightMain getInstance ()
	{
		return instance;
	}
	
	// Use this for initialization
	void Start ()
	{
		instance = this;
	
		FutileParams fparams = new FutileParams (true, true, false, false);
		
		fparams.AddResolutionLevel (480.0f, 1.0f, 1.0f, ""); //iPhone
		
		fparams.origin = new Vector2 (0.5f, 0.5f);
		
		Futile.instance.Init (fparams);
		
		Futile.atlasManager.LoadAtlas ("Atlases/atlasOne");
		
		GoToScene (new TitleScene ());
		
	}
	
	public void GoToScene (BaseScene newScene)
	{
		if (currentScene != null) {
			if (currentScene.animatedOut) {
				currentScene = newScene;
				Futile.stage.AddChild (currentScene);
				currentScene.Start ();
			} else
				currentScene.AnimateOut (newScene);
		} else {
			currentScene = newScene;
			Futile.stage.AddChild (currentScene);
			currentScene.Start ();
		}
		
	}
	
	
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
