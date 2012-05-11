package inav;


public class TitlesFragment_MyDragShadowBuilder
	extends android.view.View.DragShadowBuilder
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onDrawShadow:(Landroid/graphics/Canvas;)V:GetOnDrawShadow_Landroid_graphics_Canvas_Handler\n" +
			"";
		mono.android.Runtime.register ("iNAV.TitlesFragment/MyDragShadowBuilder, iNAV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TitlesFragment_MyDragShadowBuilder.class, __md_methods);
	}


	public TitlesFragment_MyDragShadowBuilder (android.view.View p0)
	{
		super (p0);
		if (getClass () == TitlesFragment_MyDragShadowBuilder.class)
			mono.android.TypeManager.Activate ("iNAV.TitlesFragment/MyDragShadowBuilder, iNAV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=c4c4237547e4b6cd", this, new java.lang.Object[] { p0 });
	}


	public TitlesFragment_MyDragShadowBuilder ()
	{
		super ();
		if (getClass () == TitlesFragment_MyDragShadowBuilder.class)
			mono.android.TypeManager.Activate ("iNAV.TitlesFragment/MyDragShadowBuilder, iNAV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onDrawShadow (android.graphics.Canvas p0)
	{
		n_onDrawShadow (p0);
	}

	private native void n_onDrawShadow (android.graphics.Canvas p0);

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
