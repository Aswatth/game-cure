# A Unity Game - Cure 
[Game link](https://play.google.com/store/apps/details?id=com.SAGamesInc.CURE)

A game to demonstrate prevailing pandemic situation (does not represent real life scenarios)

<b>Objective:</b>
  - Stay safe
  - Make the city virus free
  - Vaccinate people

<b>Gameplay:</b>
  - Use left joystick to move around
  - Click on handwash icon to clean an area and vaccinate icon to vaccinate people
  - Rush to hospital to heal yourself if you are infected
  - Collect shield which are scattered across the city
  - Each vaccination will add coins which can later be used to purchase more durable characters
  - Vaccinated NPCs are represented in white, uninfected NPCs are represented in green and infected NPCs are represented in red
  - HUD incdicates the uninfected, infected and spread count on top left, minimap on top right and player's health and coins at the bottom.
  - Game ends when player runs out of health or by making the city virus free.

<b>Working:</b>
<p>
  The city is represented as a grid and each cell will hold a value based on how long the virus is present in that particular cell hence increasing the spread rate to people who pass/ stay in this cell.
  
  When player clears an area, the values of all the cells within the clearing hemisphere is set to zero ensuring the cell does not initiate the spread unless someone infected passes through the particular cell again.
  
  Player has the ability to vaccinate both infected and non-infected NPCs making them immune to the spread. Vaccinating NPCs will display a progress bar on top of NPCs to indicate the vaccination progress.

  <img src="/Images/SS-1.png" width="400" height="200"/>

  While the player is not immune to spread, player's health can be regenerated if they are within the hospital bounds.
  
  <img src="/Images/SS-3.png" width="400" height="200"/>
  
  If the player is infected, health continues to degenerate and is indicated in two ways. One at the bottom indicating player's current health. Seondly, through post processing, the screen starts to turn red based on players health.
  
  <img src="/Images/SS-2.png" width="400" height="200"/>
    
</p>
