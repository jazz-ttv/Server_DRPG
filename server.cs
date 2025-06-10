forceRequiredAddon("Bot_Hole");
forceRequiredAddon("Player_FarmAnimals");
$DRPG::Version = 0.10;
$DRPG::ClientVersion = 0.17;

$DRPG::Prefs::BuyMultiplier = 1.2;
//Added these for easier balancing
$DRPG::Prefs::ExpMultiplier["smithing"] = 1;
$DRPG::Prefs::ExpMultiplier["fletching"] = 1;
$DRPG::Prefs::ExpMultiplier["cooking"] = 1;
$DRPG::Prefs::ExpMultiplier["melee"] = 1;
$DRPG::Prefs::ExpMultiplier["defense"] = 1;
$DRPG::Prefs::ExpMultiplier["archery"] = 1;
$DRPG::Prefs::ExpMultiplier["woodcutting"] = 1;
$DRPG::Prefs::ExpMultiplier["mining"] = 1;
$DRPG::Prefs::ExpMultiplier["farming"] = 1;
$DRPG::Prefs::ExpMultiplier["building"] = 1;
$DRPG::Prefs::ExpMultiplier["herblore"] = 1;
//Also these for making people less rich
$DRPG::Prefs::PriceMultiplier["apparel"] = 1;
$DRPG::Prefs::PriceMultiplier["armor"] = 1;
$DRPG::Prefs::PriceMultiplier["weapons"] = 1;
$DRPG::Prefs::PriceMultiplier["ammo"] = 1;
$DRPG::Prefs::PriceMultiplier["wood"] = 1;
$DRPG::Prefs::PriceMultiplier["ore"] = 1;
$DRPG::Prefs::PriceMultiplier["metal"] = 1;
$DRPG::Prefs::PriceMultiplier["seeds"] = 1;
$DRPG::Prefs::PriceMultiplier["crops"] = 1;
$DRPG::Prefs::WildMobs = 1;
$DRPG::Prefs::WitherTime = 5;

function serverCmddexec(%cl,%fi)
{
	if(%cl.isSuperAdmin)
	{
		if(%fi $= "")
		{
			%fi = "server";
		}
		discoverFile("Add-Ons/Server_DRPG.zip");
		if(isFile("Add-Ons/Server_DRPG/" @ %fi @ ".cs"))
		{
			exec("Add-Ons/Server_DRPG/" @ %fi @ ".cs");
			messageClient(%cl,'',"\c6Executed:\c3" SPC %fi @ ".cs");
		} else {
			messageClient(%cl,'',"\c6No such file:\c3" SPC %fi @ ".cs");
		}
	}
}

function addItemToList(%string,%item)
{
	if(hasItemOnList(%string,%item))
		return %string;

	if(%string $= "")
		return %item;
	else
		return %string SPC %item;
}
function hasItemOnList(%string,%item)
{
	for(%i=0;%i<getWordCount(%string);%i++)
	{
		if(getWord(%string,%i) $= %item)
			return 1;
	}
	return 0;
}
function removeItemFromList(%string,%item)
{
	if(!hasItemOnList(%string,%item))
		return %string;

	for(%i=0;%i<getWordCount(%string);%i++)
	{
		if(getWord(%string,%i) $= %item)
		{
			if(%i $= 0)
				return getWords(%string,1,getWordCount(%string));
			else if(%i $= getWordCount(%string)-1)
				return getWords(%string,0,%i-1);
			else
				return getWords(%string,0,%i-1) SPC getWords(%string,%i+1,getWordCount(%string));
		}
	}
}
exec("Add-Ons/Brick_Plant/server.cs");
//---MAIN---
exec("./main.cs");
//---DATABASE---
exec("./database.cs");
// tick shit
exec("./tick.cs");
//---EQUIPMENT---
exec("./equipment.cs");
//---NATIONS---
exec("./nations.cs");
//---ZONES---
exec("./zones.cs");
//---MOBS---
exec("./mobs.cs");
//---ITEMS---
exec("./items.cs");
//misc
exec("./misc.cs");
//---SHOPS---
exec("./shops.cs");
//---LEVELS---
exec("./levels.cs");
//---PACKAGE---
exec("./package.cs");
//---TOOLS---
exec("./tools.cs");
//---WOODCUTTING---
exec("./woodcutting.cs");
//---MINING---
exec("./mining.cs");
//---CRAFTING---
exec("./crafting.cs");
//---FARMING---
exec("./farming.cs");
//---PLAYER PERKS---
exec("./playerperks.cs");

exec("./quizgame.cs");

exec("./herblore.cs");

exec("./quest_event.cs");

exec("./potions.cs");

exec("./hunting.cs");
exec("./animals/turkey.cs");
exec("./animals/goat.cs");
exec("./animals/ram.cs");
exec("./animals/bull.cs");
exec("./animals/cow.cs");

exec("./seeding.cs");

exec("./archery.cs");
exec("./chat.cs");
exec("./armor.cs");

//exec("./dungeons.cs");

// Init content - recipes, items, etc
exec("./content.cs");

//---WEB END: VIEWING SERVER STUFF IN YOUR BROWSER YAY
//Disabled
//exec("./webend.cs");

exec("./shapes/Weapon_Spear.cs");








function ServerCmdhelp(%client,%z)
{
	messageclient(%client,"","\c6Welcome to \c0DRPG\c6!");
	messageclient(%client,"","\c6If this is your first time playing, it is strongly recommended that you read this entire tutorial.");
	messageclient(%client,"","\c6--- INDEX ---");
	messageclient(%client,"","\c0 1\c6. Getting Started");
	messageclient(%client,"","\c0 2\c6. Crafting");
	messageclient(%client,"","\c0 3\c6. Farming");
	messageclient(%client,"","\c0 4\c6. Nations");
	messageclient(%client,"","");
	messageclient(%client,"","\c0------------------Getting Started------------------");
	messageclient(%client,"","\c6Getting started is easy! DRPG is basically about grinding materials/resources to get better gear, to join nations, and to become the most powerful");
	messageclient(%client,"","\c6To get started, open your control menu (CTRL O for short) and scroll down until you see the DRPG subcategory. Set a key to your DRPG GUI.");
	messageclient(%client,"","\c6Open the GUI, and equip your pickaxe and axe.");
	messageclient(%client,"","\c6Head to the lumbering area, and cut down some trees! Cutting trees gets you woodcutting experience and gets you logs.");
	messageclient(%client,"","\c6There are many different trees available, but when starting out you will only be able to cut down oak trees (Brown trees) as they are level 1.");
	messageclient(%client,"","\c6Once you have a satisfying amount of oak logs, head to the mining area. Here you can mine ores, and like woodcutting, gain XP to level up.");
	messageclient(%client,"","\c6These are the basics of DRPG! Oh, and by the way, once you get past total level 10, other players can kill you. Gear up by reading more.");
	messageclient(%client,"","");
	messageclient(%client,"","\c0------------------Crafting------------------");
	messageclient(%client,"","\c6Once you have at least 1 wood, 1 copper, and 1 tin, open your crafting menu in the GUI");
	messageclient(%client,"","\c6Try crafting a Bronze Bar. You can level up your smithing level by crafting items that are metal related.");
	messageclient(%client,"","\c6You can also craft things like bows and arrows to increase your fletching level.");
	messageclient(%client,"","\c6All items in the crafting menu will show what items are needed. The more you level up in an area, the more crafting recipies are shown.");
	messageclient(%client,"","\c6Don't know how to find an item required? Ask another player! Everyone is here to help.");
	messageclient(%client,"","");
	messageclient(%client,"","\c0------------------Farming------------------");
	messageclient(%client,"","\c6Farming is a great way to make some money.");
	messageclient(%client,"","\c6To get farming, you need to buy a sickle and some wheat seeds from a shop. Every shop sells them.");
	messageclient(%client,"","\c6Once you bought the seeds, open the GUI, look at the ground and hit use on the seeds. They will start growing.");
	messageclient(%client,"","\c6Some people like to steal other's crops, so its good to build your own house to protect your crops.");
	messageclient(%client,"","\c6There are many different seeds that can be bought from the exclusive Elrad markets. There are turnips, tomatos, and carrots.");
	messageclient(%client,"","\c6Carrots are the hardest to grow because they are DRPG. When you use a carrot, you get mounted to a horse.");
	messageclient(%client,"","\c6Happy Farming, and remember, this isn't Farmville! Don't get too obsessed with your crops!");
	messageclient(%client,"","\c0------------------Nations------------------");
	messageclient(%client,"","\c6Nations are a great way to have fun with your friends and get part of a community in DRPG.");
	messageclient(%client,"","\c6There are 4 nations: \c6Derma, \c0Elrad, \c4Vanote,\c6 and \c2Oloni.");
	messageclient(%client,"","\c6The nations have different proficiencies, and are completely ruled by the players.");
	messageclient(%client,"","\c6Nations can have perks, and those perks affect the member's bonuses.");
	messageclient(%client,"","\c0Oloni \c6- Mining Nation. Located in a beach biome with a large mine containing two DRPGty ores.");
	messageclient(%client,"","\c0Elrad \c6- Commerce Nation. Has 3 DRPG markets that sell DRPG items. ");
	messageclient(%client,"","\c0Derma \c6- Woodcutting Nation. Has large patches of trees, and has the only rarest tree.");
	messageclient(%client,"","\c0Vanote \c6- Stock Nation. Has no DRPGties. The nation of natural born hipsters.");
	messageclient(%client,"","\c6That concludes the help section of DRPG. Remember, other players are here to help!");
	messageclient(%client,"","\c0^^^PRESS PAGE UP ON YOUR KEYBOARD^^^");
}

