using UnityEngine;
using System.Collections;

public class BaseScene : FContainer
{
	BaseScene nextScene = null;
	public bool animatedOut = false;
	
	// Use this for initialization
	public virtual void Start ()
	{
	
	}
	
	public virtual void AnimateIn()
	{
		
	}
	
	public virtual void AnimateOut(BaseScene nextScene)
	{
		this.nextScene = nextScene;
	}
	
	protected void HandleAnimateOutComplete(AbstractTween at)
	{
		animatedOut = true;
		this.RemoveFromContainer();
		TowerKnightMain.getInstance().GoToScene(nextScene);
	}
}

