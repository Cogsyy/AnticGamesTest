# AnticGamesTest
Create a Unity game where ant defenders protect a flag from approaching enemy insects (ladybugs,
beetles, and aphids). The defenders start at the flag position and must intercept enemies.

## Folder structure / organization
In my experience, most projects tend to organize their folders by file type (Script, Scenes, Prefabs, etc.), and usually as a result I follow this to keep the project structure consistent.
However, I personally prefer to place related assets in similar folders on a feature basis, so I've done that here. I have prefabs and scripts related to game units in the GameUnits folder, with subfolders, and 
UI scripts and prefabs inside a UI folder. I find this method to be superior, especially in larger projects, because it reduces the need to jump contexts, and browse multiple folders to find what you need, when in reality,
you are most likely to be working on related scripts. I find it easier to navigate in this way, where the typical organize-by-asset-type default feels like it doesn't have many thought out benefits other than placing assets in arbitrary bins.
Arbitrary bins however, is better organization than everything in one folder, and to me it feels like that's its only benefit.

## Naming conventions
For the most part, I do my best to follow typical C# conventions, and those laid out by Unity Technologies in their docs. Functions are capitalized, private variables are underscored, fields at the top and grouped by relevancy. Const variables are capitalized.

## Project scaling
I believe this architecture can work for larger projects, as behaviours and classes are well defined and seperated. There's support for multiple different factories, unit types, and movement components that can be added on as needed.
One thing I would consider missing is an overall game state system, something that tracks the current state a bit better than what I have, but I did not choose to focus on this. Something more of a finite state machine with more clear game states laid out and transitions to and from is a pattern I've followed before, and would work well with this project as well.

## Search algorithm
This is my first time working with a spatial partitioning system, and it has been interesting. It works as was described in the instructions, but when it comes to searching, I use a simple algorithm to check
all the spaces around a given unit in a square and check if there are any units there. If there is, I'll do a distance check, and if it meets search criteria, it stops there, since the closest unit is going to be one of the units inside the search radius since it starts at the unit itself and works outward. 
If a unit is not found, the search radius increases to one tile further in all directions in a square shape, again, stopping if a unit meets the search criteria.
In addition to this, the ant will search for the closest enemy around the flag and check its distance. If it's not within a threatening range, then it will prioritize units closer to himself. 

## Observations
- This is my first time working with a spatial partitioning system. One thing I noticed was a lot of examples used a Grid class that doesn't take a generic <TCell> param. In this project I sticked with the generic param, but as development continued,
I found myself questioning it's value and came to realize why others don't use it. At face value, it seems useful, but since at some point you necessarily have to couple your units into your grid system when registering and moving units (Demonstrated with IUnit here), It feels like making something generic to fit a specific purpose.
It does have its benefits though, at minimum it can be reused for different types of IUnits, which is pretty minimalistic and flexible on its own.
- This is also my first time working with the new Unity Input System, and I quite like it so far. One thing of note, it seems like there are a lot of ways to work with the asset, so I am uncertain if my usage was optimal or not, but it seems nice so far for my first go at it.

## instructions
- Game and unit settings can be found in Assets > GameSettings. The game will work with multiple friendly ants as well
- Pause with esc key, and can use the arrow keys/WASD to navigate menus + enter for submit, or use mouse and keyboard.
- Left click to target an enemy unless AI mode is on from the pause menu
- I've included a build under the Build Folder
- I've also included UML diagrams in the root folder.
- The game is not built to handle resetting things properly, please restart the application for this!
- The ant flashes red when he attacks.
- The aphids will group with other enemy units at the same time as moving towards the flag, and have one health. The beetles are slower, and have 3 health each.

## Preview
- See Preview.mp4 in the root directory
