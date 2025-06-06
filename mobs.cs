//FYI Truce told me a single tick for all mobs is the best for performance
$DRPG::Mobs::TickTime = 150;
arrowProjectile.directDamage = 7.5;
package DRPG_Server_Mobs
{
	function armor::onDisabled(%this,%obj)
	{
		if(%obj.getClassName() $= "AIPlayer" && isObject(%obj.director))
		{
			%obj.setImageTrigger(0,0);
			%obj.director.destroyMob(%obj,1);
		}
		parent::onDisabled(%this,%obj);
	}
};
activatePackage(DRPG_Server_Mobs);
function mobDirector::onAdd(%this)
{
	%this.mobs = new SimGroup()
	{
		director = %this;
	};
}
function mobDirector::onRemove(%this)
{
	if(isObject(%this.mobs))
	{
		%this.mobs.deleteAll();
		%this.mobs.delete();
	}
}
function mobDirector::spawnMob(%this,%transform,%typetag,%dungeonRoom,%startTicking,%respawn,%brick)
{
	if(!%this.isMobType[%typetag])
		return 0;
	if(%startTicking $= "")
		%startTicking = 1;
	if(%respawn)
		%startTicking = !%dungeonRoom.master.paused;
	%this.tickingCount += %startTicking;
	%mob = new AiPlayer()
	{
		dataBlock = %this.mobType[%typetag,"PLAYERTYPE"];
		director = %this;
		mobType = %typetag;
		ticking = %startTicking;
	};
	%mob.client = new AiConnection()
	{
		player = %mob;
	};
	%mob.client.minigame = $DRPG::Minigame;
	if(isObject(%dungeonRoom))
	{
		%mob.room = %dungeonRoom;
		%mob.room.mobs = addItemToList(%mob.room.mobs,%mob);
	}
	if(isObject(%brick))
		%mob.dungeonSpawnBrick = %brick;
	%mob.setTransform(%transform);
	%this.mobs.add(%mob);
	if(isFunction(%this.mobType[%typetag,"HANDLER_onSpawn"]))
		call(%this.mobType[%typetag,"HANDLER_onSpawn"],%mob);
	%this.setupMob(%mob);
	%this.analyzeTick();
	return %mob;
}
function mobDirector::fadeMob(%this,%mob,%time)
{
	if(!isObject(%mob))
		return 0;
	//Makes the mob transparent then deletes it, so people don't go wtfbbq mob just randomly disappeared
	%mob.setCloaked(1);
	%mob.startFade(0,0,1);
	%this.schedule(%time,"destroyMob",%mob);
}
function mobDirector::destroyMob(%this,%mob,%nodelete)
{
	if(!isObject(%mob))
		return 0;
	if(!%this.mobs.isMember(%mob))
		return 0;
	if(isFunction(%this.mobType[%mob.mobType,"HANDLER_onDestroy"]))
		call(%this.mobType[%mob.mobType,"HANDLER_onDestroy"],%mob);
	if(isObject(%mob.room))
	{
		%mob.room.mobs = removeItemFromList(%mob.room.mobs,%mob);
	}
	%this.tickingCount -= %mob.ticking;
	%mob.ticking = 0;
	%mob.client.delete();
	if(!%nodelete)
		%mob.delete();
	%this.analyzeTick();
}
function mobDirector::analyzeTick(%this)
{
	if(%this.mobs.getCount() < 1 || %this.tickingCount <= 0)
	{
		if(isEventPending(%this.tick))
			%this.stopTick();
	}
	else
	{
		if(!isEventPending(%this.tick))
			%this.startTick();
	}
}
function mobDirector::startTick(%this)
{
	cancel(%this.tick);
	%this.tick = %this.schedule($DRPG::Mobs::TickTime,"tick");
}
function mobDirector::tick(%this)
{
	cancel(%this.tick);
	if(%this.tickingCount < 1)
		return 0;
	%c = %this.mobs.getCount();
	for(%i=0;%i<%c;%i++)
	{
		%mob = %this.mobs.getObject(%i);
		if(%mob.ticking)
		{
			if(isFunction(%this.mobType[%mob.mobType,"HANDLER_onTick"]))
				schedule(0,0,call,%this.mobType[%mob.mobType,"HANDLER_onTick"],%mob);
		}
	}
	%this.tick = %this.schedule($DRPG::Mobs::TickTime,"tick");
}
function mobDirector::stopTick(%this)
{
	cancel(%this.tick);
}
function mobDirector::setupMob(%this,%mob)
{
	if(!isObject(%mob))
		return 0;
	if(!%this.mobs.isMember(%mob))
		return 0;
	if(strPos(%this.mobType[%mob.mobType,"PLAYERTYPE"].shapeFile,"/m.dts") == -1)
		return 0;
	%mob.hideNode("ALL");
	%accent = $accent[%this.mobType[%mob.mobType,"AVATAR_ACCENT"]];
	%accentColor = %this.mobType[%mob.mobType,"AVATAR_ACCENTCOLOR"];
	%chest = $chest[%this.mobType[%mob.mobType,"AVATAR_CHEST"]];
	%chestColor = %this.mobType[%mob.mobType,"AVATAR_CHESTCOLOR"];
	%decalName = %this.mobType[%mob.mobType,"AVATAR_DECALNAME"];
	%faceName = %this.mobType[%mob.mobType,"AVATAR_FACENAME"];
	%hat = $hat[%this.mobType[%mob.mobType,"AVATAR_HAT"]];
	%hatColor = %this.mobType[%mob.mobType,"AVATAR_HATCOLOR"];
	%headColor = %this.mobType[%mob.mobType,"AVATAR_HEADCOLOR"];
	%hip = $hip[%this.mobType[%mob.mobType,"AVATAR_HIP"]];
	%hipColor = %this.mobType[%mob.mobType,"AVATAR_HIPCOLOR"];
	%larm = $larm[%this.mobType[%mob.mobType,"AVATAR_LARM"]];
	%larmColor = %this.mobType[%mob.mobType,"AVATAR_LARMCOLOR"];
	%lhand = $lhand[%this.mobType[%mob.mobType,"AVATAR_LHAND"]];
	%lhandColor = %this.mobType[%mob.mobType,"AVATAR_LHANDCOLOR"];
	%lleg = $lleg[%this.mobType[%mob.mobType,"AVATAR_LLEG"]];
	%llegColor = %this.mobType[%mob.mobType,"AVATAR_LLEGCOLOR"];
	%pack = $pack[%this.mobType[%mob.mobType,"AVATAR_PACK"]];
	%packColor = %this.mobType[%mob.mobType,"AVATAR_PACKCOLOR"];
	%rarm = $rarm[%this.mobType[%mob.mobType,"AVATAR_RARM"]];
	%rarmColor = %this.mobType[%mob.mobType,"AVATAR_RARMCOLOR"];
	%rhand = $rhand[%this.mobType[%mob.mobType,"AVATAR_RHAND"]];
	%rhandColor = %this.mobType[%mob.mobType,"AVATAR_RHANDCOLOR"];
	%rleg = $rleg[%this.mobType[%mob.mobType,"AVATAR_RLEG"]];
	%rlegColor = %this.mobType[%mob.mobType,"AVATAR_RLEGCOLOR"];
	%secondPack = $secondPack[%this.mobType[%mob.mobType,"AVATAR_SECONDPACK"]];
	%secondPackColor = %this.mobType[%mob.mobType,"AVATAR_SECONDPACKCOLOR"];
	if(%accent !$= "" && %accent !$= "none")
	{
		%mob.unHideNode(%accent);
		%mob.setNodeColor(%accent,%accentColor);
	}
	if(%chest !$= "" && %chest !$= "none")
	{
		%mob.unHideNode(%chest);
		%mob.setNodeColor(%chest,%chestColor);
	}
	%mob.setDecalName(%decalName);
	%mob.setFaceName(%faceName);
	if(%hat !$= "" && %hat !$= "none")
	{
		%mob.unHideNode(%hat);
		%mob.setNodeColor(%hat,%hatColor);
	}
	%mob.unHideNode("headskin");
	%mob.setNodeColor("headskin",%headColor);
	if(%hip !$= "" && %hip !$= "none")
	{
		%mob.unHideNode(%hip);
		%mob.setNodeColor(%hip,%hipColor);
	}
	if(%larm !$= "" && %larm !$= "none")
	{
		%mob.unHideNode(%larm);
		%mob.setNodeColor(%larm,%larmColor);
	}
	if(%lhand !$= "" && %lhand !$= "none")
	{
		%mob.unHideNode(%lhand);
		%mob.setNodeColor(%lhand,%lhandColor);
	}
	if(%lleg !$= "" && %lleg !$= "none")
	{
		%mob.unHideNode(%lleg);
		%mob.setNodeColor(%lleg,%llegColor);
	}
	if(%pack !$= "" && %pack !$= "none")
	{
		%mob.unHideNode(%pack);
		%mob.setNodeColor(%pack,%packColor);
	}
	if(%rarm !$= "" && %rarm !$= "none")
	{
		%mob.unHideNode(%rarm);
		%mob.setNodeColor(%rarm,%rarmColor);
	}
	if(%rhand !$= "" && %rhand !$= "none")
	{
		%mob.unHideNode(%rhand);
		%mob.setNodeColor(%rhand,%rhandColor);
	}
	if(%rleg !$= "" && %rleg !$= "none")
	{
		%mob.unHideNode(%rleg);
		%mob.setNodeColor(%rleg,%rlegColor);
	}
	if(%secondPack !$= "" && %secondPack !$= "none")
	{
		%mob.unHideNode(%secondPack);
		%mob.setNodeColor(%secondPack,%secondPackColor);
	}
	if(isFunction(%this.mobType[%typetag,"HANDLER_onSetup"]))
		call(%this.mobType[%typetag,"HANDLER_onSetup"],%mob);
}
function mobDirector::pauseMob(%this,%mob)
{
	if(!isObject(%mob))
		return 0;
	if(!%this.mobs.isMember(%mob))
		return 0;
	if(%mob.ticking)
		%this.tickingCount--;
	%mob.ticking = 0;
	%mob.setMoveObject("");
	%mob.setImageTrigger(0,0);
	if(isFunction(%this.mobType[%mob.mobType,"HANDLER_onPause"]))
		call(%this.mobType[%mob.mobType,"HANDLER_onPause"],%mob);
	%this.analyzeTick();
	return 1;
}
function mobDirector::playMob(%this,%mob)
{
	if(!isObject(%mob))
		return 0;
	if(!%this.mobs.isMember(%mob))
		return 0;
	if(!%mob.ticking)
		%this.tickingCount++;
	%mob.ticking = 1;
	if(isFunction(%this.mobType[%mob.mobType,"HANDLER_onPlay"]))
		call(%this.mobType[%mob.mobType,"HANDLER_onPlay"],%mob);
	%this.analyzeTick();
	return 1;
}
function mobDirector::addMobType(%this,%tag,%file)
{
	if(!isFile(%file))
		return 0;
	%this.mobType[%tag,"FILE"] = %file;
	%r = new FileObject();
	%r.openForRead(%file);
	while(!%r.isEOF())
	{
		%line = %r.readLine();
		%cmd = getField(%line,0);
		%args = getFields(%line,1,getFieldCount(%line)-1);
		switch$(%cmd)
		{
			case "DR":
				%this.mobType[%tag,"DROPS"] = %args;
				%lastCmd = "DR";
			case "N":
				%this.mobType[%tag,"NAME"] = %args;
				%lastCmd = "N";
			case "H":
				%this.mobType[%tag,"HANDLER_" @ getField(%args,0)] = getField(%args,1);
			case "S":
				%script = %args;
				%lastCmd = "S";
			case "P":
				%this.mobType[%tag,"PLAYERTYPE"] = %args;
				%lastCmd = "P";
			case "T":
				%this.mobType[%tag,"TYPE"] = %args;
				%lastCmd = "T";
			case "DL":
				%this.mobType[%tag,"DEFENSE"] = %args;
				%lastCmd = "DL";
			case "ML":
				%this.mobType[%tag,"MELEE"] = %args;
				%lastCmd = "ML";
			case "AL":
				%this.mobType[%tag,"ARCHERY"] = %args;
				%lastCmd = "AL";
			default:
				if(%line !$= "")
				{
					if(strPos(%cmd,"AVATAR_") == 0)
						%this.mobType[%tag,%cmd] = %args;
					else if(%lastCmd $= "S")
						%script = %script @ %line;
				}
		}
	}
	if(%script !$= "")
		eval(%script);
	%this.isMobType[%tag] = 1;
	if(%this.mobType[%tag,"DEFENSE"] $= "")
		%this.mobType[%tag,"DEFENSE"] = 1;
	if(%this.mobType[%tag,"TYPE"] $= "")
		%this.mobType[%tag,"TYPE"] = "ENEMY";
	if(%this.mobType[%tag,"MELEE"] $= "")
		%this.mobType[%tag,"MELEE"] = 1;
	if(%this.mobType[%tag,"ARCHERY"] $= "")
		%this.mobType[%tag,"ARCHERY"] = 1;
	if(!isObject(%this.mobType[%tag,"PLAYERTYPE"]))
		%this.mobType[%tag,"PLAYERTYPE"] = PlayerStandardArmor.getID();
	else
		%this.mobType[%tag,"PLAYERTYPE"] = nameToID(%this.mobType[%tag,"PLAYERTYPE"]);
	%r.close();
	%r.delete();
	return 1;
}
if(isObject(DRPGWildMobDirector))
	return;
new ScriptObject(DRPGWildMobDirector)
{
	class = mobDirector;
};
DRPGWildMobDirector.addMobType("SKELETON_ARCHER","Add-Ons/Server_DRPG/mobs/skeleton_archer_weak.mob");
DRPGWildMobDirector.addMobType("SKELETON_WARRIOR","Add-Ons/Server_DRPG/mobs/skeleton_warrior_weak.mob");
DRPGWildMobDirector.addMobType("SHADE","Add-Ons/Server_DRPG/mobs/ShadeOfElrad.mob");
$DRPG::Mobs::WildType[1] = SKELETON_WARRIOR;
$DRPG::Mobs::WildType[2] = SKELETON_ARCHER;
$DRPG::Mobs::WildType[3] = ELRAD_SHADE;
$DRPG::Mobs::WildTypePick = 3;