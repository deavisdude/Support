Batch Builder, v1.00
an editor extension tool for Unity
Written by David Ducrest, Stephen Borden for IS3D
http://is3d-online.com/unitytools/

Last Modified: June 19th, 2012


What is this thing good for?
----------------------------

The Batch Builder is a simple interface for automating multi-platform builds in Unity 3. 


Manual building across multiple platforms involves a lot of waiting and watching. 
Switching between build platforms can require a lengthy asset reimport, followed by a lengthy build time. 


The Build Batcher won't reduce the reimport or build time, but it does reduce the amount of human time wasted waiting.

1) You select your desired platforms.
2) You select your desired build options.
3) You click "build" and walk away like a badass (i.e. never looking back). 
   If Batch Builder hits an unrecoverable problem, it will create a error log and continue to the next build platform.
 

How do I use this thing?
------------------------

Preconfiguration:
1) Before using Build Batcher, you need to add your scene files to the build via the [Unity Editor: File->Build Settings] menu.

Starting a Build:
1) Open the [Unity: File->Build Batcher] menu.
2) Enter an app name.
3) Select a build destination path.
4) Choose your build types. (Build Batcher has options for all supported Unity platforms.)
5) (Optional) Choose your Development and Debugging build options.
6) Click "Start Build"


How do I use this on the command line?
--------------------------------------

Did you know that Unity support command-line terminal access?
Well, it does and so does the BuildBatcher.

Note: On MacOS, you can run only run this if your Mac account has an active graphical session. 
      You run this via ssh, but you have to be logged on locally too. It will not interfere with other user accounts.
      
      tl;dr
      You must be logged in locally on MacOS to run this, but you can switch user accounts after loginning in.


1) Navigate to your Unity installation dir
  On MacOS: 
    $ cd /Applications/Unity/Unity.app/Contents/MacOS
  On Windows:
    > cd C:\Program Files (x86)\Unity\Editor
    
    
2) Running Batch Builder
 Syntax: Unity -batchmode -quit -projectPath <path to your project with batch builder> -executeMethod 
 BatchBuilder.build --buildDir <build destination dir> --appName <app name> [Platforms] [Build Options]
 
   Required Parameters:
     -batchmode 			- Open a Unity session without a graphical window.
     -quit				- Close Unity session when done.
     -projectPath			- Open the project at the given path.
     -executeMethod			- Execute a static method in a script. e.g. -executeMethod <Script>.<Method>
 
   Platforms:
     --buildMacOS			- If no platforms are selected, MacOS and Win32 are enabled by default.			
     --buildWin32			- If no platforms are selected, MacOS and Win32 are enabled by default.
     --buildWin64
     --buildAndroid
     --buildWeb
     --buildWebStreaming
     --buildNaCl
     --buildFlash
     --buildXBOX360
     --buildWii
     --buildPS3
     --buildiOS
     --buildAll				- Builds across all available platforms

  Build Options:
     --enableDevelopment  
     --enableAllowDebug			- Toggles Debug Script option. Also enables Development mode.
     --enableAutoconnectProfiler	- Toggles Autoconnect to Unity Editor Profiler. Also enables Development mode.
     --enableStrip
     --enableAppendXCode
     --enableSymlinkXCode
     
  Typical Examples:
  (MacOS)     Unity -batchmode -quit -projectPath /Users/UnityBuilder/MyProject -executeMethod 
 BatchBuilder.build --buildDir /Users/UnityBuilder/Desktop/MyGame --appName Gamerific --buildWin32 --buildMacOS

  (Windows)   Unity -batchmode -quit -projectPath c:\Users\UnityBuilder\MyProject -executeMethod 
 BatchBuilder.build --buildDir c:\Users\UnityBuilder\Desktop\MyGame --appName Gamerific --buildWin32 --buildMacOS

What happens to my build?
-------------------------

For each platform, Unity will create a build within the path: <Build Destination Path>/<App Name>_<Platform>

Log File: <Build Destination Path>/<App Name>.log
 Lists successful builds and failed builds.

Error File: <Build Destination Path>/<App Name>.error
 Lists only failed builds.
 Will Only be created if a build fails.   


Helpful Hints
-------------

1) With a copy of your Unity project, run the Build Batcher from a separate "Builder" account on your workstation. 
   Now you can continue to work unimpeded, while Unity completes it building tasks.
2) We created a web utility that allows us to trigger a build on our web server, then drop the build on our FTP site. 
   It allows team members immediate access to build products.



Revision History
----------------
1.00 Initial release.


Questions? Comments?
--------------------
Contact us at unitytools@is3d-online.com