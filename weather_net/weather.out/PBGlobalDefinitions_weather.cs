[Sybase.PowerBuilder.PBGroupAttribute("pbglobaldefinitions_weather", Sybase.PowerBuilder.PBGroupType.Unknown,"","", null,"weather")]
[Sybase.PowerBuilder.PBTargetAttribute("c:\\project\\weather_net\\weather.pbtx", "")]
[System.Diagnostics.DebuggerStepThrough]
public class PBGlobalDefinitions_weather
{
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kGroupVar, "w_weather", null, "w_weather", "", "c:\\project\\weather_net\\gui.pbl\\gui.pblx",null, null, "w_weather")]
	public c__w_weather w_weather = null;
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kGlobalVar, "weather_proxy_weatherwebservicesoapclient", null, "weather_proxy_WeatherWebServiceSoapClient", "", "c:\\project\\weather_net\\proxy.pbl\\proxy.pblx",null, null, "weather_proxy_weatherwebservicesoapclient")]
	public c__weather_proxy_weatherwebservicesoapclient weather_proxy_weatherwebservicesoapclient = null;

	public static PBGlobalDefinitions_weather _instance = null;

	public static PBGlobalDefinitions_weather GetInstance()
	{
		if (_instance == null)
			_instance = new PBGlobalDefinitions_weather();

		return _instance;
	}
	public static void InitUninitVariables()
	{
		GetInstance().Init();
	}

	public void Init()
	{
			
	}
} 