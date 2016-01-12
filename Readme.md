##Dreamcatcher##

A Unity mobile game for phones.

**Instructions for contributing**

Anyone is welcome to contribute, provided that I have added them as a contributor to the repository. If you have any questions, please hit me up online!

* Clone the repo.
* Make a branch off Master.
* Run with Unity 5.3 by opening the Main scene (in Assets/Scenes).
* If the only thing in your scene is the Main Camera, double-click on the Main scene again.
* Save the Main scene as a new scene (please do not work in the main scene!)
* Begin working.
* When you're done working, save, commit, push, and put in a pull request.

**Next tasks**

Here is a non-exhaustive list of the things that need doing. If you think of any others, please feel free to add them here or let me know!

* SaveManager
  * Needs to persist high scores to a JSON object in PersistentDataPath
* SoundManager
  * Needs to handle switching between songs, playing sound effects, and master and music/sfx volume controls.
  * We need sounds for _every_ player action and _every_ change of state for the game controller and for every enemy.
* Fleshing out the gameplay elements
  * Timer
	* Coding the timer to update the HUD element near the top of the screen (in place of a score tracker).
	* Hooking up event with the GameController to reset it OnGameStart.
	* Making sure it is suspended if the game is suspended.
	* Creating a graphic of a frame for it to go in near the top of the screen.
	* Hooking up to the SaveManager to store high scores.
  * Heartbeat tracker
	* Making sure the game controller knows when the player has lost.
	* Hooking it up to the HUD to show an overlaid graphic of an EKG or something when the player takes damage.
	* Should also update the HUD with the new BPM when health changes, of course.
	* Hooking it up to listen to Enemy events so that it registers damage to the player.
  * Enemies
	* Need to plug in sprites.
	* Set sprite animations for walking and hurting.
	* Set appropriate values for speeds and damage.
  * Dreamcatcher item
	* Putting a cooldown timer on it.
	* Making it so that, when selected, next player touch leaves a dreamcatcher that expands and catches any nightmares inside its radius.
  * Wall item
	* Putting a cooldown timer on it (can be the same code as the Dreamcatcher's cooldown).
	* When selected, next player's drag touch drops a particle effect that forms a wall that kills enemies that touch it.
  * Game Over Menu
	* Retry (triggers GameController.OnGameStart)
	* Title screen
* Title screen
  * Menu (Play, High scores, Options)
	* Play takes you to the main scene and starts the game.
	* High scores shows you the high scores locally (depends on SaveManager).
	* Options shows you sliders to change sound and music volumes (depends on SoundManager).
  
  