$DRPG::Resources::Radius = 2500; // 5000
$DRPG::Resources::Center = "0 0 0"; // "-400 400 400"
$DRPG::Resources::Brickgroup = Brickgroup_380;
function servercmdseed(%cl,%trees,%rocks,%herbs)
{
	%permissions = %cl.bl_id == 380;

	if(%permissions)
    {
		messageAll('', "Seeding began at" SPC getDateTime());
		DRPGSeedResources("trees" SPC %trees @ "\tveins" SPC %rocks @ "\therbs" SPC %herbs @ "\tmoonwelldist 500");
	}
}
function servercmdseedstop(%cl)
{
	%permissions = %cl.bl_id == 380;

	if (%permissions)
    {
		messageAll('', "Seeding stopped at" SPC getDateTime());
		cancel($DRPG::Resources::SeedSchedule);
	}
}
function DRPGSeedResources(%ruleset)
{
	if(isEventPending($DRPG::Resources::SeedSchedule))
	{
		error("Resource seeding already in progress!");
		return;
	}
	for(%i=0;%i<getFieldCount(%ruleset);%i++)
	{
		%rule = getField(%ruleset,%i);
		%ruletype = getWord(%rule,0);
		%ruleval = getWords(%rule,1);
		$DRPG::Resources::Rule[%ruletype] = %ruleval;
	}
	$DRPG::Resources::SeedSchedule = "";
	$DRPG::Resources::TreeCount = 0;
	$DRPG::Resources::OreVeins = 0;
	$DRPG::Resources::HerbFields = 0;
	new SimSet(MoonwellSet);
	DRPGSeed();
}
function DRPGSeed()
{
	cancel($DRPG::Resources::SeedSchedule);
	if($DRPG::Resources::TreeCount < $DRPG::Resources::Rule["trees"])
	{
		DRPGSeedTree();
	} 
	else if($DRPG::Resources::OreVeins < $DRPG::Resources::Rule["veins"])
	{
	DRPGSeedOre();
	}
	else if ($DRPG::Resources::HerbFields < $DRPG::Resources::Rule["herbs"])
	{
		DRPGSeedHerb();
	}
	else 
	{
		return DRPGSeedComplete();
	}
	bottomprintAll("T:" SPC $DRPG::Resources::TreeCount SPC "/" SPC $DRPG::Resources::Rule["trees"] SPC ": O:" SPC $DRPG::Resources::OreVeins SPC "/" SPC $DRPG::Resources::Rule["veins"] SPC ": H:" SPC $DRPG::Resources::HerbFields SPC "/" SPC $DRPG::Resources::Rule["herbs"], 1);
	dstcontinue();
}
function DRPGSeedComplete()
{
	messageAll('',"\c6Finished seeding trees and ores (" @  getDateTime() @ "). Seeding plants...");
	if(!isObject(DRPGWildManager))
	{
		new ScriptObject(DRPGWildManager);
	}
	DRPGWildManager.check(DRPGFarming_Wheat);
	DRPGWildManager.check(DRPGFarming_Turnip);
	DRPGWildManager.check(DRPGFarming_Tomato);
	DRPGWildManager.check(DRPGFarming_Carrot);
}
function dstcontinue()
{
	return $DRPG::Resources::SeedSchedule = schedule(10,0,"DRPGSeed");
}
function dstWaterblock(%pos, %radius)
{
	%search = containerFindFirst($TypeMasks::PhysicalZoneObjectType,%pos,%radius,%radius,%radius);
	if(isObject(%search))
	{
		%obj = firstWord(%search);
		if(%obj.isWater)
        {
			return %search;
		}
		return 0;
	}
	return 0;
}
function DRPGSeedTree(%pos)
{
	if(%pos $= "")
	{
		%pos = vectorAdd($DRPG::Resources::Center,getRandom(0 - $DRPG::Resources::Radius,$DRPG::Resources::Radius) SPC getRandom(0 - $DRPG::Resources::Radius,$DRPG::Resources::Radius) SPC 100);
	}
	%ray = containerRayCast(%pos,vectorAdd(%pos,"0 0 -1000"),$TypeMasks::All);
	%obj = getWord(%ray,0);
	%boxcenter = vectorAdd(posFromRaycast(%ray),"0 0 2.2");
	%rpos = vectorAdd(posFromRaycast(%ray),"0 0 1.8");
	%db[0] = "brickDRPGOakData";
	%db[30] = "brickDRPGMapleData";
	%db[50] = "brickDRPGYewData";
	%db[60] = "brickDRPGMoonwellData";

	%db = getRandom(0,70);
	for(%i=%db;%i>=0;%i--)
	{
		if(%db[%i] !$= "")
		{
			%db = %i;
			break;
		}
	}
	%water = dstwaterblock(%rpos, 6);
	if(isObject(%water))
	{
		%dbn = "brickDRPGWillowData";
	}
	if(%dbn $= "")
	{
		%dbn = %db[%db];
	}
	if(%dbn $= "brickDRPGMoonwellData")
	{
		for(%i=0;%i<Moonwellset.getcount();%i++)
		{
			%moonwell = Moonwellset.getObject(%i);
			if(vectorDist(%moonwell.getWorldBoxCenter(),%rpos) < $DRPG::Resources::Rule["moonwelldist"])
			{
				return;
			}
		}
	}
	%br = new fxDTSbrick()
	{
		datablock = %dbn;
		position = %rpos;
		angleID = 0;
		colorID = %dbn.defColor;
		colorFXID = 0;
		isPlanted = 1;
	};
	%br.setTrusted(1);
	%e = %br.plant();
	if(!%e)
	{
		$DRPG::Resources::Brickgroup.add(%br);
		if(%dbn $= "brickDRPGMoonwellData")
		{
			MoonwellSet.add(%br);
		}
		$DRPG::Resources::TreeCount++;
	} else {
		%br.delete();
	}
}

// Note: Use this but only on PhysicalZoneObjectType typemask, to create fishing skill
function DRPGSeedOre(%pos)
{
	if (%pos $= "")
	{
		%pos = vectorAdd($DRPG::Resources::Center,getRandom(0 - $DRPG::Resources::Radius,$DRPG::Resources::Radius) SPC getRandom(0 - $DRPG::Resources::Radius,$DRPG::Resources::Radius) SPC 100);
	}
	%db[0] = "brickDRPGCopperData";
	%db[40] = "brickDRPGTinData";
	%db[80] = "brickDRPGSilverData";
	%db[100] = "brickDRPGGoldData";
	%db[110] = "brickDRPGIronData";
	%db[135] = "brickDRPGCoalData";
	%db[150] = "brickDRPGDestiniteData";
	%db = getRandom(0,155);
	for(%i=%db;%db>=0;%i--)
	{
		if(%db[%i] !$= "")
		{
			%db = %i;
			break;
		}
	}
	%dbn = %db[%db];
	%rdbn = %dbn;
	%size = %dbn.veinSize;
	%var = %dbn.veinVar;
	%count = %dbn.veinCount;
	%count += getRandom(0 - %var,%var);
	
	for(%i=0;%i<%count;%i++)
	{
		%dbn = %rdbn;
		if(%dbn $= "brickDRPGCopperData" || %dbn $= "brickDRPGTinData")
		{
			%chance = getRandom(0,100);
			if(%chance <= 15)
			{
				%dbn = "brickDRPGDermiteData";
			}
		} else if(%dbn $= "brickDRPGCoalData")
		{
			%chance = getRandom(0,1000) / 10;
			if(%chance < 0.5)
			{
				%dbn = "brickDRPGobsCoalData";
			}
		}
		%rpos = vectorAdd(%pos,getRandom(0 - %size,%size) SPC getRandom(0 - %size,%size) SPC 0);
		%water = dstwaterblock(%rpos, 2);
		if (isObject(%water))
		{
			// don't spawn on water blocks
			continue;
		}
		%ray = containerRayCast(%rpos,vectorAdd(%rpos,"0 0 -1000"),$TypeMasks::FxBrickObjectType);
		%obj = getWord(%ray,0);
		if(isObject(%obj))
		{
			%type = %obj.getType();
			if(%type & $TypeMasks::fxBrickObjectType)
			{
				%odb = %obj.getDatablock();
				if(%odb.brickFile $= "base/data/bricks/special/pineTree.blb" || %odb.brickFile $= "base/data/bricks/rounds/2x2round.blb")
				{
					%i--;
					continue;
				}
			}
			%rpos = vectorAdd(posFromRaycast(%ray),"0 0 0");
			%br = new fxDTSbrick()
			{
				datablock = %dbn;
				position = %rpos;
				angleID = 0;
				colorID = %dbn.defColor;
				colorFXID = (%dbn $= "brickDRPGobsCoalData" || %dbn $= "brickDRPGCoalData" ? 0 : 1);
				isPlanted = 1;
			};
			%br.setTrusted(1);
			%e = %br.plant();
			if(!%e)
			{
				$DRPG::Resources::Brickgroup.add(%br);
				%pl++;
			} else {
				%br.delete();
				%i--;
			}
		}
	}
	if(%pl > 0)
	{
		$DRPG::Resources::OreVeins++;
	}
}

function DRPGSeedHerb(%pos)
{
	if (%pos $= "")
	{
		%pos = vectorAdd($DRPG::Resources::Center,getRandom(0 - $DRPG::Resources::Radius,$DRPG::Resources::Radius) SPC getRandom(0 - $DRPG::Resources::Radius,$DRPG::Resources::Radius) SPC 100);
	}
	%db[0] = "brickDRPGLimberData";
	%db[40] = "brickDRPGRosisData";
	%db[80] = "brickDRPGAccurisData";
	%db[100] = "brickDRPGDeftoraData";
	%db[115] = "brickDRPGDamacData";
	%db[135] = "brickDRPGGundorData";
	%db = getRandom(0,145);
	for(%i=%db;%db>=0;%i--)
	{
		if(%db[%i] !$= "")
		{
			%db = %i;
			break;
		}
	}
	%dbn = %db[%db];
	%rdbn = %dbn;
	%size = %dbn.veinSize;
	%var = %dbn.veinVar;
	%count = %dbn.veinCount;
	%count += getRandom(0 - %var,%var);
	
	for(%i=0;%i<%count;%i++)
	{
		%dbn = %rdbn;
		%rpos = vectorAdd(%pos,getRandom(0 - %size,%size) SPC getRandom(0 - %size,%size) SPC 0);
		%water = dstwaterblock(%rpos, 2);
		if (isObject(%water))
		{
			// don't spawn on water blocks
			continue;
		}
		%ray = containerRayCast(%rpos,vectorAdd(%rpos,"0 0 -1000"),$TypeMasks::FxBrickObjectType);
		%obj = getWord(%ray,0);
		if(isObject(%obj))
		{
			%type = %obj.getType();
			if(%type & $TypeMasks::fxBrickObjectType)
			{
				%odb = %obj.getDatablock();
				if(%odb.brickFile $= "base/data/bricks/special/pineTree.blb" || %odb.brickFile $= "base/data/bricks/rounds/2x2round.blb")
				{
					%i--;
					continue;
				}
			}
			%rpos = vectorAdd(posFromRaycast(%ray),"0 0 0");


			%angleID = (getRandom(0, 100) > 50 ? 1 : 0);

			switch(%angleID)
			{
				case 0:
					%rot = "1 0 0 0";
				case 1:
					%rot = "0 0 1 90";
			}

			%br = new fxDTSbrick()
			{
				datablock = %dbn;
				position = %rpos;
				angleID = %angleID;
				rotation = %rot;
				colorID = %dbn.defColor;
				colorFXID = 0;
				isPlanted = 1;
			};
			%br.setTrusted(1);
			%e = %br.plant();
			if(!%e)
			{
				$DRPG::Resources::Brickgroup.add(%br);
				%pl++;
			} else {
				%br.delete();
				%i--;
			}
		}
	}
	if(%pl > 0)
	{
		$DRPG::Resources::HerbFields++;
	}
}
function DRPGWildManager::seedCrop(%this,%type,%pos)
{
	%center = $DRPG::Resources::Center;
	%rad = $DRPG::Resources::Radius;
	%count = %type.plantCount;
	%crad = %type.plantRadius;
	%count += getRandom(0 - %type.plantVar,%type.plantVar);
	if(%pos $= "")
	{
		%pos = vectorAdd($DRPG::Resources::Center,getRandom(0 - $DRPG::Resources::Radius,$DRPG::Resources::Radius) SPC getRandom(0 - $DRPG::Resources::Radius,$DRPG::Resources::Radius) SPC 100);
	}
	for(%i=0;%i<%count;%i++)
	{
		%rpos = vectorAdd(%pos,getRandom(0 - %crad,%crad) SPC getRandom(0 - %crad,%crad) SPC 0);
		%ray = containerRayCast(%rpos,vectorAdd(%rpos,"0 0 -1000"),$TypeMasks::All);
		%obj = getWord(%ray,0);
		if(isObject(%obj))
		{
			%rpos = posFromRaycast(%ray);
			%crop = new StaticShape()
			{
				dataBlock = %type;
				position = %rpos;
				isGrown = 0;
				owner = %this;
			};
			if(!isObject(%this.crops[%type]))
			{
				%this.crops[%type] = new SimSet();
			}
			%this.crops[%type].add(%crop);
			%crop.setScale(%type.initScale);
			%crop.growCrop(100);
		}
	}
	%this.schedule(60000,"check",%type);
}
function StaticShape::wither(%this)
{
	%this.startFade(250,0,1);
	%this.schedule(250,"delete");
	%crop = %this.getdatablock().getname();
	if(!isObject(DRPGWildManager.crops[%crop]))
	{
		DRPGWildManager.crops[%crop] = new SimSet();
	}
	DRPGWildManager.crops[%crop].remove(%this);
	DRPGWildManager.check(%crop);
}
function DRPGWildManager::onGrown(%this,%crop)
{
	%crop.schedule(60000 * $DRPG::Prefs::WitherTime,"wither");
}
function DRPGWildManager::onHarvest(%this,%crop)
{
	%this.check(%crop);
}
function DRPGWildManager::check(%this,%crop)
{
	if(!isObject(%this.crops[%crop]))
	{
		%this.crops[%crop] = new SimSet();
	}
	if(%this.crops[%crop].getCount() < %crop.threshold)
	{
		%this.seedCrop(%crop);
	}
}