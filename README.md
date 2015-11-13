# NFL2K5Tool
Program for modifying NFL2K5 gamesaves
To Build, use Visual studio. A free version is here: https://www.visualstudio.com/en-us/products/visual-studio-express-vs.aspx
Double click on the NFL2K5Tool.sln file to open the project.
Hit the "F5" key (or press the play button) to start a debugging session (Which also places the .exe at /bin/Debug/)

It's purpose is to make it super easy to apply data to NFL2K5 gamesave files. As it will allow you to store 
the data in text format.
Simply copy and paste text into the program.

##Project Status:
1 Can read and write player data successfully! (Everything but DOB). 1
2 Can read all 2317 players from save file and write them back with no file differences! 2

##Implemented features:
1 Auto update depth chart. 1
2 Auto update PBP (should possibly just update the text instead of the gamesave file directly) 2
3 Auto update Photo (This could be improved by using player position information, should possibly just update the text instead of the gamesave file directly) 3
4 works on Roster & Franchise 

##Next features:
	1 Get DOB working 1
	2 Game Schedule 2
	3 Command line interface. 3
	4 Player edit GUI 4
	5 Zip file management. 5
	6 Editing coaches. 6
	7 Editing playbooks. 7
	8 Add support for more Photos and names. 8
	9 Smart input parser (Where it tries to replace players at the same position instead of overwriting some random player) 9
	10 Warn probable error conditions (Like when a RB weighs 300+ lbs, or has XL body type). 10
	
