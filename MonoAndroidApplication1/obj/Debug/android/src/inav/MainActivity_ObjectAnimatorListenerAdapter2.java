package inav;


public class MainActivity_ObjectAnimatorListenerAdapter2
	extends android.animation.AnimatorListenerAdapter
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onAnimationCancel:(Landroid/animation/Animator;)V:GetOnAnimationCancel_Landroid_animation_Animator_Handler\n" +
			"n_onAnimationEnd:(Landroid/animation/Animator;)V:GetOnAnimationEnd_Landroid_animation_Animator_Handler\n" +
			"";
		mono.android.Runtime.register ("iNAV.MainActivity/ObjectAnimatorListenerAdapter2, iNAV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainActivity_ObjectAnimatorListenerAdapter2.class, __md_methods);
	}


	public MainActivity_ObjectAnimatorListenerAdapter2 ()
	{
		super ();
		if (getClass () == MainActivity_ObjectAnimatorListenerAdapter2.class)
			mono.android.TypeManager.Activate ("iNAV.MainActivity/ObjectAnimatorListenerAdapter2, iNAV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public MainActivity_ObjectAnimatorListenerAdapter2 (inav.MainActivity p0, android.app.FragmentManager p1, inav.TitlesFragment p2)
	{
		super ();
		if (getClass () == MainActivity_ObjectAnimatorListenerAdapter2.class)
			mono.android.TypeManager.Activate ("iNAV.MainActivity/ObjectAnimatorListenerAdapter2, iNAV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "iNAV.MainActivity, iNAV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null:Android.App.FragmentManager, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=c4c4237547e4b6cd:iNAV.TitlesFragment, iNAV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public void onAnimationCancel (android.animation.Animator p0)
	{
		n_onAnimationCancel (p0);
	}

	private native void n_onAnimationCancel (android.animation.Animator p0);


	public void onAnimationEnd (android.animation.Animator p0)
	{
		n_onAnimationEnd (p0);
	}

	private native void n_onAnimationEnd (android.animation.Animator p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
