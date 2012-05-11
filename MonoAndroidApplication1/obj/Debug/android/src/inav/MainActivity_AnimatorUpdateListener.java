package inav;


public class MainActivity_AnimatorUpdateListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.animation.ValueAnimator.AnimatorUpdateListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onAnimationUpdate:(Landroid/animation/ValueAnimator;)V:GetOnAnimationUpdate_Landroid_animation_ValueAnimator_Handler:Android.Animation.ValueAnimator/IAnimatorUpdateListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("iNAV.MainActivity/AnimatorUpdateListener, iNAV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainActivity_AnimatorUpdateListener.class, __md_methods);
	}


	public MainActivity_AnimatorUpdateListener ()
	{
		super ();
		if (getClass () == MainActivity_AnimatorUpdateListener.class)
			mono.android.TypeManager.Activate ("iNAV.MainActivity/AnimatorUpdateListener, iNAV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public MainActivity_AnimatorUpdateListener (boolean p0, android.view.View p1, android.view.ViewGroup.LayoutParams p2)
	{
		super ();
		if (getClass () == MainActivity_AnimatorUpdateListener.class)
			mono.android.TypeManager.Activate ("iNAV.MainActivity/AnimatorUpdateListener, iNAV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.Boolean, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=c4c4237547e4b6cd:Android.Views.ViewGroup/LayoutParams, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=c4c4237547e4b6cd", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public void onAnimationUpdate (android.animation.ValueAnimator p0)
	{
		n_onAnimationUpdate (p0);
	}

	private native void n_onAnimationUpdate (android.animation.ValueAnimator p0);

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
