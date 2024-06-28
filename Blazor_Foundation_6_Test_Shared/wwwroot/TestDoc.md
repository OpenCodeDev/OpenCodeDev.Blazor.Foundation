## What is Polyliner Tool?
The polyline tool allows you to save drawings/lines on the map as code so you don't have to draw all the lines during the briefing thus saving load time and increasing the fun time.

## How to Save Lines?

1. Open your mission in the 3den editor.
1. Start Scenario and get in the game.
1. Open the map.
1. Start drawing using <code>CTRL + LMB</code>.
1. Once drawing is completed, close the map and press <code>ESC</code>.
1. Execute (```Exec Local```) the following code: 

[--HighlightCode(Language="sqf")--]
[] call BrokenSkullMods_fnc_generatePolylines;

[--/HighlightCode--]

Once confirmation code ```OK (XXXX)!```,

Return to the editor, by now the code to draw the lines is stored in your memory.

You have multiple options:
1. Place a cover map and paste in the INIT section using ```CTRL + V```.
1. Paste using ```CTRL+V``` in ```initPlayerLocal.sqf``` at the mission root.