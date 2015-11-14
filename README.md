# NFL2K5Tool
Program for modifying NFL2K5 gamesaves
To Build, use Visual studio. A free version is here: https://www.visualstudio.com/en-us/products/visual-studio-express-vs.aspx
Double click on the NFL2K5Tool.sln file to open the project.
Hit the "F5" key (or press the play button) to start a debugging session (Which also places the .exe at /bin/Debug/)

It's purpose is to make it super easy to apply data to NFL2K5 gamesave files. As it will allow you to store 
the data in text format.
Simply copy and paste text into the program.

###Project Status:
Can read and write player data successfully! (Everything but DOB).
Can read all 2317 players from save file and write them back with no file differences!

###Implemented features:
1. Auto update depth chart. 
2. Auto update PBP (should possibly just update the text instead of the gamesave file directly) 
3. Auto update Photo (This could be improved by using player position information, should possibly just update the text instead of the gamesave file directly) 
4. works on Roster & Franchise 
5. Command line interface. 
6. Zip file management. 

###Next features:
	1. Get DOB working 
	2. Game Schedule 
	3. Player edit GUI 	
	4. Editing coaches. 
	5. Editing playbooks. 
	6. Add support for more Photos and names. 
	7. Smart input parser (Where it tries to replace players at the same position instead of overwriting some random player), name management
	8. Warn probable error conditions (Like when a RB weighs 300+ lbs, or has XL body type). 
	9. Auto update Player equipment (from stock file database)
	
###Also need
	1. To add tests.
	2. Test Photo and PBP
