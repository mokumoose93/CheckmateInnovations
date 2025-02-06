---
title: Resources Guide
created: Wednesday 5th February 2025 16:38
Last Modified: Wednesday 5th February 2025 16:38
aliases: 
tags:
  - programming
  - software_engineering
---
# Resources Guide
---
## Learning Resources
---
### Videos
---
- [Integrating VS Code into Unity](https://youtu.be/-AgcVsS-rtQ)
	- I already went through the process of integrating VS Code into our Unity project but feel free to watch this video to get familiar with the changes if you run into issues.
	- He also covers some really helpful VS Code Extensions that help coding in C#
		- I don't recommend the intellisense feature - I found it kind of annoying to use, but you may like what it has to offer. Experiment
- [Using GitHub with Unity](https://youtu.be/qpXxcvS-g3g)
	- I already went through the Unity Setup but I recommend watching this anyways so that you can:
		- Learn to get GitHub set up on your desktop
		- Get familiar with these important steps:
			- creating a branch
			- making changes to your local file
			- committing those changes
			- pushing those changes to the GitHub repositor

### Unity Documentation
---
- [Unity Scripting Reference](https://docs.unity3d.com/ScriptReference/index.html)
	- shows all the existing code
- [Unity User Manual](https://docs.unity3d.com/Manual/index.html)
	- User manual giving you information on how to use Unity
	- I recommend going through this manual as a SUPPLEMENT to watching videos. It's very open ended which will be overwhelming for beginner use but it does go into detail which will help you add depth to your design goals
## Development Notes
---
### Code Testing Folders (**PLEASE READ THIS**)
---
- **I added a Testing folder "../Assets/Scripts/Testing/"**
	- I also added in that folder my own "david-testing" folder
	- please add your own folders with the naming convention "yourName-testing"
	- feel free to use any name but make sure we all understand that it is you
	- please only use YOUR OWN folder for code testing
- **Why use this folder?**
	- as we are all getting familiar with unity we will probably have to create test files to better understand how certain functions work
	- e.g. I was having trouble accessing certain tilemap tiles via nested for loops because I didn't understand that the locations were based on world space and not necessarily local to the tilemap "array". I used Debug.Log("") to figure out that the tilemap origin was actually (-2, -4, 0) and accessing that origin tile wasn't a simple (0, 0, 0).
	- You may run into similar situations where the Unity API is not so intuitive and may require testing, please use your assigned folder to perform those tests to help reduce clutter.
## To-do List
---
- [x] Add Naming Convention guide
- [ ] Have everyone create their own Testing Folders
- [ ] Maybe move To-do list to its own file?

