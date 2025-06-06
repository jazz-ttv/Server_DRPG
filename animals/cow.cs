datablock fxDTSBrickData (BrickCowBot_HoleSpawnData)
{
	brickFile = "Add-ons/Bot_Hole/4xSpawn.blb";
	category = "Special";
	subCategory = "Holes";
	uiName = "Cow Hole";
	iconName = "Add-Ons/Bot_Blockhead/icon_blockhead1";

	bricktype = 2;
	cancover = 0;
	orientationfix = 1;
	indestructable = 1;

	isBotHole = 1;
	holeBot = "CowHoleBot";
};

datablock PlayerData(CowHoleBot : PlayerStandardArmor)
{
	shapeFile = "Add-Ons/Player_FarmAnimals/Cow.dts";
	uiName = "Cow";
	minJetEnergy = 0;
	jetEnergyDrain = 0;
	canJet = 0;
	maxItems   = 0;
	maxWeapons = 0;
	maxTools = 0;
	runforce = 100 * 90;
	maxForwardSpeed = 8;
	maxBackwardSpeed = 4;
	maxSideSpeed = 8;
	attackpower = 10;
	rideable = false;
	canRide = true;

	maxdamage = 75;//Bot Health
	jumpSound = "";//Removed due to bots jumping a lot
	
	//Hole Attributes
	isHoleBot = 1;

	//Spawning option
	hSpawnTooClose = 0;//Doesn't spawn when player is too close and can see it
	  hSpawnTCRange = 8;//above range, set in brick units
	hSpawnClose = 1;//Only spawn when close to a player, can be used with above function as long as hSCRange is higher than hSpawnTCRange
	  hSpawnCRange = 64;//above range, set in brick units

	hType = Friendly; //Enemy,Friendly, Neutral
	  hNeutralAttackChance = 0; //0 to 100, Chance that this type will attack neutral bots, ie horses/vehicles/civilians
	//can have unique types, nazis will attack zombies but nazis will not attack other bots labeled nazi
	hName = "Cow";//cannot contain spaces
	hTickRate = 3000;
	
	//Wander Options
	hWander = 1;//Enables random walking
	  hSmoothWander = 1;//This is in addition to regular wander, makes them walk a bit longer, and a bit smoother
	  hReturnToSpawn = 1;//Returns to spawn when too far
	  hSpawnDist = 48;//Defines the distance bot can travel away from spawnbrick
	  hGridWander = 0;//Locks the bot to a grid, overwrites other settings
	
	//Searching options
	hSearch = 0;//Search for Players
	  hSearchRadius = 64;//in brick units
	  hSight = 1;//Require bot to see player before pursuing
	  hStrafe = 1;//Randomly strafe while following player
	hSearchFOV = 0;//if enabled disables normal hSearch
	  hFOVRange = 0.7; // determines the angle we can see the player at, 0.7 is about normal fov for players, if 0 we can see all in front, if 1 we could only see someone perfectly ahead, can be negative
	  // hFOVRadius = 6; // no longer used, we now use searchRadius
	  hHearing = 1;//If it hears a player it'll look in the direction of the sound

	  hAlertOtherBots = 1;//Alerts other bots when he sees a player, or gets attacked

	//Attack Options
	hMelee = 0;//Melee
	  hAttackDamage = 15;//Melee Damage
	hShoot = 0;
	  hWep = "";
	  hShootTimes = 4;//Number of times the bot will shoot between each tick
	  hMaxShootRange = 256;//The range in which the bot will shoot the player
	  hAvoidCloseRange = 1;//
		hTooCloseRange = 7;//in brick units

	//Misc options
	hActivateDirection = 0; // 0 1 2, determines the direction you have to face the bot to activate him, both, front back
	hMoveSlowdown = 0; // bool, determines wether the bot will slow down when following enemies
	hAvoidObstacles = 1;
	hSuperStacker = 0;//When enabled makes the bots stack a bit better, in other words, jumping on each others heads to get to a player
	hSpazJump = 0;//Makes bot jump when the user their following is higher than them

	hAFKOmeter = 1;//Determines how often the bot will wander or do other idle actions, higher it is the less often he does things

	hIdle = 1;// Enables use of idle actions, actions which are done when the bot is not doing anything else
	  hIdleAnimation = 0;//Plays random animations/emotes, sit, click, love/hate/etc
	  hIdleLookAtOthers = 0;//Randomly looks at other players/bots when not doing anything else
	    hIdleSpam = 0;//Makes them spam click and spam hammer/spraycan
	  hSpasticLook = 0;//Makes them look around their environment a bit more.
	hEmote = 0;
	
	hEXP = 40;
	lowDrop = 1;
	highDrop = 2;
	dropID = "BEEF";
	secondDropID = "LEATHER";
	requiredLevel = 25;
};