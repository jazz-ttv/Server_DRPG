if(!isObject(brickDRPGOakData))
{
datablock fxDtsBrickData(brickDRPGOakData : brickPineTreeData)
{
	category = "DRPG";
	subCategory = "Trees";
	uiName = "Oak Tree";
	DRPG_isWood = 1;
	DRPG_requiredLevel = 1;
	DRPG_gives = "OAK_WOOD";
	DRPG_exp = 0.38;
	DRPG_life = 15;
	defColor = 8;
};
}
if(!isObject(brickDRPGWillowData))
{
datablock fxDtsBrickData(brickDRPGWillowData : brickPineTreeData)
{
	category = "DRPG";
	subCategory = "Trees";
	uiName = "Willow Tree";
	DRPG_isWood = 1;
	DRPG_requiredLevel = 5;
	DRPG_gives = "WILLOW_WOOD";
	DRPG_exp = 1.1;
	DRPG_life = 30;
	defColor = 2;
};
}
if(!isObject(brickDRPGMapleData))
{
datablock fxDtsBrickData(brickDRPGMapleData : brickPineTreeData)
{
	category = "DRPG";
	subCategory = "Trees";
	uiName = "Maple Tree";
	DRPG_isWood = 1;
	DRPG_requiredLevel = 15;
	DRPG_gives = "MAPLE_WOOD";
	DRPG_exp = 2.6;
	DRPG_life = 50;
	defColor = 9;
};
}
if(!isObject(brickDRPGYewData))
{
datablock fxDtsBrickData(brickDRPGYewData : brickPineTreeData)
{
	category = "DRPG";
	subCategory = "Trees";
	uiName = "Yew Tree";
	DRPG_isWood = 1;
	DRPG_requiredLevel = 40;
	DRPG_gives = "YEW_WOOD";
	DRPG_exp = 5.2;
	DRPG_life = 120;
	defColor = 1;
};
}
if(!isObject(brickDRPGMoonwellData))
{
datablock fxDtsBrickData(brickDRPGMoonwellData : brickPineTreeData)
{
	category = "DRPG";
	subCategory = "Trees";
	uiName = "Moonwell Tree";
	DRPG_isWood = 1;
	DRPG_requiredLevel = 75;
	DRPG_gives = "MOONWELL_WOOD";
	DRPG_exp = 12;
	DRPG_life = 400;
	defColor = 15;
};
}
function axeProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	if(%col.dataBlock.DRPG_isWood)
	{
		%level = %obj.client.getSkillLevel("woodcutting");
		if(%level < %col.dataBlock.DRPG_requiredLevel)
		{
			centerPrint(%obj.client,"You need a \c6Woodcutting\c0 level of \c6" @ %col.dataBlock.DRPG_requiredLevel @ "\c0 to chop down this tree.",3);
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

			if(getPerkLevel($DRPG::Accounts.value[%obj.client.BL_ID,nation],WoodcuttingEXP) == 1)
				%exp *= 1.25;
			else if(getPerkLevel($DRPG::Accounts.value[%obj.client.BL_ID,nation],WoodcuttingEXP) == 2)
				%exp *= 1.40;
			else if(getPerkLevel($DRPG::Accounts.value[%obj.client.BL_ID,nation],WoodcuttingEXP) == 3)
				%exp *= 2;

			%obj.client.addExp("woodcutting",%exp*$DRPG::Prefs::ExpMultiplier["woodcutting"]);
			centerPrint(%obj.client,"You chopped the \c6" @ %col.dataBlock.uiName @ "\c0 down.",3);
			%obj.client.addItem(%col.dataBlock.DRPG_gives,1);
		}
		else
		{
			centerPrint(%obj.client,"The \c6" @ %col.dataBlock.uiName @ "\c0 is \c6" @ mFloor((%col.hits / mCeil(%col.dataBlock.DRPG_life)) * 100) @ "%\c0 chopped down.",3);
		}
	}
}
