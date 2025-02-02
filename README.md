# ShooterAssess

## Approach to the solution:
I used the mouse for player movement. The right click on the mouse must be held
for the player view to follow the mouse movement. 

The left click is used to "shoot" at the "**Enemy**". 
The Enemies spawn in random locations. (I first defined 10 possible locations for 
spawning the "Enemy". 


The **EnemyManager** script is responsible for spawning the enemies, and it has a public
action that can be called to spawn one enemy. This action is invoked in the 
**GameManager** script to ensure that there's only 3 enemies spawned at a time. 
The EnemyManager also defines an action for when the enemy is destroyed and an action
for after the enemy is destroyed. 

The **Player** script allows for player camera movement and changing the color of the 
**Line Renderer** component. The Line Renderer is defined to use on 2 points. The *start* point of the straight line and the *end* point. On player movement, the end point of the line is adjusted to be *max distance* defined ahead(forward) of the player's view after 
player movement. It also defines a timer for when you can shoot again after a previous shot.
If the Physics raycast starting at the start of the line, going straight forward for a  *max distance* is used to get a **RayCastHit**, if the hit collider is of the **Enemy**, that enemy is destroyed using the defined action in **EnemyManager**. 

### Action/Event based approach
Given the problem being solved, there are certain events defined as constraints in the problem statement and that is why I went with actions. 

**GamaManager** script defines the *maximum score* to reach till the game restarts. The script defines the *Action* for when the max score is reached. It also defines *Actions* for when the game starts and restarts.

### Observer Design Pattern
Given that the subject of the pattern can be defined as the player, the observers can be the UI corresponding to the player state, I used the Observer Design Pattern to keep the UI up to date with the subject context. 

The subject (aka player) in the pattern only defines the score, a bool to determine if max score is reached and time taken to play. The Observer updates the score, time while playing, and time taken in total. 

The **GameManager** encapsulates both the observers and the subject. It invokes the subject to update time at each frame. On certain *actions*, it updates the score and checks for when maxScore is reached. It also defines functions used to update to do certain tasks on the *Actions* defined already. 

### Linked Lists
Since the solution can restart and repeat with the same execution, I used linked lists to defined what the next "**GameView**" is. Each GameView defines what the next GameView will be and the last "GameView" can define the next as the very first **GameView**, essentially making a loop. 

This approach allows the solution to grow even easier. The "links/next" can be adjusted and updated to add or even reorder the game views. The switching of the views is uniform and can be called from multiple places. In this solution the switch happens on the MaxScoreReached *action* and in button presses. 


 
## Time taken
As evident in the repository history, this took me more than 2 hours to complete 😅. I would estimate the time spent on it, including Readme Documentation to be 9 - 10 hours. 
## External Sources

- https://stock.adobe.com/205781751?clickref=1011lAddGWjy&mv=affiliate&mv2=pz&as_camptype=search-top&as_channel=affiliate&as_source=partnerize&as_campaign=cheezy#state=%7B%22ac%22%3A%22stock.adobe.com%22%7D&token_type=bearer&expires_in=86399

- https://atlas-content-cdn.pixelsquid.com/stock-images/bullseye-target-facial-expression-RB71OR8-600.jpg

- https://media.gettyimages.com/id/351942-001/photo/arrow-splitting-another-arrow-in-bulls-eye-of-target.jpg?s=1024x1024&w=gi&k=20&c=gWBY-iakw1eBtcOVwezGpLQ4wzHh3t51fkjnV88reNg=

