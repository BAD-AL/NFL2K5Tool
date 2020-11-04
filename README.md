# NFL2K5Tool
Program for modifying NFL2K5 gamesaves
To Build, use Visual studio. A free version is here: https://www.visualstudio.com/en-us/products/visual-studio-express-vs.aspx
Double click on the NFL2K5Tool.sln file to open the project.
Hit the "F5" key (or press the play button) to start a debugging session (Which also places the .exe at /bin/Debug/)

It's purpose is to make it super easy to apply data to NFL2K5 gamesave files. As it will allow you to store 
the data in text format.
Simply copy and paste text into the program.

YouTube Tutorial: https://youtu.be/NCqsq_2GqYs 
GitHub: https://github.com/BAD-AL/NFL2K5Tool 
Forum: https://forums.operationsports.com/forums/espn-nfl-2k5-football/881901-nfl2k5tool.html

### Project Status:
Can read and write player data successfully! 
Can read all 2317 players from save file and write them back with no file differences!
Can Schedule games; Specifying a year will auto update the dates in the Game (to 'close' dates, not the exact dates the games are actually played)
Can load and save XBOX gamesave files.
Can load and save (.max) PS2 gamesave files.

Currently works well in the following use cases:
1. Extracting text data from gamesaves.
2. Applying data to Base gamesave files.

Markdown cheatsheet: https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet

### Implemented features:
1. Auto update depth chart. 
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

### Next features:  
* Salary Cap 
	# Set Salary Cap -> 198.2M
	SET(0x9ACCC, 0x38060300)
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
This is helpful when copying and pasting schedules from websites online.


### Menus
#### File
##### Load Save
Loads a Roster or Franchise file into program memory.
##### Load Text File
Loads a text file into the text area
##### Apply data without saving
Apply the data currently in the text area to the current gamesave loaded in memory.
##### Save
Applies the data currently in the text area, prompts the user to save to a file.
##### Exit
Exits the program.
#### View
#####Find
Enter text to search for
##### Debug Dialog
Launches a special dialog that I use to search through the gamesave file (pretty complicated)
##### List 'x'
Will list the selection when the 'List Contents' button is pressed.
#### Edit
##### Show Schedule now
Appends the schedule to the text box
##### Auto Correct schedule
Split up the listed schedule into the correct number of games per week for NFL2K5 franchise.
##### Show team players now
List the teams out now (overwrites text box text)
##### Validate Players
Checks through the players and shows warnings based on player weight and body type.
##### Sort Players
Sorts the currently listed players based on 'SortFormulas.csv'
##### Edit Sort formulas
Lets the user edit the formulas used to sort players
##### Auto Update Special teams depth
Assigns fast (non-starting) RB,CB or WR to return punts and kicks. Assigns a Center to be long snapper. (works on gamesave data in memory, not the text)
Use this option after you have applied data to a gamesave, before saving to a file.
##### Auto Update Depth chart
Automatically updates the teams' depth charts (in program memory) based on player index and position.
Players listed higher will be at the top of the depth chart, players listed lower will be lower on the depth chart.
##### Auto update Photo
Automatically update the 'Photo' attribute of each player based on name. (uses ''ENFPhotoIndex.txt' file, operates on text )
##### Auto Update PBP
Automatically Update what name gets called for a player (operates on text)


## Uses solutions from Projects:
https://github.com/icsharpcode/SharpZipLib
https://github.com/PMStanley/ARMax

## Stats Link
https://somsubhra.com/github-release-stats/?username=BAD-AL&repository=NFL2K5Tool

