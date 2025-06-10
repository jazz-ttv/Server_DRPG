if(!isObject(brickDRPGTinData))
{
datablock fxDtsBrickData(brickDRPGTinData : brick2x2RoundData)
{
	category = "DRPG";
	subCategory = "Ores";
	uiName = "Tin Ore";
	DRPG_isOre = 1;
	DRPG_requiredLevel = 1;
	DRPG_gives = "TIN_ORE";
	DRPG_exp = 0.38;
	DRPG_life = 15;
	veinSize = 20;
	veinCount = 16;
	veinVar = 2;
	defColor = 50;
};
}
if(!isObject(brickDRPGCopperData))
{
datablock fxDtsBrickData(brickDRPGCopperData : brick2x2RoundData)
{
	category = "DRPG";
	subCategory = "Ores";
	uiName = "Copper Ore";
	DRPG_isOre = 1;
	DRPG_requiredLevel = 1;
	DRPG_gives = "COPPER_ORE";
	DRPG_exp = 0.38;
	DRPG_life = 15;
	veinSize = 12;
	veinCount = 12;
	veinVar = 2;
	defColor = 11;
};
}
if(!isObject(brickDRPGSilverData))
{
datablock fxDtsBrickData(brickDRPGSilverData : brick2x2RoundData)
{
	category = "DRPG";
	subCategory = "Ores";
	uiName = "Silver Ore";
	DRPG_isOre = 1;
	DRPG_requiredLevel = 10;
	DRPG_gives = "SILVER_ORE";
	DRPG_exp = 1.55;
	DRPG_life = 45;
	veinSize = 30;
	veinCount = 6;
	veinVar = 0;
	defColor = 48;
};
}
if(!isObject(brickDRPGGoldData))
{
datablock fxDtsBrickData(brickDRPGGoldData : brick2x2RoundData)
{
	category = "DRPG";
	subCategory = "Ores";
	uiName = "Gold Ore";
	DRPG_isOre = 1;
	DRPG_requiredLevel = 30;
	DRPG_gives = "GOLD_ORE";
	DRPG_exp = 3.2;
	DRPG_life = 110;
	veinSize = 30;
	veinCount = 4;
	veinVar = 0;
	defColor = 12;
};
}
if(!isObject(brickDRPGDermiteData))
{
datablock fxDtsBrickData(brickDRPGDermiteData : brick2x2RoundData)
{
	category = "DRPG";
	subCategory = "Ores";
	uiName = "Dermite Ore";
	DRPG_isOre = 1;
	DRPG_requiredLevel = 15;
	DRPG_gives = "DERMITE_ORE";
	DRPG_exp = 2.85;
	DRPG_life = 50;
	defColor = 10;
};
}
if(!isObject(brickDRPGIronData))
{
datablock fxDtsBrickData(brickDRPGIronData : brick2x2RoundData)
{
	category = "DRPG";
	subCategory = "Ores";
	uiName = "Iron Ore";
	DRPG_isOre = 1;
	DRPG_requiredLevel = 40;
	DRPG_gives = "IRON_ORE";
	DRPG_exp = 5;
	DRPG_life = 200;
	veinSize = 30;
	veinCount = 24;
	veinVar = 8;
	defColor = 41;
};
}
if(!isObject(brickDRPGDestiniteData))
{
datablock fxDtsBrickData(brickDRPGDestiniteData : brick2x2RoundData)
{
	category = "DRPG";
	subCategory = "Ores";
	uiName = "Destinite Ore";
	DRPG_isOre = 1;
	DRPG_requiredLevel = 90;
	DRPG_gives = "DESTINITE_ORE";
	DRPG_exp = 13;
	DRPG_life = 500;
	veinSize = 100;
	veinCount = 8;
	veinVar = 1;
	defColor = 18;
};
}

// Coal and obsidian coal
if(!isObject(brickDRPGCoalData))
{
datablock fxDtsBrickData(brickDRPGCoalData : brick2x2RoundData)
{
	category = "DRPG";
	subCategory = "Ores";
	uiName = "Coal";
	DRPG_isOre = 1;
	DRPG_requiredLevel = 40;
	DRPG_gives = "COAL";
	DRPG_exp = 6.1;
	DRPG_life = 90;
	veinSize = 30;
	veinCount = 20;
	veinVar = 10;
	defColor = 52;
};
}
if(!isObject(brickDRPGObsCoalData))
{
datablock fxDtsBrickData(brickDRPGObsCoalData : brick2x2RoundData)
{
	category = "DRPG";
	subCategory = "Ores";
	uiName = "Obsidian Coal";
	DRPG_isOre = 1;
	DRPG_requiredLevel = 100;
	DRPG_gives = "OBSIDIAN_COAL";
	DRPG_exp = 19;
	DRPG_life = 250;
	defColor = 53;
};
}
function pickaxeProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	if(%col.dataBlock.DRPG_isOre)
	{
		%level = %obj.client.getSkillLevel("mining");
		if(%level < %col.dataBlock.DRPG_requiredLevel)
		{
			centerPrint(%obj.client,"You need a \c6Mining\c0 level of \c6" @ %col.dataBlock.DRPG_requiredLevel @ "\c0 to ground this ore.",3);
			return;
		}
		%bonus = (mCeil(%level / 15)/10) + 0.9;
		if(%obj.client.speedpotion)
		{
			%bonus += 2;
		}
		%col.hits+=%bonus;
		if(%col.hits >= %col.dataBlock.DRPG_life)
		{
			%col.hits = 0;
			%col.disappear(%col.dataBlock.DRPG_life);
			%exp = %col.dataBlock.DRPG_exp;
			if(getPerkLevel($DRPG::Accounts.value[%obj.client.BL_ID,nation],MiningEXP) == 1)
				%exp *= 1.25;
			else if(getPerkLevel($DRPG::Accounts.value[%obj.client.BL_ID,nation],MiningEXP) == 2)
				%exp *= 1.40;
			else if(getPerkLevel($DRPG::Accounts.value[%obj.client.BL_ID,nation],MiningEXP) == 3)
				%exp *= 2;

			%obj.client.addExp("mining",%exp*$DRPG::Prefs::ExpMultiplier["mining"]);
			centerPrint(%obj.client,"You ground the \c6" @ %col.dataBlock.uiName @ "\c0 down.",3);
			%obj.client.addItem(%col.dataBlock.DRPG_gives,1);
		}
		else
		{
			centerPrint(%obj.client,"The \c6" @ %col.dataBlock.uiName @ "\c0 is \c6" @ mFloor((%col.hits / mCeil(%col.dataBlock.DRPG_life)) * 100) @ "%\c0 grounded.",3);
		}
	}
}