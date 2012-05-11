package inav;


public class TitleFragment
	extends android.app.ListFragment
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("iNAV.TitleFragment, iNAV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TitleFragment.class, __md_methods);
	}

	public TitleFragment ()
	{
		super ();
		if (getClass () == TitleFragment.class)
			mono.android.TypeManager.Activate ("iNAV.TitleFragment, iNAV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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
