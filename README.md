# NFL2K5Tool
Program for modifying NFL2K5 gamesaves
To Build, use Visual studio. A free version is here: https://www.visualstudio.com/en-us/products/visual-studio-express-vs.aspx
Double click on the NFL2K5Tool.sln file to open the project.
Hit the "F5" key (or press the play button) to start a debugging session (Which also places the .exe at /bin/Debug/)

It's purpose is to make it super easy to apply data to NFL2K5 gamesave files. As it will allow you to store 
the data in text format.
Simply copy and paste text into the program.

###Project Status:
Can read and write player data successfully! 
Can read all 2317 players from save file and write them back with no file differences!
Can Schedule games; Specifying a year will auto update the dates in the Game (to 'close' dates, not the exact dates the games are actually played)
Can load gamesave files onto your XBOX.
Need to work on solution for setting mass data when some names are mapped to multiple players.
Currently works well in the following use cases:
1. Extracting text data from gamesaves.
2. Applying data to Base gamesave files.

Currently Buggy at:
Automatic photo/PBP update. Need to update the name and photo files.

###Implemented features:
1. Auto update depth chart. 
2. Auto update PBP (should possibly just update the text instead of the gamesave file directly) 
3. Auto update Photo (This could be improved by using player position information, should possibly just update the text instead of the gamesave file directly) 
4. works on Roster & Franchise 
5. Command line interface. 
6. Zip file management (Currently not signing). 
7. Scheduler.
8. 'SET' Command support; SET(0x10, 0x2233) --> Sets the location 0x10 to '22' and location 0x11 to '33' in the gamesave file.
9. Gamesaves are signed and loaded.
10. Warns possible error conditions for weight and body type (Like when a RB weighs 300+ lbs, or has XL body type). 
11. User defined Player sort formulas
12. Player validator (flag players that have attributes that don't seem right.); allow user defined formulas?

###Next features:  
	1. Player edit GUI -- in progress
	2. Editing coaches. 
	3. Editing playbooks. 
	4. Add support for more Photos and names. 
	5. Smart input parser (Where it tries to replace players at the same position instead of overwriting some random player), name management
	6. Auto update Player equipment (from stock file database)
	7. Help Manual
	8. Updating the Kick and punt returners.
	
###Also need
	1. To add tests.
	2. Test Photo and PBP (still need 2k2 photo file & 2k4 photo files)
