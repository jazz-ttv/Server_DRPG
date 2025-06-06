%e = forceRequiredAddOn("Brick_Large_Cubes");
if(%e == $Error::AddOn_NotFound)
	return;
if(!isObject(brickBattlefieldZoneData))
{
	datablock fxDTSBrickData(brickBattlefieldZoneData : brick64xCubeData)
	{
		category = "DRPG";
		subCategory = "Zones";
		uiName = "Battlefield 64x";
		battleField = 1;
		isWaterBrick = true;
		zone = DRPGBattlefieldTriggerData;
	};
}
if(!isObject(brickSafeZoneData))
{
	datablock fxDTSBrickData(brickSafeZoneData : brick64xCubeData)
	{
		category = "DRPG";
		subCategory = "Zones";
		uiName = "Safe 64x";
		safeField = 1;
		isWaterBrick = true;
		zone = DRPGSafeTriggerData;
	};
}
function fxDtsBrick::setupDRPGZone(%brick)
{
	%db = %brick.getDatablock();
	if(isObject(%db.zone))
	{
		%t = new Trigger()
		{
			dataBlock = %db.zone;
      			polyhedron = "0 0 0 1 0 0 0 -1 0 0 0 1";
		};
		missionCleanup.add(%t);
		%brick.drpgZone = %t;
		%t.spawnBrick = %brick;
		%boxMin = getWords(%brick.getWorldBox(),0,2);
		%boxMax = getWords(%brick.getWorldBox(),3,5);
		%boxDiff = vectorSub(%boxMax,%boxMin);
		%boxDiff = vectorAdd(%boxDiff,"0 0 0.1"); 
		%t.setScale(%boxDiff);
		%posA = %brick.getWorldBoxCenter();
		%posB = %t.getWorldBoxCenter();
		%posDiff = vectorSub(%posA,%posB);
		%posDiff = vectorSub(%posDiff,"0 0 0.1");
		%t.setTransform(%posDiff);
	}
}
datablock TriggerData(DRPGBattlefieldTriggerData)
{
	tickPeriodMS = 100;
};
function DRPGBattlefieldTriggerData::onEnterTrigger(%this,%trigger,%user)
{
	%user.inBattlefield = 1;
	if(isEventPending(%user.fieldTimeout))
		return;
	if(isObject(%user.client) && isObject(%trigger.spawnBrick))
	{
		%name = %trigger.spawnBrick.getName();
		if(strLen(%name) > 1)
			%user.client.bottomPrint("\c0WARNING - You are entering a battlefield: \c6" @ strReplace(getSubStr(%name,1,strLen(%name)-1),"_"," "),3);
	}
}
function DRPGBattlefieldTriggerData::onLeaveTrigger(%this,%trigger,%user)
{
	%user.inBattlefield = 0;
	if(!isObject(%user.client) || isEventPending(%user.fieldTimeout))
		return;
	if(isObject(%trigger.spawnBrick))
	{
		%name = %trigger.spawnBrick.getName();
		if(strLen(%name) > 1)
			%user.client.bottomPrint("\c0Leaving a battlefield: \c6" @ strReplace(getSubStr(%name,1,strLen(%name)-1),"_"," "),3);
	}
	%user.fieldTimeout = %user.schedule(%user.client.getPing(),"");
}
datablock TriggerData(DRPGSafeTriggerData)
{
	tickPeriodMS = 100;
};
function DRPGSafeTriggerData::onEnterTrigger(%this,%trigger,%user)
{
	if(%user.inSafeField)
	{
		%user.zoneSwitching = 1;
		%name = %trigger.spawnBrick.getName();
		if(strLen(%name) > 1)
		{
			%dn = strReplace(getSubStr(%name,1,strLen(%name)-1),"_"," ");
			if(%dn $= %user.zoneName)
			{
				return;
			} else {
				%user.zoneName = %dn;
			}
		}
	}
	%user.inSafefield = 1;
	if(!(%user.getType() & $TypeMasks::PlayerObjectType))
	{
		return;
	}
	if(!isObject(%user.director) && isObject(%user.client) && isObject(%trigger.spawnBrick))
	{
		%name = %trigger.spawnBrick.getName();
		if(strLen(%name) > 1)
		{
			%dn = strReplace(getSubStr(%name,1,strLen(%name)-1),"_"," ");
			%user.client.bottomPrint("\c6You are now entering \c3" @ %dn @ "\c6.",3);
			%user.zoneName = %dn;
		}
	}
	else
	{
		if(%user.director.mobType[%user.mobType,"TYPE"] $= "ENEMY")
		{
			%user.director.pauseMob(%user);
			%user.director.fadeMob(%user,500);
		}
	}
}
function DRPGSafeTriggerData::onLeaveTrigger(%this,%trigger,%user)
{
	if(!isObject(%user.client) || %user.zoneSwitching)
	{
		%user.zoneSwitching = 0;
		return;
	}
	%user.inSafefield = 0;
	%user.zoneName = "";

	if(isObject(%trigger.spawnBrick) && !isObject(%user.director))
	{
		%name = %trigger.spawnBrick.getName();
		if(strLen(%name) > 1)
			%user.client.bottomPrint("\c6You are now leaving \c3" @ strReplace(getSubStr(%name,1,strLen(%name)-1),"_"," ") @ "\c6.",3);
	}
}