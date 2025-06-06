function StaticShape::growCrop(%this,%level)
{
	cancel(%this.grow);
	%g = %this.dataBlock.growsize;
	if(%this.stage > 0)
		%this.setScale(vectorAdd(%this.getScale(),%g SPC %g SPC %g));
	if(%this.stage++ == 100)
	{
		%this.isGrown = 1;
		if(isObject(DRPGWildManager.crops[%this.getDatablock().getName()]) && DRPGWildManager.crops[%this.getDatablock().getName()].isMember(%this))
		{
			DRPGWildManager.onGrown(%this);
		}
		return;
	}
	%bonus = (mCeil(%level / 15)/10) + 0.9;
	%this.grow = %this.schedule(%this.dataBlock.growSpeed/%bonus,growCrop,%level);
}

function GameConnection::plantSeed(%client,%source)
{
	if(!isObject(%client.player) || %client.cropCount >= 50 || !isObject($DRPG::Crops::CropFromSeed[%source]))
		return;

	%level = %client.getSkillLevel("farming");
	if(%client.hasItem(%source,1))
	{
		%raycast = containerRayCast(%client.player.getEyePoint(),vectorAdd(vectorScale(vectorNormalize(%client.player.getEyeVector()),10),%client.player.getEyePoint()),$TypeMasks::FxBrickAlwaysObjectType
,%client.player);
		if(!isObject(firstWord(%raycast)))
			return;
		%pos = posFromRaycast(%raycast);
		%crop = new StaticShape()
		{
			dataBlock = $DRPG::Crops::CropFromSeed[%source];
			position = %pos;
			isGrown = 0;
			owner = %client;
		};
		%client.cropCount++;
		%crop.setScale($DRPG::Crops::CropFromSeed[%source].initScale);
		%crop.setNodeColor("ALL",$DRPG::Crops::CropFromSeed[%source].color);
		%crop.growCrop(%level);
		%client.removeItem(%source,1);
	}
}
function sickleProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	if(!isObject(%obj.client))
		return;
	if(%col.dataBlock.DRPG_isHerb)
	{
		%level = %obj.client.getSkillLevel("farming");
		if(%level < %col.dataBlock.DRPG_requiredLevel)
		{
			centerPrint(%obj.client,"You need a \c6Farming\c0 level of \c6" @ %col.dataBlock.DRPG_requiredLevel @ "\c0 to harvest this herb.",3);
			return;
		}
		%bonus = (mCeil(%level / 15)/10) + 0.9;
		%col.hits+=%bonus;
		if(%col.hits >= %col.dataBlock.DRPG_life)
		{
			%col.hits = 0;
			%col.disappear(%col.dataBlock.DRPG_life);
			%exp = %col.dataBlock.DRPG_exp;

			%obj.client.addExp("farming",%exp*$DRPG::Prefs::ExpMultiplier["farming"]);
			centerPrint(%obj.client,"You harvested some \c6" @ %col.dataBlock.uiName @ "\c0",3);
			%obj.client.addItem(%col.dataBlock.DRPG_gives,1);
		}
		else
		{
			centerPrint(%obj.client,"The \c6" @ %col.dataBlock.uiName @ "\c0 is \c6" @ mFloor((%col.hits / mCeil(%col.dataBlock.DRPG_life)) * 100) @ "%\c0 harvested.",3);
		}
	}
	if(%col.dataBlock.DRPG_isCrop && %col.isGrown && !%col.isHarvested)
	{
		%level = %obj.client.getSkillLevel("farming");
		if(%level < %col.dataBlock.DRPG_requiredLevel)
		{
			centerPrint(%obj.client,"You must be level \c6" @ %col.dataBlock.DRPG_requiredLevel @ "\c0 farming to harvest this crop.",3);
			return;
		}
		%obj.client.addItem(%col.dataBlock.DRPG_gives,1);
		%exp = %col.dataBlock.DRPG_exp;
		if(getPerkLevel($DRPG::Accounts.value[%client.BL_ID,nation],FarmingEXP) == 1)
			%exp *= 1.25;
		else if(getPerkLevel($DRPG::Accounts.value[%client.BL_ID,nation],FarmingEXP) == 2)
			%exp *= 1.40;
		else if(getPerkLevel($DRPG::Accounts.value[%client.BL_ID,nation],FarmingEXP) == 3)
			%exp *= 2;

		%obj.client.addExp("farming",%exp*$DRPG::Prefs::ExpMultiplier["farming"]);
		%col.isHarvested = 1;
		%col.startFade(250,0,1);
		if(isObject(DRPGWildManager.crops[%col.getDatablock().getName()]) && DRPGWildManager.crops[%col.getDatablock().getName()].isMember(%col))
		{
			DRPGWildManager.schedule(500,"onHarvest",%col.getDatablock().getName());
		}
		%col.schedule(250,delete);
		%col.owner.cropCount--;
		%seedChance = -0.5 + %obj.client.skillCheck("farming",%col.dataBlock.DRPG_RequiredLevel);
		if(%seedchance <= 0)
		{
			dbgo("No seed");
			return;
		}
		while(%seedchance > 1)
		{
			%seedchance--;
			%seedProfit++;
		}
		if(getRandom() <= %seedchance)
		{
			%seedProfit++;
		}
		dbgo("seeds:" SPC %seedProfit);
		if(%seedProfit > 0)
		{
			%obj.client.addItem(%col.dataBlock.DRPG_source,%seedProfit);
		}
	}
	else if(%col.dataBlock.DRPG_isCrop && !%col.isGrown && !%col.isHarvested)
		centerPrint(%obj.client,"This crop is not done growing. (\c6" @ %col.stage @ "\c0%)",3);
}
