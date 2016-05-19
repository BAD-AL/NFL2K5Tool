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
Scrolls after closing player edit form.

###Implemented features:
1. Auto update depth chart. 
2. Auto update PBP (should possibly just update the text instead of the gamesave file directly) 
3. Auto update Photo (This could be improved by using player position information, should possibly just update the text instead of the gamesave file directly) 
4. works on Roster & Franchise 
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
15. Get/Set special teams

###Next features:  
	1. Editing coaches. 
	2. Editing playbooks. 
	5. Smart input parser (Where it tries to replace players at the same position instead of overwriting some random player), name management
	6. Auto update Player equipment (from stock file database)
	7. Help Manual
	
###Also need
	1. To add tests.

###Schedule Notes
The input parser gathers lines like "WEEK x" and "teamA at teamB" and runs them through the scheduler.
When scheduling the games, you just need to make sure the teams are spelled corrrectly. Junk in front and at the end of line is ignored.
So a line like: "1 Sun September 11 vikings at titans 1:00 PM" will end up working just fine, the parser sees "vikings at titans" and ignores the rest of the line.
This is helpful when copying and pasting schedules from websites online.


###Menus
####File
#####Load Save
Loads a Roster or Franchise file into program memory.
#####Load Text File
Loads a text file into the text area
#####Apply data without saving
Apply the data currently in the text area to the current gamesave loaded in memory.
#####Save
Applies the data currently in the text area, prompts the user to save to a file.
#####Exit
Exits the program.
####View
#####Find
Enter text to search for
#####Debug Dialog
Launches a special dialog that I use to search through the gamesave file (pretty complicated)
#####List 'x'
Will list the selection when the 'List Contents' button is pressed.
####Edit
#####Show Schedule now
Appends the schedule to the text box
#####Auto Correct schedule
Split up the listed schedule into the correct number of games per week for NFL2K5 franchise.
#####Show team players now
List the teams out now (overwrites text box text)
#####Validate Players
Checks through the players and shows warnings based on player weight and body type.
#####Sort Players
Sorts the currently listed players based on 'SortFormulas.csv'
#####Edit Sort formulas
Lets the user edit the formulas used to sort players
#####Auto Update Special teams depth
Assigns fast (non-starting) RB,CB or WR to return punts and kicks. Assigns a Center to be long snapper. (works on gamesave data in memory, not the text)
Use this option after you have applied data to a gamesave, before saving to a file.
#####Auto Update Depth chart
Automatically updates the teams' depth charts (in program memory) based on player index and position.
Players listed higher will be at the top of the depth chart, players listed lower will be lower on the depth chart.
#####Auto update Photo
Automatically update the 'Photo' attribute of each player based on name. (uses ''ENFPhotoIndex.txt' file, operates on text )
#####Auto Update PBP
Automatically Update what name gets called for a player (operates on text)


[![Bitdeli Badge](https://d2weczhvl823v0.cloudfront.net/BAD-AL/nfl2k5tool/trend.png)](https://bitdeli.com/free "Bitdeli Badge")

