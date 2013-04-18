using UnityEngine;
using System.Collections;

public class TitleScene : BaseScene
{
	private FSprite title;
	private FButton testButton;
	
	public override void Start ()
	{
		
		title = new FSprite ("titleImage.png");
		
		testButton = new JFButton ("play.png");
		testButton.AddLabel("Large", "Play", Color.white);
		
		testButton.SignalRelease += HandleTestButtonSignalRelease;
		
		AddChild (title);
		AddChild (testButton);		
		
		title.y = Futile.screen.halfHeight - title.height / 2 - 30;
		title.scale = 0;
		
		testButton.scale = 0;
		
		TweenConfig animateInTween = new TweenConfig ()
			.setDelay (.7f)
				.floatProp ("scale", 1.0f)
				.setEaseType (EaseType.BounceOut);
		
		Go.to (testButton, 2.0f, animateInTween);
		
		animateInTween
			.setDelay (.5f)
			.onComplete (HandleTitleAnimationComplete);
		
		Go.to (title, 2.0f, animateInTween);
		
		base.Start ();
	}

	void HandleTestButtonSignalRelease (FButton obj)
	{
		AnimateOut (new GameScene ());
		
	}
	
	public override void AnimateOut (BaseScene nextScene)
	{
		base.AnimateOut (nextScene);
		
		Go.killAllTweensWithTarget (title);
		Go.killAllTweensWithTarget (testButton);
		
		TweenConfig animateOutTween = new TweenConfig ()
			.setDelay (.3f)
			.floatProp ("scale", 0)
			.setEaseType (EaseType.Linear);
		
		Go.to (title, 1.0f, animateOutTween);
		animateOutTween
			.setDelay (.5f)
			.onComplete (HandleAnimateOutComplete);
				
		Go.to (testButton, 1.0f, animateOutTween);
	}
	
	private void HandleTitleAnimationComplete (AbstractTween at)
	{
		object title = ((Tween)at).target;
		Go.to (title, 2.0f, new TweenConfig ()
			.setDelay (1.0f)
			.floatProp ("scale", 1.05f)
			.setEaseType (EaseType.SineIn)
			.setIterations (-1, LoopType.PingPong)
			);
	}
	
}

