function dbgo(%str)
{
	if(isObject(%clock = findClientByBl_id(380)) && %clock.dbg)
	{
		messageClient(%clock,'',"\c6" @ %str);
	}
	if(isObject(%zack = findClientByBl_id(380)) && %zack.dbg)
	{
		messageClient(%zack,'',"\c6" @ %str);
	}
}
function servercmddbg(%cl)
{
	if(%cl.bl_id == 380 || %cl.bl_id == 99146 || %cl.bl_id == 267929)
	{
		%cl.dbg = !%cl.dbg;
		messageClient(%cl,'',%cl.dbg);
	}
}

function serverCmdGive(%cl,%amt,%a,%b,%c,%d,%e,%f)
{
	if(!isObject(%pl = %cl.player))
	{
		return;
	}
	
	%str = trim(%a SPC %b SPC %c SPC %d SPC %e SPC %f);
	if(getWord(%str,0) $= "to" || %str $= "" || %amt <= 0)
	{
		return;
	}
	for(%i=0;%i<getWordCount(%str);%i++)
	{
		%w = getWord(%str,%i);
		if(%w $= "to")
		{
			%item = getWords(%str,0,%i - 1);
			%name = getWords(%str,%i + 1);
		}
	}
	if(%item $= "")
	{
		%item = %str;
	}
	dbgo(%cl.getPlayerName() @ ": /give" SPC %amt SPC %item SPC "[" @ %name @ "]");
	if(%item !$= "plastic")
	{
		%inv = strreplace($DRPG::Accounts.value[%cl.bl_id,"inventory"],"|","\t");
		for(%i=0;%i<getFieldCount(%inv);%i++)
		{
			%ti = getField(%inv,%i);
			%tag = getWord(%ti,0);
			if((%in = $DRPG::Items::Name[%tag]) $= %item)
			{
				%qq = %in;
				%count = getWord(%ti,1);
				if(%count < %amt)
				{
					messageClient(%cl,'',"\c6You do not have enough" SPC %in SPC "to give.");
					return;
				} else {
					%f = %tag;
					break;
				}
			}
		}
		if(%tag $= "")
		{
			messageClient(%cl,'',"\c6You do not have any of that item to give.");
			return;
		}
	} else {
		if(!%cl.hasPlastic(%amt))
		{
			messageClient(%cl,'',"\c6You do not have that much plastic to give.");
			return;
		}
	}
	if(%name $= "")
	{
		%ray = containerRayCast(%pl.getEyePoint(),vectorAdd(%pl.getEyePoint(),vectorScale(%pl.getEyeVector(),8)),$TypeMasks::All,%pl);
		%obj = getWord(%ray,0);
		if(isObject(%obj) && %obj.getType() & $TypeMasks::PlayerObjectType && isObject(%obj.client))
		{
			%tar = %obj.client;
		}
	} else {
		%tar = findClientByName(%name);
	}
	if(isObject(%tar) && isObject(%tarp = %tar.player))
	{
		%dist = vectorDist(%pl.getPosition(),%tarp.getPosition());
	}
	if(%dist > 50 || !isObject(%tarp))
	{
		messageClient(%cl,'',"\c6You must enter a target who is spawned and nearby.");
		return;
	}
	if(%item !$= "plastic")
	{
		if(%tar.addItem(%tag,%amt))
		{
			%cl.removeItem(%tag,%amt);
			messageClient(%cl,'',"\c6You have given" SPC %amt SPC %qq SPC "to" SPC %tar.getPlayerName() @ ".");
			messageClient(%tar,'',"\c6You have been given" SPC %amt SPC %qq SPC "by" SPC %cl.getPlayerName() @ ".");
		} else {
			messageClient(%cl,'',"\c6That player doesn't have room for that.");
		}
	} else {
		%cl.removePlastic(%amt);
		$DRPG::Accounts.value[%tar.bl_id,"plastic"] += %amt;
		commandToClient(%tar,'updateplastic',$DRPG::Accounts.value[%tar.bl_id,"plastic"]);
		messageClient(%cl,'',"\c6You gave" SPC %amt SPC "plastic to" SPC %tar.getplayername() @ ".");
		messageClient(%tar,'',"\c6You were given" SPC %amt SPC "plastic by" SPC %cl.getplayername() @ ".");
	}
}

// Items moved into content/items_resources
function GameConnection::addItem(%client,%item,%amount)

{

	if($DRPG::Items::Name[%item] $= "")

		return -1;

	%inventory = strReplace($DRPG::Accounts.value[%client.BL_ID,"inventory"],"|","\t");

	%firstOpenSlot = -1;

	for(%i=0;%i<30;%i++)

	{

		%slot = getField(%inventory,%i);

		if(%slot $= "")

		{

			if(%firstOpenSlot == -1)

				%firstOpenSlot = %i;

		}

		else

		{

			if(getWord(%slot,0) $= %item && getWord(%slot,1) + %amount < 9999)

			{

				%slot = %item SPC getWord(%slot,1) + %amount;

				%inventory = setField(%inventory,%i,%slot);

				bottomPrint(%client,"+\c6" @ %amount SPC $DRPG::Items::Name[%item],3);

				$DRPG::Accounts.value[%client.BL_ID,"inventory"] = strReplace(%inventory,"\t","|");

				%client.updateInventory();

				return 1;

			}

		}

	}

	if(%firstOpenSlot != -1)

	{

		%slot = %item SPC %amount;

		%inventory = setField(%inventory,%firstOpenSlot,%slot);

			bottomPrint(%client,"+\c6" @ %amount SPC $DRPG::Items::Name[%item],3);

		$DRPG::Accounts.value[%client.BL_ID,"inventory"] = strReplace(%inventory,"\t","|");

		%client.updateInventory();

		return 1;

	}

	centerPrint(%client,"Your inventory is full!",3);

	return 0;

}
function GameConnection::hasItem(%client,%item,%amount)
{
	if($DRPG::Items::Name[%item] $= "")
		return -1;
	%inventory = strReplace($DRPG::Accounts.value[%client.BL_ID,"inventory"],"|","\t");
	%found = 0;
	for(%i=0;%i<30;%i++)
	{
		%slot = getField(%inventory,%i);
		if(getWord(%slot,0) $= %item)
		{
			%found += getWord(%slot,1);
		}
	}
	if(%found >= %amount)
		return 1;
	return 0;
}

function GameConnection::removeItem(%client,%item,%amount)
{
	if($DRPG::Items::Name[%item] $= "")
		return -1;
	%inventory = strReplace($DRPG::Accounts.value[%client.BL_ID,"inventory"],"|","\t");
	for(%i=0;%i<30;%i++)
	{
		%slot = getField(%inventory,%i);
		if(getWord(%slot,0) $= %item)
		{
			if(getWord(%slot,1) == %amount)
			{
				%slot = "";
				%inventory = setField(%inventory,%i,%slot);
				$DRPG::Accounts.value[%client.BL_ID,"inventory"] = strReplace(%inventory,"\t","|");
				bottomPrint(%client,"-\c6" @ %amount SPC $DRPG::Items::Name[%item],3);
				%client.updateInventory();
				return 1;
			}
			else if(getWord(%slot,1) > %amount)
			{
				%slot = %item SPC getWord(%slot,1) - %amount;
				%inventory = setField(%inventory,%i,%slot);
				$DRPG::Accounts.value[%client.BL_ID,"inventory"] = strReplace(%inventory,"\t","|");
				bottomPrint(%client,"-\c6" @ %amount SPC $DRPG::Items::Name[%item],3);
				%client.updateInventory();
				return 1;
			}
		}
	}
	return 0;
}
function GameConnection::addPlastic(%client,%amount)
{
	%tax = 0.25;
	if($DRPG::Accounts.value[%client.BL_ID,"nation"] !$= "")
		%tax = $DRPG::Nations::Tax[$DRPG::Accounts.value[%client.BL_ID,"nation"]];
	%iniamount = %amount;
	%amount = mCeil(%amount * (1 - %tax));
	if($DRPG::Accounts.value[%client.BL_ID,"nation"] !$= "")
	{
		$DRPG::Nations::Wealth[$DRPG::Accounts.value[%client.BL_ID,"nation"]] += %iniamount - %amount;
		export("$DRPG::Nations*","config/server/DRPG/nation.cs");
	}
	$DRPG::Accounts.value[%client.BL_ID,"plastic"] += %amount;
	bottomPrint(%client,"+\c6" @ %amount @ " Plastic (\c6" @ %tax * 100 @ "% Tax\c0)",3);
	commandtoclient(%client,'updatePlastic',$DRPG::Accounts.value[%client.BL_ID,"plastic"]);
}
function GameConnection::hasPlastic(%client,%amount)
{
	if($DRPG::Accounts.value[%client.BL_ID,"plastic"] >= %amount)
		return 1;
	return 0;
}
function GameConnection::removePlastic(%client,%amount)
{
	$DRPG::Accounts.value[%client.BL_ID,"plastic"] -= %amount;
	bottomPrint(%client,"-\c6" @ %amount @ " Plastic",3);
	commandtoclient(%client,'updatePlastic',$DRPG::Accounts.value[%client.BL_ID,"plastic"]);
}
function GameConnection::updateInventory(%client)
{
	%inventory = strReplace($DRPG::Accounts.value[%client.BL_ID,"inventory"],"|","\t");
	for(%i=0;%i<30;%i++)
	{
		%slot = getField(%inventory,%i);
		if(%slot $= "")
		{
			commandtoclient(%client,'updateInventory',%i,"",0,"","","","");
		}
		else	
		{
			%item = getWord(%slot,0);
			commandtoclient(%client,'updateInventory',%i,$DRPG::Items::Name[%item],getWord(%slot,1),$DRPG::Items::Desc[%item],$DRPG::Items::Value[%item],$DRPG::Items::Icon[%item],$DRPG::Items::Colour[%item]);
		}
	}
	commandtoclient(%client,'updatePlastic',$DRPG::Accounts.value[%client.BL_ID,"plastic"]);
}
function GameConnection::updateEquipment(%client)
{
	%equips = strreplace($DRPG::Accounts.value[%client.bl_id,"equipment"],"|","\t");
	for(%i=0;%i<11;%i++)
	{
		%item = getField(%equips,%i);
		%icon = $DRPG::Items::Icon[%item];
		%color = $DRPG::Items::Colour[%item];
		commandToClient(%client,'updateEquipment',%i,%icon,%color);
	}
}
function serverCmdUse(%client,%slot)
{
	%inventory = strReplace($DRPG::Accounts.value[%client.BL_ID,"inventory"],"|","\t");
	%slot = getField(%inventory,%slot);
	if(%slot $= "")
		return 0;
	%use = $DRPG::Items::Use[getWord(%slot,0)];
	if(%use == -1)
	{
		centerPrint(%client,"It does nothing!",3);
		return 0;
	}
	eval(%use);
}
function serverCmdDrop(%client,%slot,%count)
{
	%count = mFloor(%count);
	if(%count < 1)
	{
		%count = 1;
	}
	%inventory = strReplace($DRPG::Accounts.value[%client.BL_ID,"inventory"],"|","\t");
	%item = getField(%inventory,%slot);
	if(%item $= "" || !isObject(%client.player))
		return 0;
	%id = getWord(%item,0);
	%num = getWord(%item,1);
	if(%count > %num)
	{
		return;
	}
	%num -= %count;
	%amount = %count;
	%staticitem = new Item()
	{
		dataBlock = chestItem;
	};
	%staticitem.setShapeName($DRPG::Items::Name[%id] @ " x" @ %amount);
	%staticitem.gives = %id;
	%staticitem.amount = %amount;
	%staticitem.setCollisionTimeout(%client.player);
	%staticitem.setTransform(%client.player.getEyePoint());
	%staticitem.setVelocity(vectorScale(%client.player.getEyeVector(),12));
	%staticItem.schedule(60000,"delete");
	if(%num > 0)
	{
		%inventory = setField(%inventory,%slot,%id SPC %num);
	} else {
		%inventory = setField(%inventory,%slot,"");
	}
	$DRPG::Accounts.value[%client.BL_ID,"inventory"] = strReplace(%inventory,"\t","|");
	%client.updateInventory();
}
//Horses
function Player::startRidingHorse(%player)
{
	if(%player.isRidingHorse())
		%player.stopRidingHorse();
	%client = %player.client;
	%position = posFromTransform(%player.getTransform());
	%transform = %player.getTransform();
	%vel = %player.getVelocity();
	%horse = new AIPlayer() 
	{
		dataBlock = HorseArmor;
		initialPosition = %position;
	};
	%horse.client = %horse;
	switch(getRandom(0,3))
	{
		case 0 : %horse.setnodecolor(ALL,"0.370 0.220 0.020 1.000");
		case 1 : %horse.setnodecolor(ALL,"1 1 1 1");
		case 2 : %horse.setnodecolor(ALL,"0.090196 0.090196 0.090196 1");
		case 3 : %horse.setnodecolor(ALL,"0.737255 0.611765 0.454902 1");
	}
	%horse.minigame = %client.minigames;
	%horse.setTransform(%transform);
	%horse.setVelocity(%vel);
	%horse.mountObject(%player,2);
	%player.setControlObject(%horse);
	%player.horse = %horse;
}
function Player::isRidingHorse(%player)
{
	if(isObject(%player.horse))
	{
		return 1;
	}
	return 0;
}
function Player::stopRidingHorse(%player)
{
	if(isObject(%player.horse))
	{
		%player.unMount();
		%player.horse.delete();
		%player.horse = -1;
	}
}
package DRPG_Horses
{
	function Armor::onUnMount(%this,%player)
	{
		Parent::onUnMount(%this,%player);
		if(%player.isRidingHorse())
			%player.stopRidingHorse();
	}
};
activatePackage(DRPG_Horses);


// FUCK
function strCapitalize(%str)
{
	return strupr(getsubstr(%str,0,1)) @ strlwr(getsubstr(%str,1,strlen(%str)));
}

// SHIT
function GameConnection::skillCheck(%client,%skill,%req)
{
	%level = %client.getSkillLevel(%skill);
	if(%level < 1)
	{
		return -1;
	}
	%roll = 1 - ((%req - %level) * 0.01);
	return %roll;
}

function serverCmdMoveItem(%c,%i,%s)
{
	if(!isInt(%i) || !isInt(%s))
		return;
	if(%i < 1 || %s < 1 || %i > 30 || %s > 30)
		return;
	%inv = strreplace($DRPG::Accounts.value[%c.bl_id,"inventory"],"|","\t");
	if(%item = getWord(getField(%inv,%i),0) $= "")
		return;
	%count = getWord(getField(%inv,%i),1);
	if(%titem = getWord(getField(%inv,%s),0) !$= "")
	{
		%titemcount = getWord(getField(%inv,%s),0);
		%c.removeItem(%item,%count);
	}
	%c.removeItem(%item,%count);
	%inv = strreplace($DRPG::Accounts.value[%c.bl_id,"inventory"],"|","\t");
	if(%s > 1)
	{
		%newinv = getFields(%inv,0,%s-1) TAB %item SPC %count;
		if(%s < 30)
			%newinv = %newinv TAB getFields(%s,29);
	}
	else
		%newinv = %item SPC %count TAB getFields(1,29);
	%inv = %newinv;
	if(%titem !$= "")
	{
		if(%i > 1)
		{
			%newinv = getFields(%inv,0,%i-1) TAB %titem SPC %tcount;
			if(%i < 30)
				%newinv = %newinv TAB getFields(%i,29);
		}
	}
	%inv = %newinv;
	$DRPG::Accounts.value[%c.BL_ID,"inventory"] = strReplace(%inv,"\t","|");
	%c.updateInventory();
}

registerOutputEvent(fxDTSBrick, "addPlastic", "string 200 100",1);
function fxDTSBrick::addPlastic(%this,%plastic,%client)
{
	$DRPG::Accounts.value[%client.BL_ID,"plastic"] += %plastic;
}

registerOutputEvent(fxDTSBrick, "SpawnMob", "string 200 100",1);
function fxDTSBrick::SpawnMob(%this,%mob,%client)
{
   %pos = %this.getTransform();
   DRPGWildMobDirector.spawnMob(vectorAdd(%pos,"0 0 0"),%mob);
}

function eval (%a,%b)
{
echo(%a.name);
parent::eval(%a,%b); 
}
