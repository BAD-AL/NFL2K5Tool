# NFL2K5Tool
Program for modifying NFL2K5 gamesaves
#### Related Links
1. YouTube Tutorial: https://youtu.be/NCqsq_2GqYs 
2. GitHub: https://github.com/BAD-AL/NFL2K5Tool 
3. Forum: https://forums.operationsports.com/forums/espn-nfl-2k5-football/881901-nfl2k5tool.html

#### It's goals are:
1. Make it super easy to extract data from NFL2K5 gamesave files.
2. Make it super easy to modify NFL2K5 gamesave files (by simply applying text).
3. Make it super easy to modify the Text data extracted from gamesave files (Double click text line to bring up GUI editor).
4. Make it super easy to work with the extracted data (as text and .csv; .csv files can be treated as spreadsheets with Excel)

### Project Status:
1. Can extract data from XBOX & PS2 NFL2K5 gamesave files (.zip, .DAT, .max)
2. Can apply data to base (Roster and Franchise) NFL2K5 gamesave files.

### Notes
When working with files that have been modified by Flying Finn's NFL2K5 editor this program can be used to extract the player data, but do not modify the (FlyingFinn edited) gamesave file with this program. Player names can be shared using Flying Finn's tool. NFL2K5Tool assumes no names are shared. To work around this issue, I recommend simply extracting the data from the gamesave and applying to a new gamesave.

### Implemented features:
0. Extract & Modify player data (32 teams + Free agents + Draft class)
1. Auto update depth chart (depth order will be according to team player list order ).
2. Auto update PBP   (text only option, text command, menu option in DebugDialog) 
3. Auto update Photo (text only option, text command, menu option in DebugDialog) 
4. Roster & Franchise Supported
5. Command line interface. 
6. Zip file management. 
7. Scheduler.
8. 'SET' Command support; SET(0x10, 0x2233) --> Sets the location 0x10 to '22' and location 0x11 to '33' in the gamesave file.
9. Gamesaves are signed and loaded.
10. Warns possible error conditions for weight and body type (Like when a RB weighs 300+ lbs, or has XL body type). 
11. User defined Player sort formulas
12. Player validator (flag players that have attributes that don't seem right.); allow user defined formulas?
13. Player edit gui
14. Auto update special teams
15. Auto update depth charts (based on player position in team's player list)
16. Get/Set special teams roles
17. Global Edit Form + applyable formulas
18. Drag/drop gamesave files into text area.
19. Text commands added [LookupAndModify, AutoUpdateDepthChart, AutoUpdatePBP, AutoUpdatePhoto, ApplyFormula]
20. Resizable face form
21. PS2 .max support https://github.com/PMStanley/ARMax (PS2 Save Tools: http://www.ps2savetools.com/)
22. Check skin/face & Dreads menu items
23. SearchTextBox.CopyAll context menu item
24. Reset key menu item.

### Next features:  
* Editing coaches (currently limited support) 
* Editing playbooks. 

	
### Notes
1. Add tests.
2. Consider removing these photos:
	PlayerData\PlayerPhotos\434a.jpg
	PlayerData\PlayerPhotos\444a.jpg
	PlayerData\PlayerPhotos\454a.jpg
	PlayerData\PlayerPhotos\454e.jpg
	PlayerData\PlayerPhotos\464a.jpg
	
3. Files/directories referenced by the program:
	"PlayerData\\CoachBodies\\"
	"PlayerData\\ENFPhotoIndex.txt"
	"PlayerData\\ENFNameIndex.txt"
	"PlayerData\\FaceFormCategories.json"
	"PlayerData\\PlayerPhotos\\"
	"PlayerData\\SortFormulas.txt"
	"PlayerData\\GenericFaces\\"
	"PlayerData\\EquipmentImages\\"

### Schedule Notes
The input parser gathers lines like "WEEK x" and "teamA at teamB" and runs them through the scheduler.
When scheduling the games, you just need to make sure the teams are spelled corrrectly. Junk in front and at the end of line is ignored.
So a line like: "1 Sun September 11 vikings at titans 1:00 PM" will end up working just fine, the parser sees "vikings at titans" and ignores the rest of the line.
This is helpful when copying and pasting schedules from websites.

### Building 
To Build, use Visual studio. A free version is here: https://www.visualstudio.com/en-us/products/visual-studio-express-vs.aspx
Double click on the NFL2K5Tool.sln file to open the project.
Hit the "F5" key (or press the play button) to start a debugging session (Which also places the .exe at /bin/Debug/)


## Uses solutions from Projects:
https://github.com/icsharpcode/SharpZipLib
https://github.com/PMStanley/ARMax

## Stats Link
https://somsubhra.com/github-release-stats/?username=BAD-AL&repository=NFL2K5Tool

Markdown cheatsheet: https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet

