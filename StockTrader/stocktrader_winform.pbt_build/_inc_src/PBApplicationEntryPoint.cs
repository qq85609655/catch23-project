public class PBApplicationEntryPoint
{

	[System.Diagnostics.DebuggerStepThrough]
	[System.STAThread]
	static void Main()
	{
		c__stocktrader.ApplicationName = "stocktrader";
		Sybase.PowerBuilder.PBSessionBase.HasPBExtensions = false;
		Sybase.PowerBuilder.PBSessionBase.MainAssembly = System.Reflection.Assembly.GetExecutingAssembly();
		Sybase.PowerBuilder.Win.PBSession session = Sybase.PowerBuilder.Win.PBSession.CreateSession(
			typeof(c__stocktrader), 
			@"stocktrader.pbd;gui.pbd;dwo.pbd;dal.pbd;proxy.pbd;soapclasses.pbd");
		c__stocktrader.GetCurrentApplication().stocktrader = c__stocktrader.GetCurrentApplication();
		session.RunWinForm();
	}
}
 