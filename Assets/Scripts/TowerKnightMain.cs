using UnityEngine;
using System.Collections;

public class TowerKnightMain : MonoBehaviour {
	
	FSprite title;
		
	// Use this for initialization
	void Start () {
	
		FutileParams fparams = new FutileParams(true,true, false, false);
		
		fparams.AddResolutionLevel(480.0f,	1.0f,	1.0f,	""); //iPhone
		
		fparams.origin = new Vector2(0.5f,0.5f);
		
		Futile.instance.Init (fparams);
		
		Futile.atlasManager.LoadAtlas("Atlases/atlasOne");
		
		FSprite background = new FSprite("background.png");
		title = new FSprite("titleImage.png");
		
		Futile.stage.AddChild(background);
		Futile.stage.AddChild(title);
		
		
		title.y = Futile.screen.halfHeight - title.height/2 - 30;
		title.scale = 0;
		
		Go.to (title, 2.0f, new TweenConfig()
			.setDelay(.5f)
			.floatProp("scale", 1.0f)
			.setEaseType(EaseType.BounceOut)
			.onComplete(HandleTitleAnimationComplete));
		
	}
	
	private void HandleTitleAnimationComplete(AbstractTween at)
	{
		Go.to (title, 2.0f,new TweenConfig()
			.setDelay (1.0f)
			.floatProp("scale", 1.05f)
			.setEaseType(EaseType.SineIn)
			.setIterations(-1,LoopType.PingPong)
			);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
