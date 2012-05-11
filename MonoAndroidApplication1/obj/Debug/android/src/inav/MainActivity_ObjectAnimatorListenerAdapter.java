package inav;


public class MainActivity_ObjectAnimatorListenerAdapter
	extends android.animation.AnimatorListenerAdapter
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onAnimationEnd:(Landroid/animation/Animator;)V:GetOnAnimationEnd_Landroid_animation_Animator_Handler\n" +
			"";
		mono.android.Runtime.register ("iNAV.MainActivity/ObjectAnimatorListenerAdapter, iNAV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainActivity_ObjectAnimatorListenerAdapter.class, __md_methods);
	}


	public MainActivity_ObjectAnimatorListenerAdapter ()
	{
		super ();
		if (getClass () == MainActivity_ObjectAnimatorListenerAdapter.class)
			mono.android.TypeManager.Activate ("iNAV.MainActivity/ObjectAnimatorListenerAdapter, iNAV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public MainActivity_ObjectAnimatorListenerAdapter (inav.MainActivity p0)
	{
		super ();
		if (getClass () == MainActivity_ObjectAnimatorListenerAdapter.class)
			mono.android.TypeManager.Activate ("iNAV.MainActivity/ObjectAnimatorListenerAdapter, iNAV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "iNAV.MainActivity, iNAV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


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
