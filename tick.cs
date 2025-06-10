exec("./support/timeBomb.cs");
if(!isObject($DRPG::Ticks::Accounts))
{
	$DRPG::Ticks::Accounts = new ScriptObject(DRPGAccountsTick)
	{
		class = timeBomb;
		tickMinutes = 5;
		requiredTicks = 1;
		go = 1;
	};
}
function DRPGAccountsTick::onTick(%this,%stamp)
{
	%done = saveDRPGAccounts();
	export("$VARSAVE::Accounts*","config/server/SAVEDVARS.txt");
	echo("DRPG: Saved" SPC %done SPC "accounts.");
	if(!isObject(DRPGWildMobDirector) || !$DRPG::Prefs::WildMobs)
		return;
	%cc = ClientGroup.getCount();
	for(%i=0;%i<%cc;%i++)
	{
		%c = ClientGroup.getObject(%i);
		if(!isObject(%c.player) || %c.player.inSafefield)
			continue;
		%clevel = getWord($DRPG::Accounts.value[%c.bl_id,"level"],0);
		if(%clevel < 10)
			continue;
		%chance = mCeil(100 / %clevel);
		if(getRandom(0,%chance) == %chance)
		{
			%start = vectorAdd(%c.player.getTransform(),getRandom(-10,10) SPC getRandom(-10,10) SPC 500);
			%end = vectorAdd(%start,"0 0 -1000");
			%ray = containerRayCast(%start,%end,$TypeMasks::TerrainObjectType | $TypeMasks::FxBrickObjectType | $TypeMasks::WaterObjectType | $TypeMasks::PlayerObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::StaticObjectType);
			%position = posFromRaycast(%ray);
			%mob = DRPGWildMobDirector.spawnMob(%position,$DRPG::Mobs::WildType[getRandom(0,$DRPG::Mobs::WildTypePick-1)]);
			%mob.target = %c.player;
		}
	}
}
function DRPGAccountsTick::onStart(%this,%stamp)
{
	echo("DRPG: Accounts (5) tick started.");
}
function DRPGAccountsTick::onHalt(%this,%stamp)
{
	echo("DRPG: Accounts (5) tick stopped.");
}
function DRPGAccountsTick::onDestroy(%this,%stamp)
{
	return;
}
if(!isObject($DRPG::Ticks::Bricks))
{
	$DRPG::Ticks::Bricks = new ScriptObject(DRPGBricksTick)
	{
		class = timeBomb;
		tickMinutes = 5;
		requiredTicks = 6;
		go = 1;
	};
}
function DRPGBricksTick::onTick(%this,%stamp)
{
	// Disabled in favor of AutoSaver
	// %done = saveDRPGBricks(1,1);
	// echo("DRPG: Saved" SPC %done SPC "bricks.");
	if(getWordCount($DRPG::Nations::War["Vanote"]) > 0)
	{
		%cost = getWordCount($DRPG::Nations::War["Vanote"]) * 1000;
		if($DRPG::Nations::Wealth["Vanote"] < %cost)
		{
			messageAll('',"\c6Vanote\c0 has hit bankruptcy, and is unable to fund it's wars. All of it's declarations are now over.");
			$DRPG::Nations::War["Vanote"] = "";
			return;
		}
		$DRPG::Nations::Wealth["Vanote"] -= %cost;
		%owner = findclientbybl_id($DRPG::Nations::Leader["Vanote"]);
		if(isObject(%owner))
			messageClient(%owner,'',"You lost a sum of \c6" @ %cost @ "\c0 plastic from your nation's wealth pool due to war costs.");
	}
	if(getWordCount($DRPG::Nations::War["Elrad"]) > 0)
	{
		%cost = getWordCount($DRPG::Nations::War["Elrad"]) * 1000;
		%owner = findclientbybl_id($DRPG::Nations::Leader["Elrad"]);
		if($DRPG::Nations::Wealth["Elrad"] < %cost)
		{
			messageAll('',"\c6Elrad\c0 has hit bankruptcy, and is unable to fund it's wars. All of it's declarations are now over.");
			$DRPG::Nations::War["Elrad"] = "";
			return;
		}
		$DRPG::Nations::Wealth["Elrad"] -= %cost;
		if(isObject(%owner))
			messageClient(%owner,'',"You lost a sum of \c6" @ %cost @ "\c0 plastic from your nation's wealth pool due to war costs.");
	}
	if(getWordCount($DRPG::Nations::War["Oloni"]) > 0)
	{
		%cost = getWordCount($DRPG::Nations::War["Oloni"]) * 1000;
		%owner = findclientbybl_id($DRPG::Nations::Leader["Oloni"]);
		if($DRPG::Nations::Wealth["Oloni"] < %cost)
		{
			messageAll('',"\c6Oloni\c0 has hit bankruptcy, and is unable to fund it's wars. All of it's declarations are now over.");
			$DRPG::Nations::War["Oloni"] = "";
			return;
		}
		$DRPG::Nations::Wealth["Oloni"] -= %cost;
		if(isObject(%owner))
			messageClient(%owner,'',"You lost a sum of \c6" @ %cost @ "\c0 plastic from your nation's wealth pool due to war costs.");
	}
	if(getWordCount($DRPG::Nations::War["Derma"]) > 0)
	{
		%cost = getWordCount($DRPG::Nations::War["Derma"]) * 1000;
		$DRPG::Nations::Wealth["Derma"] -= %cost;
		%owner = findclientbybl_id($DRPG::Nations::Leader["Derma"]);
		if($DRPG::Nations::Wealth["Derma"] < %cost)
		{
			messageAll('',"\c6Derma\c0 has hit bankruptcy, and is unable to fund it's wars. All of it's declarations are now over.");
			$DRPG::Nations::War["Derma"] = "";
			return;
		}
		if(isObject(%owner))
			messageClient(%owner,'',"You lost a sum of \c6" @ %cost @ "\c0 plastic from your nation's wealth pool due to war costs.");
	}
}
function DRPGBricksTick::onStart(%this,%stamp)
{
	echo("DRPG: Bricks (30) tick started.");
}
function DRPGBricksTick::onHalt(%this,%stamp)
{
	echo("DRPG: Bricks (30) tick stopped.");
}
function DRPGBricksTick::onDestroy(%this,%stamp)
{
	return;
}
if(!isObject($DRPG::Ticks::Day))
{
	$DRPG::Ticks::Day = new ScriptObject(DRPGDayTick)
	{
		class = timeBomb;
		tickMinutes = 5;
		requiredTicks = 288;
		go = 1;
	};
}
function DRPGDayTick::onTick(%this,%stamp)
{
	if($DRPG::Nations::MemberCount["Vanote"] > 0)
	{
		%cost = mCeil(($DRPG::Nations::Wealth["Vanote"] / 200) * $DRPG::Nations::MemberCount["Vanote"]);
		if($DRPG::Nations::Wealth["Vanote"] < %cost)
		{
			return;
		}
		%owner = findclientbybl_id($DRPG::Nations::Leader["Vanote"]);
		$DRPG::Nations::Wealth["Vanote"] -= %cost;
		if(isObject(%owner))
			messageClient(%owner,'',"You lost a sum of \c6" @ %cost @ "\c0 plastic from your nation's wealth pool due to upkeep costs.");
	}
	if($DRPG::Nations::MemberCount["Elrad"] > 0)
	{
		%cost = mCeil(($DRPG::Nations::Wealth["Elrad"] / 200) * $DRPG::Nations::MemberCount["Elrad"]);
		if($DRPG::Nations::Wealth["Elrad"] < %cost)
		{
			return;
		}
		%owner = findclientbybl_id($DRPG::Nations::Leader["Elrad"]);
		$DRPG::Nations::Wealth["Elrad"] -= %cost;
		if(isObject(%owner))
			messageClient(%owner,'',"You lost a sum of \c6" @ %cost @ "\c0 plastic from your nation's wealth pool due to upkeep costs.");
	}
	if($DRPG::Nations::MemberCount["Oloni"] > 0)
	{
		%cost = mCeil(($DRPG::Nations::Wealth["Oloni"] / 200) * $DRPG::Nations::MemberCount["Oloni"]);
		if($DRPG::Nations::Wealth["Oloni"] < %cost)
		{
			return;
		}
		%owner = findclientbybl_id($DRPG::Nations::Leader["Oloni"]);
		$DRPG::Nations::Wealth["Oloni"] -= %cost;
		if(isObject(%owner))
			messageClient(%owner,'',"You lost a sum of \c6" @ %cost @ "\c0 plastic from your nation's wealth pool due to upkeep costs.");
	}
	if($DRPG::Nations::MemberCount["Derma"] > 0)
	{
		%cost = mCeil(($DRPG::Nations::Wealth["Derma"] / 200) * $DRPG::Nations::MemberCount["Derma"]);
		if($DRPG::Nations::Wealth["Derma"] < %cost)
		{
			return;
		}
		%owner = findclientbybl_id($DRPG::Nations::Leader["Derma"]);
		$DRPG::Nations::Wealth["Derma"] -= %cost;
		if(isObject(%owner))
			messageClient(%owner,'',"You lost a sum of \c6" @ %cost @ "\c0 plastic from your nation's wealth pool due to upkeep costs.");
	}
	%dayMsg[0] = "The sun rises over the plains of the realm as the new day begins..";
	%dayMsg[1] = "And so the new day begins..";
	%dayMsg[2] = "Birds start tweeting as they greet the new day..";
	messageAll('',%dayMsg[getRandom(0,2)]);
}
function DRPGDayTick::onStart(%this,%stamp)
{
	echo("DRPG: Day (1440) tick started.");
}
function DRPGDayTick::onHalt(%this,%stamp)
{
	echo("DRPG: Day (1440) tick stopped.");
}
function DRPGDayTick::onDestroy(%this,%stamp)
{
	return;
}
function serverCmdModTick(%client,%tick,%state)
{
	if(!%client.isAdmin)
		return;
	%tick = nameToID("DRPG" @ %tick @ "Tick");
	if(!isObject(%tick))
		return;
	if(%state && !isEventPending(%tick.tick))
	{
		%tick.start();
		messageAll('',"\c6" @ %client.getPlayerName() @ "\c0 has forced the \c6" @ %tick.getName() @ "\c0 tick to start.");
	}
	else if(%state && isEventPending(%tick.tick))
	{
		%tick.process();
		messageAll('',"\c6" @ %client.getPlayerName() @ "\c0 has forced the \c6" @ %tick.getName() @ "\c0 tick to process.");
	}
	else if(!%state && isEventPending(%tick.tick))
	{
		%tick.stop();
		messageAll('',"\c6" @ %client.getPlayerName() @ "\c0 has forced the \c6" @ %tick.getName() @ "\c0 tick to stop.");
	}
}