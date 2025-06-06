forceRequiredAddon("Player_No_Jet");
datablock PlayerData(PlayerSlow : PlayerNoJet)
{
	runForce = 4320*0.8;
	maxForwardSpeed = 7*0.8;
	maxBackwardSpeed = 4*0.8;
	maxSideSpeed = 6*0.8;
	maxUnderwaterForwardSpeed = 8.4*0.8;
	maxUnderwaterBackwardSpeed = 7.8*0.8;
	maxUnderwaterSideSpeed = 7.8*0.8;
	maxForwardCrouchSpeed = 3*0.8;
	maxBackwardCrouchSpeed = 2*0.8;
	maxSideCrouchSpeed = 2*0.8;
};
function serverCmdUseWrench(%client)
{
	if(%client.isSuperAdmin && isObject(%client.player))
	{
		%client.player.mountImage(wrenchImage,0);
	}
}
registerOutputEvent(GameConnection,"addItem","string 175 255\tint 1 999999 1");
registerOutputEvent(GameConnection,"removeItem","string 175 255\tint 1 999999 1");
function gameConnection::addItems(%client,%items,%amount)
{
	%items = strReplace(%items,"|","\t");
	%itemsc = getFieldCount(%items);
	if(%itemsc > 1)
	{
		for(%i=0;%i<%itemsc;%i++)
			%client.addItem(getField(%items,%i),%amount);
	}
	else
		%client.addItem(%items,%amount);
}
function gameConnection::removeItems(%client,%items,%amount)
{
	%items = strReplace(%items,"|","\t");
	%itemsc = getFieldCount(%items);
	if(%itemsc > 1)
	{
		for(%i=0;%i<%itemsc;%i++)
			%client.removeItem(getField(%items,%i),%amount);
	}
	else
		%client.removeItem(%items,%amount);
}
function saveDRPGAccounts()
{
	export("$DRPG::Nations*","config/server/DRPG/nation.cs");
	$DRPG::Accounts.exportDatabase($DRPG::Accounts.dataBaseFile);
	return getFieldCount($DRPG::Accounts.keyList);
}
function saveDRPGBricks(%events,%ownership)
{
	%path = "config/server/temp/temp.bls";
	if(!isWriteableFileName(%path))
	{
		return;
	}
	%file = new FileObject();
	%file.openForWrite(%path);
	%file.writeLine("This is a Blockland save file.  You probably shouldn't modify it cause you'll screw it up.");
	%file.writeLine("1"); // What does this mean?
	%file.writeLine(%desc);
	for(%i=0;%i<64;%i++)
		%file.writeLine(getColorIDTable(%i));
	%bricks = 0;
	for(%i=0;%i<mainBrickGroup.getCount();%i++)
		%bricks += mainBrickGroup.getObject(%i).getCount();
	%file.writeLine("Linecount " @ %bricks);
	for(%d=0;%d<2;%d++)
	{
		for(%i=0;%i<mainBrickGroup.getCount();%i++)
		{
			%group = mainBrickGroup.getObject(%i);
			for(%a=0;%a<%group.getCount();%a++)
			{
				%brick = %group.getObject(%a);
				if(!(%d ^ %brick.isBasePlate()))
					continue;
				if(%brick.getDataBlock().hasPrint)
				{
					%texture = getPrintTexture(%brick.getPrintId());
					%path = filePath(%texture);
					%underscorePos = strPos(%path, "_");
					%name = getSubStr(%path, %underscorePos + 1, strPos(%path, "_", 14) - 14) @ "/" @ fileBase(%texture);
					if($printNameTable[%name] !$= "")
					{
						%print = %name;
					}
				}
				%file.writeLine(%brick.getDataBlock().uiName @ "\" " @ %brick.getPosition() SPC %brick.getAngleID() SPC %brick.isBasePlate() SPC %brick.getColorID() SPC %print SPC %brick.getColorFXID() SPC %brick.getShapeFXID() SPC %brick.isRayCasting() SPC %brick.isColliding() SPC %brick.isRendering());
				if(%ownership && %brick.isBasePlate() && !$Server::LAN)
					%file.writeLine("+-OWNER " @ getBrickGroupFromObject(%brick).bl_id);
				if(%events)
				{
					if(%brick.getName() !$= "")
						%file.writeLine("+-NTOBJECTNAME " @ %brick.getName());
					for(%b=0;%b<%brick.numEvents;%b++)
					{
						%targetClass = %brick.eventTargetIdx[%b] >= 0 ? getWord(getField($InputEvent_TargetListfxDTSBrick_[%brick.eventInputIdx[%b]], %brick.eventTargetIdx[%b]), 1) : "fxDtsBrick";
						%paramList = $OutputEvent_parameterList[%targetClass, %brick.eventOutputIdx[%b]];
						%params = "";
						for(%c=0;%c<4;%c++)
						{
							if(firstWord(getField(%paramList, %c)) $= "dataBlock" && isObject(%brick.eventOutputParameter[%b, %c + 1]))
								%params = %params TAB %brick.eventOutputParameter[%b, %c + 1];
							else
								%params = %params TAB %brick.eventOutputParameter[%b, %c + 1];
						}
						%file.writeLine("+-EVENT" TAB %b TAB %brick.eventEnabled[%b] TAB %brick.eventInput[%b] TAB %brick.eventDelay[%b] TAB %brick.eventTarget[%b] TAB %brick.eventNT[%b] TAB %brick.eventOutput[%b] @ %params);
					}
				}
				if(isObject(%brick.emitter))
					%file.writeLine("+-EMITTER " @ %brick.emitter.emitter.uiName @ "\" " @ %brick.emitterDirection);
				if(%brick.getLightID() >= 0)
					%file.writeLine("+-LIGHT " @ %brick.getLightID().getDataBlock().uiName @ "\" "); // Not sure if something else comes after the name
				if(isObject(%brick.item))
					%file.writeLine("+-ITEM " @ %brick.item.getDataBlock().uiName @ "\" " @ %brick.itemPosition SPC %brick.itemDirection SPC %brick.itemRespawnTime);
				if(isObject(%brick.audioEmitter))
					%file.writeLine("+-AUDIOEMITTER " @ %brick.audioEmitter.getProfileID().uiName @ "\" "); // Not sure if something else comes after the name
				if(isObject(%brick.vehicleSpawnMarker))
					%file.writeLine("+-VEHICLE " @ %brick.vehicleSpawnMarker.uiName @ "\" " @ %brick.reColorVehicle);
			}
		}
	}
	%file.close();
	%file.delete();
	fileCopy("config/server/temp/temp.bls","base/server/temp/temp.bls");
	return %bricks;
}
if(!isObject($DRPG::Minigame))
{
	$DRPG::Minigame = new ScriptObject()
	{
		botDamage = 1;
		brickDamage = 1;
		brickRespawnTime = 10000;
		class = miniGameSO;
		colorIdx = 0;
		EnableBuilding = 1;
		EnablePainting = 1;
		enableWand = 1;
		fallingDamage = 1;
		inviteOnly = 0;
		numMembers = 1;
		playerDataBlock = PlayerNoJet;
		PlayersUseOwnBrick = 0;
		Points_BreakBrick = 0;
		Points_Die = 0;
		Points_KillPlayer = 0;
		Points_KillSelf = 0;
		Points_PlantBrick = 0;
		respawnTime = 1000;
		selfDamage = 1;
		StartEquip0 = 0;
		StartEquip1 = 0;
		StartEquip2 = 0;
		StartEquip3 = 0;	
		StartEquip4 = 0;
		title = "DRPG Minigame";
		useAllPlayersBricks= 1;
		useSpawnBricks = 1;
		VehicleDamage = 0;
		vehicleReSpawnTime = 5;
		weaponDamage = 1;
	};
}

function ServerCmdSpawnSkele(%cl)
{
if(!isObject(%cl.player)||!%cl.isSuperAdmin)
return;

%pl = %cl.player;
%eye = vectorScale(%pl.getEyeVector(), 300);
%pos = %pl.getEyePoint();
%mask = $TypeMasks::FxBrickObjectType;
%raycast = containerRaycast(%pos, vectorAdd(%pos, %eye), %mask, %pl);
%hit = firstWord(%raycast);

if(!isObject(%hit))
return;

%pos = posFromRaycast(%raycast);
DRPGWildMobDirector.spawnMob(%pos,$DRPG::Mobs::WildType[1]);


}


function Spawnskele(%cl)
{

%pl = %cl.player;
%eye = vectorScale(%pl.getEyeVector(), 300);
%pos = %pl.getEyePoint();
%mask = $TypeMasks::FxBrickObjectType;
%raycast = containerRaycast(%pos, vectorAdd(%pos, %eye), %mask, %pl);
%hit = firstWord(%raycast);

if(!isObject(%hit))
return;

%pos = posFromRaycast(%raycast);
DRPGWildMobDirector.spawnMob(%pos,$DRPG::Mobs::WildType[2]);

}


