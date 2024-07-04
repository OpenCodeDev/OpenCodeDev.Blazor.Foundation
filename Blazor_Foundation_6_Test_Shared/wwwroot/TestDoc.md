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

[--HighlightCode(Language="json")--]
{
    "MMO": {
        "Brief": [
            "Assault the radar outpost and destroy it and all the anti-aircraft on the object.",
            "HORNET 1-1 is not allowed in the air until this objective is completed."
        ],
        "Zeus": []
    },
    "SMO-1": {
        "Brief": [
            "Assault the village and destroy the enemy's ammo depot.",
            "Search marked buildings for intelligence."
        ],
        "Zeus": []
    },
    "SMO-2": {
        "Brief": [
            "Destroy the enemy supply line and search for Shiek Abudobi, Shcmalek Abudobi's brother."
        ],
        "Zeus": [
            "Shiek will not be found and the hidden task to capture him will show.",
            "Shiek should be taken alive.",
            "By that time team already knows about shiek's position Hornet 1-1 should take out the 2 support target before the team leaves the area."
        ]
    },
    "Capture Shiek Abudbobi (Hidden)": {
        "Brief": [
            "You can informe the team to retrieve shiek alive and to wait for HORNET 1-1 confirmation to move there."
        ],
        "Zeus": [
            "If HORNET 1-1 failed to destroy the vehicles, a AI jet will flyby and shoot it make sure to set the health to 0 in case of failure.",
            "Once Shiek is captured, Hornet 1-1 should come pick him up."
        ]
    },
    "PMO": {
        "Brief": [
            "The goal here is to assasinate Shcmalek Abudobi, plain and simple."
        ],
        "Zeus": [
            "Once, team enters the AO, they will be informed about a convoy being prepared in the town of Landay which will not be active until Schmalek is dead.",
            "Once target is eliminated, they will be notified to escape the area as the convoy starts moving towards their position and extract position will be available nearby.",
            "At this point, HORNET 1-1 can try to help the team on peril of being shot down."
        ]
    },
}

[--/HighlightCode--]
[--OperationObjectiveCard()--]
    {
        "MMO": {
            "Brief": [
                "Assault the radar outpost and destroy it and all the anti-aircraft on the object.",
                "HORNET 1-1 is not allowed in the air until this objective is completed."
            ],
            "Zeus": []
        },
        "SMO-1": {
            "Brief": [
                "Assault the village and destroy the enemy's ammo depot.",
                "Search marked buildings for intelligence."
            ],
            "Zeus": []
        },
        "SMO-2": {
            "Brief": [
                "Destroy the enemy supply line and search for Shiek Abudobi, Shcmalek Abudobi's brother."
            ],
            "Zeus": [
                "Shiek will not be found and the hidden task to capture him will show.",
                "Shiek should be taken alive.",
                "By that time team already knows about shiek's position Hornet 1-1 should take out the 2 support target before the team leaves the area."
            ]
        },
        "Capture Shiek Abudbobi (Hidden)": {
            "Brief": [
                "You can informe the team to retrieve shiek alive and to wait for HORNET 1-1 confirmation to move there."
            ],
            "Zeus": [
                "If HORNET 1-1 failed to destroy the vehicles, a AI jet will flyby and shoot it make sure to set the health to 0 in case of failure.",
                "Once Shiek is captured, Hornet 1-1 should come pick him up."
            ]
        },
        "PMO": {
            "Brief": [
                "The goal here is to assasinate Shcmalek Abudobi, plain and simple."
            ],
            "Zeus": [
                "Once, team enters the AO, they will be informed about a convoy being prepared in the town of Landay which will not be active until Schmalek is dead.",
                "Once target is eliminated, they will be notified to escape the area as the convoy starts moving towards their position and extract position will be available nearby.",
                "At this point, HORNET 1-1 can try to help the team on peril of being shot down."
            ]
        },
    }
[--/OperationObjectiveCard--]
Once confirmation code ```OK (XXXX)!```,

Return to the editor, by now the code to draw the lines is stored in your memory.

You have multiple options:
1. Place a cover map and paste in the INIT section using ```CTRL + V```.
1. Paste using ```CTRL+V``` in ```initPlayerLocal.sqf``` at the mission root.