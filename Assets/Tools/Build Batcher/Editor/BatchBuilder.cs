using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
/*!
 * BatchBuilder Must live in the <project>/Assets/Tools/Editor dir
 * The script provides the functionality to build different versions of the application.
 */
public class BatchBuilder {
	
	private static CustomParams customParams;
	private static string[] levels;
	
	public static Dictionary<BuildTarget, string> platformBuildSuffix = new Dictionary<BuildTarget, string>() 

	{
		{ BuildTarget.StandaloneOSXIntel, ".app" },
		{ BuildTarget.StandaloneWindows, ".exe" },
		{ BuildTarget.StandaloneWindows64, ".exe" },
		{ BuildTarget.StandaloneLinux, "" },
		{ BuildTarget.StandaloneLinux64, "" },
		{ BuildTarget.WebPlayer, "" },
		{ BuildTarget.WebPlayerStreamed, "" },
		{ BuildTarget.NaCl, ""},
		{ BuildTarget.FlashPlayer, ".swf"},
		{ BuildTarget.iPhone, " Xcode Project" },
		{ BuildTarget.Android, ".apk" },
//		{ BuildTarget.Wii, "" },
		{ BuildTarget.XBOX360, "" },
		{ BuildTarget.PS3, "" }
	};
	
	public static Dictionary<BuildTarget, string> platformBuildDir = new Dictionary<BuildTarget, string>() 
	{
		{ BuildTarget.StandaloneOSXIntel, "MacOS/" },
		{ BuildTarget.StandaloneWindows, "Win32/" },
		{ BuildTarget.StandaloneWindows64, "Win64/" },
		{ BuildTarget.StandaloneLinux, "Linux/" },
		{ BuildTarget.StandaloneLinux64, "Linux64/" },	
		{ BuildTarget.WebPlayer, "Web/" },
		{ BuildTarget.WebPlayerStreamed, "WebStreaming/" },
		{ BuildTarget.NaCl, "NaCl/"},
		{ BuildTarget.FlashPlayer, "Flash/"},
		{ BuildTarget.iPhone, "iOS/" },
		{ BuildTarget.Android, "Android/" },
//		{ BuildTarget.Wii, "Wii/" },
		{ BuildTarget.XBOX360, "Xbox/" },
		{ BuildTarget.PS3, "PS3/" }
	};
	
	
	public class CustomParams
	{
		//Parameters
		public string _buildDir= "";
		public string _appName = PlayerSettings.productName;
		public List<string> _levels = new List<string>();
		
		public Dictionary<BuildTarget, bool> platformBuildEnabled = new Dictionary<BuildTarget, bool>() 
		{
			{ BuildTarget.StandaloneOSXIntel, false},
			{ BuildTarget.StandaloneWindows, false },
			{ BuildTarget.StandaloneWindows64, false },
			{ BuildTarget.StandaloneLinux, false },
			{ BuildTarget.StandaloneLinux64, false },		
			{ BuildTarget.WebPlayer, false },
			{ BuildTarget.WebPlayerStreamed, false },
			{ BuildTarget.NaCl, false},
			{ BuildTarget.FlashPlayer, false},
			{ BuildTarget.iPhone, false },
			{ BuildTarget.Android, false },
//			{ BuildTarget.Wii, false },
			{ BuildTarget.XBOX360, false },
			{ BuildTarget.PS3, false }
		};
		
		public Dictionary<BuildOptions, bool> buildOptionsEnabled= new Dictionary<BuildOptions, bool>() 
		{				
			{ BuildOptions.Development, false },
				{ BuildOptions.ConnectWithProfiler, false },
				{ BuildOptions.AllowDebugging, false },				
//			{ BuildOptions.StripPhysics, false },			//Flash only
			{ BuildOptions.SymlinkLibraries, false },			//iOS only
			{ BuildOptions.AcceptExternalModificationsToPlayer, false } //iOS only (Append to Current iOS build directory
		};
		
		public CustomParams()
		{
			
			for( int x =0; x<EditorBuildSettings.scenes.Length; x++)
			{
				if(EditorBuildSettings.scenes[x].enabled){
					_levels.Add(EditorBuildSettings.scenes[x].path);
				}
			}
		}
		
		public static CustomParams getParams()
		{
			CustomParams param = new CustomParams();
						
			//Parse Parameters
			String[] arguments = Environment.GetCommandLineArgs();
			for(int x=0; x<arguments.Length; x++)
			{
			 	String command = arguments[x];
				
				if(command=="--buildDir")
				{
					if( x+1 < arguments.Length )				
						param._buildDir = arguments[x+1];
				}
				else if(command=="--appName")
				{
					if( x+1 < arguments.Length )				
						param._appName = arguments[x+1];
				}
				else if(command=="--enableDevelopment")
					param.buildOptionsEnabled[BuildOptions.Development] = true;
				else if(command=="--enableAutoconnectProfiler") {
					param.buildOptionsEnabled[BuildOptions.Development] = true;
					param.buildOptionsEnabled[BuildOptions.ConnectWithProfiler] = true;
				}
				else if(command=="--enableAllowDebug"){
					param.buildOptionsEnabled[BuildOptions.Development] = true;
					param.buildOptionsEnabled[BuildOptions.AllowDebugging] = true;
				}
				else if(command=="--enableSymlinkXCode")
					param.buildOptionsEnabled[BuildOptions.SymlinkLibraries] = true;
				else if(command=="--enableAppendXCode")
					param.buildOptionsEnabled[BuildOptions.AcceptExternalModificationsToPlayer] = true;
				
				else if(command=="--buildMacOS")
					param.platformBuildEnabled[BuildTarget.StandaloneOSXIntel] = true;
				else if(command=="--buildWin32")
					param.platformBuildEnabled[BuildTarget.StandaloneWindows] = true;
				else if(command=="--buildWin64")
					param.platformBuildEnabled[BuildTarget.StandaloneWindows64] =  true;
				else if(command=="--buildLinux")
					param.platformBuildEnabled[BuildTarget.StandaloneLinux] =  true;
				else if(command=="--buildLinux64")
					param.platformBuildEnabled[BuildTarget.StandaloneLinux64] =  true;					
				else if(command=="--buildWeb")
					param.platformBuildEnabled[BuildTarget.WebPlayer] = true;
				else if(command=="--buildWebStreaming")
					param.platformBuildEnabled[BuildTarget.WebPlayerStreamed] = true;
				else if(command=="--buildNaCl")
					param.platformBuildEnabled[BuildTarget.NaCl] = true;
				else if(command=="--buildFlash")
					param.platformBuildEnabled[BuildTarget.FlashPlayer] = true;
				else if(command=="--buildAndroid")
					param.platformBuildEnabled[BuildTarget.Android] = true;
				else if(command=="--buildXBOX360")
					param.platformBuildEnabled[BuildTarget.XBOX360] = true;
//				else if(command=="--buildWii")
//					param.platformBuildEnabled[BuildTarget.Wii] = true;
				else if(command=="--buildPS3")
					param.platformBuildEnabled[BuildTarget.PS3] = true;				
				else if(command=="--buildiOS")
					param.platformBuildEnabled[BuildTarget.iPhone] = true;
				else if(command=="--buildAll")
				{
					param.platformBuildEnabled[BuildTarget.StandaloneOSXIntel] = true;
					param.platformBuildEnabled[BuildTarget.StandaloneWindows] = true;
					param.platformBuildEnabled[BuildTarget.StandaloneWindows64] =  true;
					param.platformBuildEnabled[BuildTarget.WebPlayer] = true;
					param.platformBuildEnabled[BuildTarget.WebPlayerStreamed] = true;
					param.platformBuildEnabled[BuildTarget.NaCl] = true;
					param.platformBuildEnabled[BuildTarget.FlashPlayer] = true;
					param.platformBuildEnabled[BuildTarget.Android] = true;
					param.platformBuildEnabled[BuildTarget.PS3] = true;				
					param.platformBuildEnabled[BuildTarget.XBOX360] = true;				
//					param.platformBuildEnabled[BuildTarget.Wii] = true;				
					param.platformBuildEnabled[BuildTarget.iPhone] = true;

				}	
			}
			
			
			//Default Build MacOS
			bool useDefault = true;
			foreach( KeyValuePair<BuildTarget, bool> pair in param.platformBuildEnabled ) {
				if(pair.Value){					
					useDefault = false;
					break;
				}
			}
			
			if(useDefault) {
				param.platformBuildEnabled[BuildTarget.StandaloneOSXIntel] = true;
				param.platformBuildEnabled[BuildTarget.StandaloneWindows] = true;
			}
				
			
			return param;
		}
		
	}
	
	/*! Build Anything 
	 * Allows for additional command line parameters
	 * 	--buildDir <path> 		specifies the directory, relative to the Unity application home dir. RECOMMEND using complete directory path. [Default is .]
	 *  --buildMacOS	DEFAULT
	 *  --buildWin32
	 *  --buildWin64
	 *  --buildLinux
	 *  --buildWeb
	 *  --buildWebStreaming
	 *  --buildAndroid
	 *  --buildiOS
	 *  --buildAll	
	 * */
	public static void build()
	{
		buildCustom(CustomParams.getParams());	
	}
	
	/*! Build Anything 
	 * Allows for additional command line parameters
	 * 	--buildDir <path> 		specifies the directory, relative to the Unity application home dir. RECOMMEND using complete directory path. [Default is .]
	 *  --buildMacOS	DEFAULT
	 *  --buildWin32
	 *  --buildWin64
	 *  --buildLinux
	 *  --buildWeb
	 *  --buildWebStreaming
	 *  --buildAndroid
	 *  --buildiOS
	 *  --buildAll	
	 * */
	public static void buildCustom(CustomParams param)
	{	
		customParams = param;
		
		levels = new string[param._levels.Count];
		for( int x=0; x < param._levels.Count; x++) {
			levels[x] = param._levels[x]; }
		
		
		if(param.platformBuildEnabled[BuildTarget.StandaloneOSXIntel]) {
			BuildOptions opts= BuildOptions.None;
				if(param.buildOptionsEnabled[BuildOptions.Development])
					opts= opts | BuildOptions.Development;
				if(param.buildOptionsEnabled[BuildOptions.AllowDebugging])
					opts= opts | BuildOptions.AllowDebugging;
				if(param.buildOptionsEnabled[BuildOptions.ConnectWithProfiler])
					opts= opts | BuildOptions.ConnectWithProfiler;
				
			buildPlatform(customParams._buildDir, customParams._appName, levels, BuildTarget.StandaloneOSXIntel, opts);
		}
		if(param.platformBuildEnabled[BuildTarget.StandaloneWindows]) {
			BuildOptions opts= BuildOptions.None;
				if(param.buildOptionsEnabled[BuildOptions.Development])
					opts= opts | BuildOptions.Development;
				if(param.buildOptionsEnabled[BuildOptions.AllowDebugging])
					opts= opts | BuildOptions.AllowDebugging;
				if(param.buildOptionsEnabled[BuildOptions.ConnectWithProfiler])
					opts= opts | BuildOptions.ConnectWithProfiler;
				
			buildPlatform(customParams._buildDir, customParams._appName, levels, BuildTarget.StandaloneWindows, opts);
		}
		if(param.platformBuildEnabled[BuildTarget.StandaloneWindows64]) {
			BuildOptions opts= BuildOptions.None;
				if(param.buildOptionsEnabled[BuildOptions.Development])
					opts= opts | BuildOptions.Development;
				if(param.buildOptionsEnabled[BuildOptions.AllowDebugging])
					opts= opts | BuildOptions.AllowDebugging;
				if(param.buildOptionsEnabled[BuildOptions.ConnectWithProfiler])
					opts= opts | BuildOptions.ConnectWithProfiler;
				
			buildPlatform(customParams._buildDir, customParams._appName, levels, BuildTarget.StandaloneWindows64, opts);
		}
		if(param.platformBuildEnabled[BuildTarget.StandaloneLinux]) {
			BuildOptions opts= BuildOptions.None;
				if(param.buildOptionsEnabled[BuildOptions.Development])
					opts= opts | BuildOptions.Development;
				if(param.buildOptionsEnabled[BuildOptions.AllowDebugging])
					opts= opts | BuildOptions.AllowDebugging;
				if(param.buildOptionsEnabled[BuildOptions.ConnectWithProfiler])
					opts= opts | BuildOptions.ConnectWithProfiler;
				
			buildPlatform(customParams._buildDir, customParams._appName, levels, BuildTarget.StandaloneLinux, opts);
		}
		if(param.platformBuildEnabled[BuildTarget.StandaloneLinux64]) {
			BuildOptions opts= BuildOptions.None;
				if(param.buildOptionsEnabled[BuildOptions.Development])
					opts= opts | BuildOptions.Development;
				if(param.buildOptionsEnabled[BuildOptions.AllowDebugging])
					opts= opts | BuildOptions.AllowDebugging;
				if(param.buildOptionsEnabled[BuildOptions.ConnectWithProfiler])
					opts= opts | BuildOptions.ConnectWithProfiler;
				
			buildPlatform(customParams._buildDir, customParams._appName, levels, BuildTarget.StandaloneLinux64, opts);
		}
		if(param.platformBuildEnabled[BuildTarget.WebPlayer])
			{
			BuildOptions opts= BuildOptions.None;
				if(param.buildOptionsEnabled[BuildOptions.Development])
					opts= opts | BuildOptions.Development;
				if(param.buildOptionsEnabled[BuildOptions.AllowDebugging])
					opts= opts | BuildOptions.AllowDebugging;
				if(param.buildOptionsEnabled[BuildOptions.ConnectWithProfiler])
					opts= opts | BuildOptions.ConnectWithProfiler;
				
			buildPlatform(customParams._buildDir, customParams._appName, levels, BuildTarget.WebPlayer, opts);
		}
		if(param.platformBuildEnabled[BuildTarget.NaCl])
		{
			BuildOptions opts= BuildOptions.None;
				if(param.buildOptionsEnabled[BuildOptions.Development])
					opts= opts | BuildOptions.Development;
				if(param.buildOptionsEnabled[BuildOptions.AllowDebugging])
					opts= opts | BuildOptions.AllowDebugging;
				if(param.buildOptionsEnabled[BuildOptions.ConnectWithProfiler])
					opts= opts | BuildOptions.ConnectWithProfiler;
				
			buildPlatform(customParams._buildDir, customParams._appName, levels, BuildTarget.NaCl, opts);
		}
		if(param.platformBuildEnabled[BuildTarget.WebPlayerStreamed])
		{
			BuildOptions opts= BuildOptions.None;
				if(param.buildOptionsEnabled[BuildOptions.Development])
					opts= opts | BuildOptions.Development;
				if(param.buildOptionsEnabled[BuildOptions.AllowDebugging])
					opts= opts | BuildOptions.AllowDebugging;
				if(param.buildOptionsEnabled[BuildOptions.ConnectWithProfiler])
					opts= opts | BuildOptions.ConnectWithProfiler;
				
			buildPlatform(customParams._buildDir, customParams._appName, levels, BuildTarget.WebPlayerStreamed, opts);
		}
		if(param.platformBuildEnabled[BuildTarget.FlashPlayer])
		{
			BuildOptions opts= BuildOptions.None;
				if(param.buildOptionsEnabled[BuildOptions.Development])
					opts= opts | BuildOptions.Development;
				//if(param.buildOptionsEnabled[BuildOptions.StripPhysics])
				//	opts= opts | BuildOptions.StripPhysics;
			buildPlatform(customParams._buildDir, customParams._appName, levels, BuildTarget.FlashPlayer, opts);
		}			
		if(param.platformBuildEnabled[BuildTarget.Android])
		{
			BuildOptions opts= BuildOptions.None;
				if(param.buildOptionsEnabled[BuildOptions.Development])
					opts= opts | BuildOptions.Development;
				if(param.buildOptionsEnabled[BuildOptions.AllowDebugging])
					opts= opts | BuildOptions.AllowDebugging;
				if(param.buildOptionsEnabled[BuildOptions.ConnectWithProfiler])
					opts= opts | BuildOptions.ConnectWithProfiler;
				
			buildPlatform(customParams._buildDir, customParams._appName, levels, BuildTarget.Android, opts);
		}			
		if(param.platformBuildEnabled[BuildTarget.iPhone])
		{
			BuildOptions opts= BuildOptions.None;
				if(param.buildOptionsEnabled[BuildOptions.Development])
					opts= opts | BuildOptions.Development;
				if(param.buildOptionsEnabled[BuildOptions.AllowDebugging])
					opts= opts | BuildOptions.AllowDebugging;
				if(param.buildOptionsEnabled[BuildOptions.ConnectWithProfiler])
					opts= opts | BuildOptions.ConnectWithProfiler;
				if(param.buildOptionsEnabled[BuildOptions.SymlinkLibraries])
					opts= opts | BuildOptions.SymlinkLibraries;
				if(param.buildOptionsEnabled[BuildOptions.AcceptExternalModificationsToPlayer])
					opts= opts | BuildOptions.AcceptExternalModificationsToPlayer;
				
			buildPlatform(customParams._buildDir, customParams._appName, levels, BuildTarget.iPhone, opts);
		}			
		if(param.platformBuildEnabled[BuildTarget.XBOX360])
		{
			BuildOptions opts= BuildOptions.None;
				if(param.buildOptionsEnabled[BuildOptions.Development])
					opts= opts | BuildOptions.Development;
				if(param.buildOptionsEnabled[BuildOptions.AllowDebugging])
					opts= opts | BuildOptions.AllowDebugging;
				if(param.buildOptionsEnabled[BuildOptions.ConnectWithProfiler])
					opts= opts | BuildOptions.ConnectWithProfiler;
				
			buildPlatform(customParams._buildDir, customParams._appName, levels, BuildTarget.XBOX360, opts);
		}			
		if(param.platformBuildEnabled[BuildTarget.PS3])
		{
			BuildOptions opts= BuildOptions.None;
				if(param.buildOptionsEnabled[BuildOptions.Development])
					opts= opts | BuildOptions.Development;
				if(param.buildOptionsEnabled[BuildOptions.AllowDebugging])
					opts= opts | BuildOptions.AllowDebugging;
				if(param.buildOptionsEnabled[BuildOptions.ConnectWithProfiler])
					opts= opts | BuildOptions.ConnectWithProfiler;
				
			buildPlatform(customParams._buildDir, customParams._appName, levels, BuildTarget.PS3, opts);
		}			
//		if(param.platformBuildEnabled[BuildTarget.Wii])
//		{
//			BuildOptions opts= BuildOptions.None;
//				if(param.buildOptionsEnabled[BuildOptions.Development])
//					opts= opts | BuildOptions.Development;
//				if(param.buildOptionsEnabled[BuildOptions.AllowDebugging])
//					opts= opts | BuildOptions.AllowDebugging;
//				if(param.buildOptionsEnabled[BuildOptions.ConnectWithProfiler])
//					opts= opts | BuildOptions.ConnectWithProfiler;
//				
//			buildPlatform(customParams._buildDir, customParams._appName, levels, BuildTarget.Wii, opts);
//		}			
	}
	
	public static void buildPlatform(string buildDir, string appName, string[] levels, BuildTarget platform, BuildOptions opts= BuildOptions.None)
	{
		Debug.Log(appName + " Building Platform ["+ platform.ToString() +"]");
		
		//string targetDir= Path.Combine(buildDir, appName);
		
		//if(!Directory.Exists(targetDir))
		//	Directory.CreateDirectory(targetDir);
		
		string platformPath = Path.Combine(buildDir, appName + " " + platformBuildDir[platform]);
		
		if(!Directory.Exists(platformPath))
			Directory.CreateDirectory(platformPath);
		
		string targetPath= Path.Combine(platformPath, appName + platformBuildSuffix[platform]);		
		string error = BuildPipeline.BuildPlayer(levels, targetPath, platform, opts);		//Start build and collect errors
		
		
		//Set the output string describing success or failure
		string outputString= "Success Building ["+appName+"] for Platform ["+platform.ToString()+"] \n";
		if (error != string.Empty)
			outputString = "Error Building ["+appName+"] for Platform ["+platform.ToString()+"] \n\tReport:["+error+"]\n";
		
		//Create a general output file
		string log= Path.Combine( buildDir, appName+".log");
		if(!File.Exists(log)) {
			using ( StreamWriter writer= File.CreateText(log) ) {
					writer.WriteLine(outputString);
			}
		}
		else {
			using ( StreamWriter writer= new StreamWriter(log,true))  {
					writer.WriteLine(outputString);
			}
		}
		
		//Create an error specific file
		if (error != string.Empty) {
			string errorlog= Path.Combine( buildDir, appName+".error");
			if(!File.Exists(errorlog)) {
				using ( StreamWriter writer= File.CreateText(errorlog) ) {
					writer.WriteLine(outputString);
				}
			}
			else {
				using ( StreamWriter writer= new StreamWriter(errorlog,true))  {
					writer.WriteLine(outputString);
				}
			}
		}
	}	

}
