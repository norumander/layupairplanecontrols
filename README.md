# layup-airplane-control
 Airplane Control solution.

 How to run:

 To run the application on windows:
 - Open My-Project/WindowsBuild/My\ Project.exe
 
 To run on MacOS:
 - Open MacBuild/MacBuild.app/Contents/MacOS/My\ Project

Application logic:

Developing the app on an engine like unity removed a lot of the trigonometry I would have expected to utilize to solve this problem. The trail is simply generated by the ariplanes path, and because no destination was specified, the planes projected trajectory will always be a straight line. In order to make that more dynamic, the forward trajectory is updated on the fly to reflect the speed of the plane such that the blurred image of the plane represents the expected position of the plane regarless of air speed if the yaw stays constant. 

I'm happy to submit an updated version if the intent was solely updating the trajectory based on the yaw and airspeed rather than having the airplane itself move. 

In order to avoid a gimmicky solution like bumping off the edges of the canvas, or wrapping arround, I took advantage of the readly available camera to follow the plane such that the canvas becomes functionally infinite.
