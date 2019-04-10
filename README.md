## ARA2D
A game about programming robots to make a factory.

I originally planned on making this game in 3D, but over time found that it would be better realized in 2D

Progress on the project can be viewed on the [projects tab](https://github.com/SpencasaurusRex/ARA2D/projects)

### Inspiration
The game draws inspiration from a variety of games:
+ Factory games ([Factorio](https://www.factorio.com/), [Satisfactory](https://www.satisfactorygame.com/))
+ Programming games ([Exapunks](http://www.zachtronics.com/exapunks/), [TIS-100](http://www.zachtronics.com/tis-100/))
+ Minecraft Mods ([ComputerCraft](http://www.computercraft.info/), [OpenComputers](https://github.com/MightyPirates/OpenComputers))

## Puzzles?
I like the idea of a sandbox game, but I also think this idea would apply really well to a puzzle adaptation.

### Programming
Robots scripts are run via a reduced command set
```
move
back
left
right
give
take
```
It's not immediately clear how I should handle failures. For example what should two robots do when they are trying to move to the same space? I still need to think about this. Likely there will be parameters for some commands, for example: `move retry`. A command like that will have to take into account deadlock and other edge cases.

The command set itself is generally not sophisticated enough to get more complicated tasks done. For these types of tasks, the player is allowed to write their own commands with lua.
For example:
```lua
function square(amount)
  amount = amount or 1
  for i = 1,amount do
    for t = 1,4 do
      move()
      left()
    end
  end
end
```

### Robots
Robots are highly configurable with different upgrades available to suit the task at hand

| Upgrade     | Description                                    |
|-------------|------------------------------------------------|
| Battery     | Robot lasts longer before running out of charge|
| Storage     | Robot can store more than one slot of items    |
| Solar Panel | Robot can generate electricity by itself       |
| TODO        | More ideas                                     |

### Production Gameplay
The production mechanics will be similar to that of other "Factory"-style game. Raw resources can be attained, which through several production steps can be refined and crafted into different products. The resulting products can be combined to make more advanced products. The products made by the factory go into making the factory bigger and more efficient. The game will have the creation of some advanced resource-intensive product as the end-goal of the game.

### Automation
I plan to let the player automate everything. Ideally it should be possible to start with a single robot and complete the game with a sufficiently advanced script.
