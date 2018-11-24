package md550104a0db939c201e2bdd47d992b25d5;


public class NewNotesActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("NoteAppHomeworkRJ.NewNotesActivity, NoteAppHomeworkRJ", NewNotesActivity.class, __md_methods);
	}


	public NewNotesActivity ()
	{
		super ();
		if (getClass () == NewNotesActivity.class)
			mono.android.TypeManager.Activate ("NoteAppHomeworkRJ.NewNotesActivity, NoteAppHomeworkRJ", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
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
