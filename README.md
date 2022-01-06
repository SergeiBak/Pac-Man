# Pac-Man
<img width="276.48" height="155.52" src="https://github.com/SergeiBak/PersonalWebsite/blob/master/images/Pacman.png?raw=true">

## Table of Contents
* [Overview](#Overview)
* [Test The Project!](#test-the-project)
* [Code](#Code)
* [Technologies](#Technologies)
* [Resources](#Resources)
* [Donations](#Donations)

## Overview
This project is a recreation of the classic 1980 arcade game known as Pac-Man, with over $14 billion generated in revenue! This solo project was developed in Unity using C# as part of my minigames series where I utilize various resources to remake simple games in order to further my learning as well as to have fun!   

Pac-Man consists of a maze structure, in which four colored ghosts — Blinky (red), Pinky (pink), Inky (cyan), and Clyde (orange) — pursue him. The goal of the game is to eat all of the dots in the maze, which will allow the player to move onto the next level. Each of the Ghosts have a unique AI behavior, causing them to behave differently when chasing Pac-Man. To aid Pac-Man, there are power pellets in each of the 4 corners of the maze that will grant him the ability to eat ghosts for a limited period of time.     

## Test The Project!
In order to play this version of Pac-Man, follow the [link](https://sergeibak.github.io/PersonalWebsite/PacMan.html) to a in-browser WebGL build (No download required!).

## Code
A brief description of all of the classes is as follows:
- The `AnimatedImage` class handles the cycling of sprites on a gameobject's Image component, giving the appearance of animation.
- The `AnimatedSprite` class handles the cycling of sprites on a gameobject's SpriteRenderer component, giving the appearance of animation.
- The `BlinkyTarget` class controls the position of Blinky's (Red Ghost) target position, as well as the visibility of it.
- The `ClydeTarget` class controls the position of Clyde's (Orange Ghost) target position, as well as the visibility of it.
- The `Fruit` class is attached to each in-game fruit object, and handles the despawning of the object as well as awarding Pac-Man with points on collision.
- The `GameManager` class controls the state of the game, such as setting new rounds, updating UI, playing audio, etc.
- The `Ghost` class is attached to each Ghost, and holds a reference to all of its attached states as well as the ability to reset & handle collisions with Pac-Man.
- The `GhostBehavior` class is an abstract class that is extended by all of the ghost states, and holds some basic enable/disable methods.
- The `GhostChase` class handles the chasing state for all of the ghosts using node collisions & target position.
- The `GhostEyes` class handles setting the eye sprite to match the direction the Ghost is traveling in.
- The `GhostFrightened` class handles the frightened state (including changing Ghost appearance) for all of the ghosts using node collisions.
- The `GhostHome` class handles the home state for all of the ghosts, as well as playing the animation for when ghosts leave the home.
- The `GhostRetreat` class handles the retreat state for all of the ghosts, which involves the ghost's eyes running back to the home block before respawning.
- The `GhostScatter` class handles the scatter state for all of the ghosts, which involves each ghost running to their respective corner of the map.
- The `InkyTarget` class controls the position of Inky's (Cyan Ghost) target position, as well as the visibility of it.
- The `MenuController` class controls everything in the menu scene, which includes the animation & loading of GameScene.
- The `Movement` class handles the movement of Pac-Man & the Ghosts. It stores the current & next travel directions, and translates the object into the current direction.
- The `Node` class is attached to each Node (intersection) on the map, and calculates + stores each available direction from said node.
- The `Pacman` class handles getting input from the user for which direction to travel into next.
- The `PacmanDeath` class handles the animation of Pac-Man's death, as well as its disappearance once the animation is done.
- The `Passage` class handles the teleportation of Pac-Man/Ghosts to the other side of the map whenever they pass through.
- The `Pellet` class is attached to each in-game dot object, and handles the awarding Pac-Man with points on collision.
- The `PinkyTarget` class controls the position of Pinky's (Pink Ghost) target position, as well as the visibility of it.
- The `PowerPellet` class is attached to each in-game power dot object, and handles the awarding Pac-Man with points on collision as well as signalling the GameManager to set all of the Ghosts to a frightened state.

## Technologies
- Unity
- Visual Studio
- GitHub
- GitHub Desktop

## Resources
- Credit goes to [Zigurous](https://www.youtube.com/channel/UCyaKsKqYTghxgAqywfefAzg) for the helpful base game tutorial!
- Dots for fruit to appear [reference](https://www.xboxachievements.com/game/arcade-game-series-pac-man/achievement/115190-Cherry.html#:~:text=Fruits%20in%20Pac%2DMan%20appear,having%20not%20been%20picked%20up)
- Pac-Man fruit points system [reference](https://pacman.fandom.com/wiki/Point_Configurations)
- Pac-Man extra life [reference](https://gaming.stackexchange.com/questions/24890/when-do-you-get-extra-life-in-pac-man)
- Pac-Man fruit UI display [reference](https://www.youtube.com/watch?v=NKKfW8X9uYk)
- Pac-Man ghost AI [reference](https://www.youtube.com/watch?v=ataGotQ7ir8)
- Pac-Man chase/scatter [reference](https://www.gamedeveloper.com/design/the-pac-man-dossier)
- Pac-Man gameplay [reference](https://www.youtube.com/watch?v=7O1OYQRqUag)

## Donations
This game, like many of the others I have worked on, is completely free and made for fun and learning! If you would like to support what I do, you can donate at my metamask wallet address: ```0x32d04487a141277Bb100F4b6AdAfbFED38810F40```. Thank you very much!
