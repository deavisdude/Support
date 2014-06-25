using UnityEngine;
using UnityEditor;
using System.Collections;

public class BatchBuilderGUI : EditorWindow {
	
	static BatchBuilder.CustomParams param = new BatchBuilder.CustomParams();
		
	[MenuItem ("File/Batch Builder %&b", false, 0)]
	static void Init() {
		
		if(EditorPrefs.HasKey("BatchBuilderDir"))
			param._buildDir = EditorPrefs.GetString("BatchBuilderDir", ".");
		
		if(EditorPrefs.HasKey("BatchBuilderAppName"))
			param._appName = EditorPrefs.GetString("BatchBuilderAppName", ".");
		
		// get selected platforms from EditorPrefs
		BuildTarget[] buildTargetKeyArray = new BuildTarget[param.platformBuildEnabled.Keys.Count];
		param.platformBuildEnabled.Keys.CopyTo(buildTargetKeyArray, 0);
		foreach(BuildTarget val in buildTargetKeyArray)
		{
			if(EditorPrefs.HasKey(val.ToString()))
				param.platformBuildEnabled[val] = EditorPrefs.GetBool(val.ToString());
		}
		
		// get build options from EditorPrefs
		BuildOptions[] buildOptionsKeyArray = new BuildOptions[param.buildOptionsEnabled.Keys.Count];
		param.buildOptionsEnabled.Keys.CopyTo(buildOptionsKeyArray, 0);
		foreach(BuildOptions val in buildOptionsKeyArray)
		{
			if(EditorPrefs.HasKey(val.ToString()))
				param.buildOptionsEnabled[val] = EditorPrefs.GetBool(val.ToString());
		}
		
		BatchBuilderGUI window= (BatchBuilderGUI)EditorWindow.GetWindow(typeof(BatchBuilderGUI), true, "Batch Builder", true);
	
		window.minSize = new Vector2(400, 425);
		window.closeThisWindow= false;
		window.build=false;
	}
	
	
	void OnGUI() {
		GUILayout.Label("Batch Builder", EditorStyles.boldLabel);
		
		GUILayout.Label("App Name: ");
		GUILayout.BeginHorizontal();
			param._appName = GUILayout.TextField(param._appName);
		GUILayout.EndHorizontal();
		
		
		GUILayout.Label("Build Path:");
		
		EditorGUILayout.BeginHorizontal();
			param._buildDir = EditorGUILayout.TextField(param._buildDir);
			if(GUILayout.Button("Choose...", GUILayout.ExpandWidth(false)))
			{

				param._buildDir= EditorUtility.OpenFolderPanel("Pick Build Directory", param._buildDir, "");
				EditorPrefs.SetString("BatchBuilderDir", param._buildDir);
			}
		EditorGUILayout.EndHorizontal();
		                      
		scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);	//ScrollView
			GUILayout.BeginHorizontal();
				GUILayout.BeginVertical();
					GUILayout.Label("Build Types",EditorStyles.boldLabel);
					param.platformBuildEnabled[BuildTarget.StandaloneOSXIntel] 	= GUILayout.Toggle(param.platformBuildEnabled[BuildTarget.StandaloneOSXIntel], "Mac OS X");
					param.platformBuildEnabled[BuildTarget.StandaloneWindows] 	= GUILayout.Toggle(param.platformBuildEnabled[BuildTarget.StandaloneWindows], "Windows 32 bit");
					param.platformBuildEnabled[BuildTarget.StandaloneWindows64] = GUILayout.Toggle(param.platformBuildEnabled[BuildTarget.StandaloneWindows64], "Windows 64 bit");
					param.platformBuildEnabled[BuildTarget.StandaloneLinux] 	= GUILayout.Toggle(param.platformBuildEnabled[BuildTarget.StandaloneLinux], "Linux ");
					param.platformBuildEnabled[BuildTarget.StandaloneLinux64] 	= GUILayout.Toggle(param.platformBuildEnabled[BuildTarget.StandaloneLinux64], "Linux x64");
					param.platformBuildEnabled[BuildTarget.WebPlayer] 			= GUILayout.Toggle(param.platformBuildEnabled[BuildTarget.WebPlayer], "Web Player");
					param.platformBuildEnabled[BuildTarget.WebPlayerStreamed] 	= GUILayout.Toggle(param.platformBuildEnabled[BuildTarget.WebPlayerStreamed], "Streaming Web Player");
					param.platformBuildEnabled[BuildTarget.NaCl] 				= GUILayout.Toggle(param.platformBuildEnabled[BuildTarget.NaCl], "Chrome Native Client");
					param.platformBuildEnabled[BuildTarget.FlashPlayer] 		= GUILayout.Toggle(param.platformBuildEnabled[BuildTarget.FlashPlayer], "Flash");
					param.platformBuildEnabled[BuildTarget.iPhone] 				= GUILayout.Toggle(param.platformBuildEnabled[BuildTarget.iPhone], "iOS 				(License Required)");
					param.platformBuildEnabled[BuildTarget.Android] 			= GUILayout.Toggle(param.platformBuildEnabled[BuildTarget.Android], "Android			(License Required)");
					param.platformBuildEnabled[BuildTarget.XBOX360]				= GUILayout.Toggle(param.platformBuildEnabled[BuildTarget.XBOX360], "Xbox 360		(License Required)");
					param.platformBuildEnabled[BuildTarget.PS3] 				= GUILayout.Toggle(param.platformBuildEnabled[BuildTarget.PS3], "PS3 				(License Required)");
//					param.platformBuildEnabled[BuildTarget.Wii] 				= GUILayout.Toggle(param.platformBuildEnabled[BuildTarget.Wii], "Wii 				(License Required)");
				GUILayout.EndVertical();
				GUILayout.BeginVertical();
					GUILayout.Label("Build Options",EditorStyles.boldLabel);
					param.buildOptionsEnabled[BuildOptions.Development] 		= GUILayout.Toggle(param.buildOptionsEnabled[BuildOptions.Development], "Development Build");
					GUI.enabled = param.buildOptionsEnabled[BuildOptions.Development];
						param.buildOptionsEnabled[BuildOptions.ConnectWithProfiler] = GUILayout.Toggle(param.buildOptionsEnabled[BuildOptions.ConnectWithProfiler], " Autoconnect Profiler");
						param.buildOptionsEnabled[BuildOptions.AllowDebugging] 		= GUILayout.Toggle(param.buildOptionsEnabled[BuildOptions.AllowDebugging], "Script Debugging");
					GUI.enabled = true;
/*	
					GUILayout.Label("");
					GUILayout.Label("Flash Only",EditorStyles.boldLabel);
					if(param.platformBuildEnabled[BuildTarget.FlashPlayer]) 
						param.buildOptionsEnabled[BuildOptions.StripPhysics] 		= GUILayout.Toggle(param.buildOptionsEnabled[BuildOptions.StripPhysics], "Strip Physics");
					else {
						GUILayout.Label(" - Strip Physics");
					}
*/		
					GUI.enabled = param.platformBuildEnabled[BuildTarget.iPhone];
					GUILayout.Label("");
					GUILayout.Label("iOS Options",EditorStyles.boldLabel);
					param.buildOptionsEnabled[BuildOptions.SymlinkLibraries] 		= GUILayout.Toggle(param.buildOptionsEnabled[BuildOptions.SymlinkLibraries], "Symlink Unity Libraries");		
					param.buildOptionsEnabled[BuildOptions.AcceptExternalModificationsToPlayer] 		= GUILayout.Toggle(param.buildOptionsEnabled[BuildOptions.AcceptExternalModificationsToPlayer], "Append Xcode Project");		
					GUI.enabled = true;
		
				GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		EditorGUILayout.EndScrollView();		//End ScrollView
		
		
		if(GUILayout.Button("Start Build", GUILayout.Height(30)))
		{
			closeThisWindow= true;
			build= true;
			Repaint();
		}
	}
	
	void Update() {
		
		
		if(closeThisWindow){
			this.Close();	
			closeThisWindow= false;
		}
		if(!closeThisWindow && build) {
			// save platforms selected
			foreach(BuildTarget val in param.platformBuildEnabled.Keys)
				EditorPrefs.SetBool(val.ToString(), param.platformBuildEnabled[val]);
				
			//save build options
			foreach(BuildOptions val in param.buildOptionsEnabled.Keys)
				EditorPrefs.SetBool(val.ToString(), param.buildOptionsEnabled[val]);
		
		
		
			BatchBuilder.buildCustom(param);
			build= false;
		}
	}

	
	private Vector2 scrollPosition = new Vector2(0,0);
	private bool closeThisWindow=false;
	private bool build=false;
}
