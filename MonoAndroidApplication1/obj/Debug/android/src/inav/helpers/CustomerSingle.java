package inav.helpers;


public class CustomerSingle
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_toString:()Ljava/lang/String;:GetToStringHandler\n" +
			"";
		mono.android.Runtime.register ("iNAV.Helpers.CustomerSingle, iNAV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CustomerSingle.class, __md_methods);
	}


	public CustomerSingle ()
	{
		super ();
		if (getClass () == CustomerSingle.class)
			mono.android.TypeManager.Activate ("iNAV.Helpers.CustomerSingle, iNAV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public CustomerSingle (java.lang.String p0, java.lang.String p1, java.lang.String p2, int p3)
	{
		super ();
		if (getClass () == CustomerSingle.class)
			mono.android.TypeManager.Activate ("iNAV.Helpers.CustomerSingle, iNAV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}


	public java.lang.String toString ()
	{
		return n_toString ();
	}

	private native java.lang.String n_toString ();

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
