if(isFile("config/server/DRPG/nation.cs"))
	exec("config/server/DRPG/nation.cs");
else
{
	$DRPG::Nations::Tax["Derma"] = 0.05;
	$DRPG::Nations::Tax["Oloni"] = 0.05;
	$DRPG::Nations::Tax["Elrad"] = 0.05;
	$DRPG::Nations::Tax["Vanote"] = 0.05;
	$DRPG::Nations::Leader["Derma"] = "";
	$DRPG::Nations::Leader["Oloni"] = "";
	$DRPG::Nations::Leader["Elrad"] = "";
	$DRPG::Nations::Leader["Vanote"] = "";
	$DRPG::Nations::Wealth["Derma"] = 2000;
	$DRPG::Nations::Wealth["Oloni"] = 2000;
	$DRPG::Nations::Wealth["Elrad"] = 2000;
	$DRPG::Nations::Wealth["Vanote"] = 2000;
	$DRPG::Nations::PerkSlots["Derma"] = 1;
	$DRPG::Nations::PerkSlots["Oloni"] = 1;
	$DRPG::Nations::PerkSlots["Elrad"] = 1;
	$DRPG::Nations::PerkSlots["Vanote"] = 1;
	$DRPG::Nations::MemberCount["Derma"] = 0;
	$DRPG::Nations::MemberCount["Oloni"] = 0;
	$DRPG::Nations::MemberCount["Vanote"] = 0;
	$DRPG::Nations::MemberCount["Elrad"] = 0;
	export("$DRPG::Nations*","config/server/DRPG/nation.cs");
}
function isInt(%string)
{
	return %string $= mFloatLength(%string, 0);
}
$DRPG::Nations::ColorText["Derma"] = "\c6";
$DRPG::Nations::ColorText["Oloni"] = "\c2";
$DRPG::Nations::ColorText["Elrad"] = "\c0";
$DRPG::Nations::ColorText["Vanote"] = "\c4";
$DRPG::Nations::ColorName["Derma"] = "1 1 1";
$DRPG::Nations::ColorName["Oloni"] = "0 1 0";
$DRPG::Nations::ColorName["Elrad"] = "1 0 0";
$DRPG::Nations::ColorName["Vanote"] = "0 1 1";

$DRPG::PerkSlot[2] = 10000;
$DRPG::PerkSlot[3] = 40000;
$DRPG::PerkSlot[4] = 200000;
$DRPG::PerkCost[WoodcuttingEXP] = 4000;
$DRPG::PerkLevelCost[WoodcuttingEXP,2] = 10000;
$DRPG::PerkLevelCost[WoodcuttingEXP,3] = 50000;
$DRPG::PerkCost[MiningEXP] = 4000;
$DRPG::PerkLevelCost[MiningEXP,2] = 10000;
$DRPG::PerkLevelCost[MiningEXP,3] = 50000;
$DRPG::PerkCost[CraftingEXP] = 4000;
$DRPG::PerkLevelCost[CraftingEXP,2] = 10000;
$DRPG::PerkLevelCost[CraftingEXP,3] = 50000;
$DRPG::PerkCost[BuildingEXP] = 4000;
$DRPG::PerkLevelCost[BuildingEXP,2] = 10000;
$DRPG::PerkLevelCost[BuildingEXP,3] = 50000;
$DRPG::PerkCost[FarmingEXP] = 4000;
$DRPG::PerkLevelCost[FarmingEXP,2] = 10000;
$DRPG::PerkLevelCost[FarmingEXP,3] = 50000;
$DRPG::PerkCost[Regen] = 6000;
$DRPG::PerkLevelCost[Regen,2] = 15000;
$DRPG::PerkLevelCost[Regen,3] = 70000;
$DRPG::PerkCost[Speed] = 5000;
$DRPG::PerkLevelCost[Speed,2] = 13000;
$DRPG::PerkLevelCost[Speed,3] = 65000;
$DRPG::PerkCost[Damage] = 10000;
$DRPG::PerkLevelCost[Damage,2] = 22000;
$DRPG::PerkLevelCost[Damage,3] = 80000;
$DRPG::PerkCost[FallingSafety] = 5000;
$DRPG::PerkLevelCost[FallingSafety,2] = 14000;
$DRPG::PerkLevelCost[FallingSafety,3] = 72000;

if(!isObject(DermaSpawnPointData))
{
datablock fxDTSBrickData(DermaSpawnPointData : brickSpawnPointData)
{
	category = "DRPG";
	subCategory = "Nation Spawns";
	uiName = "Derma Spawn";
	DermaSpawn = 1;
};
}

if(!isObject(OloniSpawnPointData))
{
datablock fxDTSBrickData(OloniSpawnPointData : brickSpawnPointData)
{
	category = "DRPG";
	subCategory = "Nation Spawns";
	uiName = "Oloni Spawn";
	OloniSpawn = 1;
};
}

if(!isObject(ElradSpawnPointData))
{
datablock fxDTSBrickData(ElradSpawnPointData : brickSpawnPointData)
{
	category = "DRPG";
	subCategory = "Nation Spawns";
	uiName = "Elrad Spawn";
	ElradSpawn = 1;
};
}

if(!isObject(VanoteSpawnPointData))
{
datablock fxDTSBrickData(VanoteSpawnPointData : brickSpawnPointData)
{
	category = "DRPG";
	subCategory = "Nation Spawns";
	uiName = "Vanote Spawn";
	VanoteSpawn = 1;
};
}

function createDRPGSpawnsets()
{
	new SimSet(DermaSpawns);
	new SimSet(OloniSpawns);
	new SimSet(ElradSpawns);
	new SimSet(VanoteSpawns);
}
if(!isObject(DermaSpawns))
	createDRPGSpawnsets();

function fxDTSBrick::getSpawnTransform(%this)
{
	%trans = %this.getTransform();
	%pos = posFromTransform(%trans);
	%rot = rotFromTransform(%trans);
	
	if(fileName(%this.dataBlock.brickfile) $= "spawnPoint.blb")
		%pos = vectorSub(%pos,"0 0 1.25");
	else
		%pos = vectorAdd(%pos,"0 0 " @ %this.dataBlock.brickSizeZ*0.1);
	
	return %pos SPC %rot;
}
function serverCmdDonate(%client,%amount)
{
	%amount = mfloor(%amount);
	if($DRPG::Accounts.value[%client.BL_ID,"nation"] $= "" || !%client.hasPlastic(%amount) || %amount < 1)
		return;
	$DRPG::Nations::Wealth[$DRPG::Accounts.value[%client.BL_ID,"nation"]] += %amount;
	%client.removePlastic(%amount);
	messageAll('',"\c6" @ %client.getPlayerName() @ "\c0 has donated \c6" @ %amount @ "\c0 Plastic to \c6" @ $DRPG::Accounts.value[%client.BL_ID,"nation"] @ "\c0.");
}

function getPerkLevel(%nation,%perk)
{
	for(%x=1;%x<=$DRPG::Nations::PerkSlots[%nation];%x++)
		if($DRPG::Nations::Perk[%nation,%x] $= %perk)
			return $DRPG::Nations::PerkLevel[%nation,%x];
	return 0;
}
	
function serverCmdBuyNationPerk(%client,%slot,%perk)
{
	%nation = $DRPG::Accounts.value[%client.BL_ID,"nation"];
	if(%nation $= "")
		return;
	if($DRPG::Nations::Leader[%nation] != %client.BL_ID)
		return;
	if(%slot $= "" || %perk $= "")
	{
		messageClient(%client,'',"Syntax: \c6/BuyNationPerk Slot Perk");
		messageClient(%client,'',"\c6Your nation has " @ $DRPG::Nations::PerkSlots[%nation] @ " perk slots.");
		messageClient(%client,'',"\c6Type /nationPerks for a list of perks.");
		messageClient(%client,'',"Your nation may not have two of the same kind of perk.");
		return;
	}
	if(%slot < 1 || %slot > $DRPG::Nations::PerkSlots[%nation])
		return messageClient(%client,'',"Invalid Slot. /buyNationSlot to earn more slots.");
	if($DRPG::PerkCost[%perk] $= "")
		return messageClient(%client,'',"Invalid Perk. Type /buyNationPerk with no slot or perk for help.");
	if(getPerkLevel(%nation,%perk) > 0)
		return messageClient(%client,'',"Your nation already has this perk.");
	if($DRPG::Nations::Wealth[%nation] < $DRPG::PerkCost[%perk])
		return messageClient(%client,'',"Your nation cannot afford this perk.");
	$DRPG::Nations::Wealth[%nation]-=$DRPG::PerkCost[%perk];
	%oldperk = $DRPG::Nations::Perk[%nation,%slot];
	$DRPG::Nations::Perk[%nation,%slot] = %perk;
	$DRPG::Nations::PerkLevel[%nation,%slot] = 1;
	for(%x=0;%x<ClientGroup.getCount();%x++)
	{
		%cl = ClientGroup.getObject(%x);
		if($DRPG::Accounts.value[%cl.BL_ID,%nation] $= %nation)
				%cl.doPerkChecks();
	}
	if(%oldperk $= "")
		messageAll('',"\c6" @ %nation @ " \c0has purchased the \c6" @ %perk @ "\c0 perk.");
	else
		messageAll('',"\c6" @ %nation @ " \c0has purchased the \c6" @ %perk @ "\c0 perk, but \c6" @ %oldperk @ "\c0 is no longer in effect.");
	if($DRPG::PerkLevelCost[%perk,2] !$= "")
		messageClient(%client,'',"Next level costs \c6" @ $DRPG::PerkLevelCost[%perk,2] @ "\c0. Type /UpgradeNationPerk " @ %slot @ " to buy it now.");
	export("$DRPG::Nations*","config/server/DRPG/nation.cs");
}
function serverCmdBuyNationSlot(%client)
{
	%nation = $DRPG::Accounts.value[%client.BL_ID,"nation"];
	if(%nation $= "")
		return;
	if($DRPG::Nations::Leader[%nation] != %client.BL_ID)
		return;
	%slot = $DRPG::Nations::PerkSlots[%nation]+1;
	if($DRPG::PerkSlot[%slot] $= "")
		return messageClient(%client,'',"Your nation has the max number of slots.");
	if($DRPG::Nations::Wealth[%nation] < $DRPG::PerkSlot[%slot])
		return messageClient(%client,'',"Your nation cannot afford another slot. /nationPerks for pricing.");
	$DRPG::Nations::Wealth[%nation]-=$DRPG::PerkSlot[%slot];
	$DRPG::Nations::PerkSlots[%nation]++;
	messageAll('',"\c6" @ %nation @ " \c0now has \c6" @ %slot @ "\c0 perk slots.");
	export("$DRPG::Nations*","config/server/DRPG/nation.cs");
}
function serverCmdUpgradeNationPerk(%client,%slot)
{
	%nation = $DRPG::Accounts.value[%client.BL_ID,"nation"];
	if(%nation $= "")
		return;
	if($DRPG::Nations::Leader[%nation] != %client.BL_ID)
		return;
	if(%slot < 1 || %slot > $DRPG::Nations::PerkSlots[%nation])
		return messageClient(%client,'',"Invalid Slot. /buyNationSlot to earn more slots.");
	%perk = $DRPG::Nations::Perk[%nation,%slot];
	%level = $DRPG::Nations::PerkLevel[%nation,%slot]+1;
	if(%perk $= "")
		return messageClient(%client,'',"You need to buy a perk for this slot before you can upgrade it.");
	if($DRPG::PerkLevelCost[%perk,%level] $= "")
		return messageClient(%client,'',"This perk is already at it's max level.");
	if($DRPG::Nations::Wealth[%nation] < $DRPG::PerkLevelCost[%perk,%level])
		return messageClient(%client,'',"Your nation cannot afford this upgrade.");
	$DRPG::Nations::Wealth[%nation]-=$DRPG::PerkLevelCost[%perk,%level];
	$DRPG::Nations::PerkLevel[%nation,%slot]++;
	for(%x=0;%x<ClientGroup.getCount();%x++)
	{
		%cl = ClientGroup.getObject(%x);
		if($DRPG::Accounts.value[%cl.BL_ID,%nation] $= %nation)
				%cl.doPerkChecks();
	}
	messageAll('',"\c6" @ %nation @ " \c0has upgraded the \c6" @ %perk @ "\c0 perk to level \c6" @ %level @ "\c0.");
	if($DRPG::PerkLevelCost[%perk,%level+1] !$= "")
		messageClient(%client,'',"Next level costs \c6" @ $DRPG::PerkLevelCost[%perk,%level+1] @ "\c0. Type /UpgradeNationPerk " @ %slot @ " to buy it now.");
	export("$DRPG::Nations*","config/server/DRPG/nation.cs");
}
function serverCmdNationPerks(%client)
{
	messageClient(%client,'',"\c2WoodcuttingEXP \c6- \c54000/10000/50000 P \c6- Increases Nation's woodcutting exp by 25/40/100%");
	messageClient(%client,'',"\c2MiningEXP \c6- \c54000/10000/50000 P \c6- Increases Nation's mining exp by 25/40/100%");
	messageClient(%client,'',"\c2CraftingEXP \c6- \c54000/10000/50000 P \c6- Increases Nation's crafting exp by 25/40/100%");
	messageClient(%client,'',"\c2BuildingEXP \c6- \c54000/10000/50000 P \c6- Increases Nation's building exp by 25/40/100%");
	messageClient(%client,'',"\c2FarmingEXP \c6- \c54000/10000/50000 P \c6- Increases Nation's farming exp by 25/40/100%");
	messageClient(%client,'',"\c2Regen \c6- \c56000/15000/70000 P \c6- Players in the Nation recover 1.5/2.5/5 health per second.");
	messageClient(%client,'',"\c2Speed \c6- \c55000/13000/65000 P \c6- Players in the Nation gain 10/18/30% speed.");
	messageClient(%client,'',"\c2Damage \c6- \c510000/22000/80000 P \c6- Players in the Nation deal 10/18/30% extra damage.");
	messageClient(%client,'',"\c2FallingSafety \c6- \c55000/14000/72000 P \c6- Players in the Nation can safely fall from greater heights.");
	messageClient(%client,'',"Slot costs: 10000, 40000, 200000");
}
function serverCmdSetTaxRate(%client,%amount)
{
	if(%amount < 0 || %amount > 100)
		return;
	if($DRPG::Accounts.value[%client.BL_ID,"nation"] $= "")
		return;
	if($DRPG::Nations::Leader[$DRPG::Accounts.value[%client.BL_ID,"nation"]] != %client.BL_ID)
		return;
	%amount = mFloor(%amount);
	$DRPG::Nations::Tax[$DRPG::Accounts.value[%client.BL_ID,"nation"]] = %amount / 100;
	export("$DRPG::Nations*","config/server/DRPG/nation.cs");
	messageAll('',"\c6" @ $DRPG::Accounts.value[%client.BL_ID,"nation"] @ "\c0 has had it's tax rate set to \c6" @ %amount @ "%\c0.");
	for(%i=0;%i<ClientGroup.getCount();%i++)
	{
		%cl = ClientGroup.getObject(%i);
		if($DRPG::Accounts.value[%client.BL_ID,"nation"] $= $DRPG::Accounts.value[%cl.BL_ID,"nation"])
		{
			commandtoclient(%cl,'updateTax',%amount);
		}
	}
}
function strCap(%string)
{
	%c = getWordCount(%string);
	for(%i=0;%i<%c;%i++)
	{
		%w = getWord(%string,%i);
		%l = strLen(%w);
		if(%l > 1)
			%string = setWord(%string,%i,strUpr(getSubStr(%w,0,1)) @ getSubStr(%w,1,%l-1));
		else
			%string = setWord(%string,%i,strUpr(%w));
	}
	return %string;
}
function serverCmdSetWar(%client,%nation,%status)
{
	%clientNation = $DRPG::Accounts.value[%client.BL_ID,"nation"];
	if(%clientNation $= "")
		return;
	if($DRPG::Nations::Leader[%clientNation] != %client.BL_ID)
		return;
	if(strLwr(%clientNation) $= strLwr(%nation))
		return;
	%nation = strCap(%nation);
	if(%nation $= "Derma")
	{
		%yes = 1;
	}
	if(%nation $= "Oloni")
	{
		%yes = 1;
	}
	if(%nation $= "Elrad")
	{
		%yes = 1;
	}
	if(%nation $= "Vanote")
	{
		%yes = 1;
	}
	if(%yes)
	{
		if(%status)
		{
			if($DRPG::Nations::Wealth[%clientNation] < 5000)
			{
				messageClient(%client,'',"You need at least \c65000\c0 nation plastic to go to war.");
				return;
			}
			if(hasItemOnList($DRPG::Nations::War[%clientNation],%nation))
			{
				messageClient(%client,'',"You're already at war with that nation.");
				return;
			}
			$DRPG::Nations::War[%clientNation] = addItemToList($DRPG::Nations::War[%clientNation],%nation);
			if(!hasItemOnList($DRPG::Nations::War[%nation],%clientNation))
				$DRPG::Nations::War[%nation] = addItemToList($DRPG::Nations::War[%nation],%clientNation);
			messageAll('',"\c6" @ %client.getPlayerName() @ "\c0 of \c6" @ %clientNation @ "\c0 has declared war on \c6" @ %nation @ "\c0.");
			$DRPG::Nations::Wealth[$DRPG::Accounts.value[%client.BL_ID,"nation"]] -= 5000;
		}
		else
		{
			if(!hasItemOnList($DRPG::Nations::War[%clientNation],%nation))
			{
				messageClient(%client,'',"You're not at war with that nation.");
				return;
			}
			$DRPG::Nations::War[%clientNation] = removeItemFromList($DRPG::Nations::War[%clientNation],%nation);
			if(hasItemOnList($DRPG::Nations::War[%nation],%clientNation))
				$DRPG::Nations::War[%nation] = removeItemFromList($DRPG::Nations::War[%nation],%clientNation);
			messageAll('',"After many deaths, \c6" @ %client.getPlayerName() @ "\c0 of \c6" @ %clientNation @ "\c0 has ended the war on \c6" @ %nation @ "\c0.");
		}
	}
}
function serverCmdBanish(%client,%id,%status)
{
	%clientNation = $DRPG::Accounts.value[%client.BL_ID,"nation"];
	if(%clientNation $= "")
		return;
	if($DRPG::Nations::Leader[%clientNation] != %client.BL_ID)
		return;
	if(!isInt(%id))
		return;
	if(%id == %client.bl_id)
		return;
	if(%status $= "" || %status != 1 && %status != 0)
		%status = 1;
	$DRPG::Nations::Banished[%clientNation,%id] = %status;
	%name = nameToID("BrickGroup_" @ %id);
	%name = isObject(%name) == 1 ? %name.name : "BL_ID: " @ %id;
	if(%status)
	{
		messageAll('',"\c6" @ %client.getPlayerName() @ "\c0 has banished \c6" @ %name @ "\c0 from \c6" @ %clientNation @ "\c0.");
		%targetNation = $DRPG::Accounts.value[%id,"nation"];
		if(%targetNation !$= "" && %targetNation $= %clientNation)
		{
			$DRPG::Accounts.value[%id,"nation"] = "";
			%target = findclientbybl_id(%id);
			if(isObject(%target))
			{
				%target.instantRespawn();
			}
			$DRPG::Nations::MemberCount[%targetNation]--;
		}
	}
	else
	{
		messageAll('',"\c6" @ %client.getPlayerName() @ "\c0 has pardoned \c6" @ %name @ "'s\c0 banishment from \c6" @ %clientNation @ "\c0.");
	}
}
function serverCmdCheckNation(%client)
{
	%nation = $DRPG::Accounts.value[%client.BL_ID,"nation"];
	if(%nation $= "")
		return;
	messageClient(%client,'',"\c6" @ %nation @ "\c0 has \c6" @ $DRPG::Nations::Tax[%nation] * 100 @ "%\c0 tax and a wealth pool of \c6" @ $DRPG::Nations::Wealth[%nation] @ "\c0 Plastic.");
	%war = $DRPG::Nations::War[%nation];
	if(getWordCount(%war) > 0)
			messageClient(%client,'',"Your nation is at war with: \c6" @ strReplace(%war," ",", ") @ "\c0.");
	for(%x=1;%x<=4;%x++)
		if(getPerkLevel(%nation,$DRPG::Nations::Perk[%nation,%x]) > 0)
			messageClient(%client,'',%x @ "\c6 - \c2" @ $DRPG::Nations::Perk[%nation,%x] @ "\c6 - Level " @ $DRPG::Nations::PerkLevel[%nation,%x]);
}
function serverCmdJoin(%client,%nation)
{
	if($DRPG::Accounts.value[%client.BL_ID,"nation"] !$= "")
	{
		centerPrint(%client,"You need to type \c6/leave\c0 to leave your current nation before joining a new one.",3);
		return;
	}
	if(!%client.hasPlastic(250))
	{
		centerPrint(%client,"You need \c6250\c0 Plastic to join a nation.",3);
		return;
	}
	%nation = strCap(%nation);
	if($DRPG::Nations::Banished[%nation,%client.bl_id])
	{
		centerPrint(%client,"You need have been banished from this nation.",3);
		return;
	}
	if(%nation $= "Derma")
	{
		$DRPG::Accounts.value[%client.BL_ID,"nation"] = "Derma";
		messageAll('',"\c6" @ %client.getPlayerName() @ "\c0 has become a citizen of \c6Derma\c0.");
		%client.removePlastic(250);
		commandtoclient(%client,'updateTax',$DRPG::Nations::Tax["Derma"] * 100);
		%client.spawnPlayer();
		$DRPG::Nations::MemberCount["Derma"]++;
		return;
	}
	if(%nation $= "Oloni")
	{
		$DRPG::Accounts.value[%client.BL_ID,"nation"] = "Oloni";
		messageAll('',"\c6" @ %client.getPlayerName() @ "\c0 has become a citizen of \c6Oloni\c0.");
		%client.removePlastic(250);
		commandtoclient(%client,'updateTax',$DRPG::Nations::Tax["Oloni"] * 100);
		%client.spawnPlayer();
		$DRPG::Nations::MemberCount["Oloni"]++;
		return;
	}
	if(%nation $= "Elrad")
	{
		$DRPG::Accounts.value[%client.BL_ID,"nation"] = "Elrad";
		messageAll('',"\c6" @ %client.getPlayerName() @ "\c0 has become a citizen of \c6Elrad\c0.");
		%client.removePlastic(250);
		commandtoclient(%client,'updateTax',$DRPG::Nations::Tax["Elrad"] * 100);
		%client.spawnPlayer();
		$DRPG::Nations::MemberCount["Elrad"]++;
		return;
	}
	if(%nation $= "Vanote")
	{
		$DRPG::Accounts.value[%client.BL_ID,"nation"] = "Vanote";
		messageAll('',"\c6" @ %client.getPlayerName() @ "\c0 has become a citizen of \c6Vanote\c0.");
		%client.removePlastic(250);
		commandtoclient(%client,'updateTax',$DRPG::Nations::Tax["Vanote"] * 100);
		%client.spawnPlayer();
		$DRPG::Nations::MemberCount["Vanote"]++;
		return;
	}
}
function serverCmdLeave(%client)
{
	if($DRPG::Accounts.value[%client.BL_ID,"nation"] $= "")
		return;
	$DRPG::Nations::MemberCount[$DRPG::Accounts.value[%client.BL_ID,"nation"]]--;
	$DRPG::Accounts.value[%client.BL_ID,"nation"] = "";
	%client.updateStats();
	centerPrint(%client,"You have left the nation.",3);
	%client.spawnPlayer();
	%client.setNationColor();
}

function GameConnection::SetNationColor(%client)
{
	%nation = $DRPG::Accounts.value[%client.BL_ID,"nation"];
	if(%nation $= "")
		%client.player.setShapeNameColor("0.375 0.375 0.375");
	else
		%client.player.setShapeNameColor($DRPG::Nations::ColorName[%nation]);
}

package DRPG_LeaderChat
{
	function serverCmdMessageSent(%client,%message)
	{
		%clientNation = $DRPG::Accounts.value[%client.bl_id,"nation"];
		if($DRPG::Nations::Leader[%clientNation] != %client.bl_id || getSubStr(%message,0,1) !$= "^")
		{
			parent::serverCmdMessageSent(%client,%message);
			return;
		}
		%message = getSubStr(%message,1,strLen(%message));
		for(%i=0;%i<clientGroup.getCount();%i++)
		{
			%cl = clientGroup.getObject(%i);
			%clNat = $DRPG::Accounts.value[%cl.bl_id,"nation"];
			if($DRPG::Nations::Leader[%clNat] == %cl.bl_id)
			{
				%cl.chatMessage("<color:ff0000>[<color:ffffff>COUNCIL<color:ff0000>]<color:ffff00>" @ %client.getPlayerName() @ "<color:ff0000>[<color:ffffff>" @ $DRPG::Accounts.value[%client.bl_id,"nation"] @ "<color:ff0000>]<color:ffffff>:" SPC %message);
			}
		}
	}
};
activatePackage(DRPG_LeaderChat);
