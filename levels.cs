function Gameconnection::updateStats(%client)
{
	if(%client.cachedLevel["level"] != (%level = getWord($DRPG::Accounts.value[%client.bl_id,"level"],0)))
	{
		commandToClient(%client,'setLevel',%level);
		%client.cachedLeve["level"] = %level;
	}
	%skills = $DRPG::Skills::List;
	for(%i=0;%i<getFieldCount(%skills);%i++)
	{
		%skill = getField(%skills,%i);
		%level = $DRPG::Accounts.value[%client.bl_id,%skill];
		%exp = getWord(%level,1);
		%level = getWord(%level,0);
		%exp = mFloor(((%exp - mPow(%level,2)) / (mPow(%level + 1,2) - mPow(%level,2))) * 100);
		if(%exp < 10)
		{
			%exp = "0" @ %exp;
		}
		%dec = %level @ "." @ %exp;
		%cached = %client.cachedLevel[%skill];
		if(%cached == %dec)
		{
			continue;
		}
		commandToClient(%client,'updateSkill',%skill,%dec);
		%client.cachedLevel[%skill] = %dec;
	}
}
function GameConnection::getSkillLevel(%client,%skill)
{
	%skills = $DRPG::Skills::List;
	for(%i=0;%i<getfieldcount(%skills);%i++)
	{
		%field = getField(%skills,%i);
		if(%field $= %skill)
		{
			%x = 1;
			break;
		}
	}
	if(%x)
	{
		%level = getWord($DRPG::Accounts.value[%client.bl_id,%skill],0);
		return %level;
	}
	return -1;
}
function GameConnection::checkSkill(%client,%skill)
{
	%skills = $DRPG::Skills::List;
	for(%i=0;%i<getfieldcount(%skills);%i++)
	{
		%field = getField(%skills,%i);
		if(%field $= %skill)
		{
			%x = 1;
			break;
		}
	}
	if(!%x)
	{
		%skill = "level";
	}
	%lev = $DRPG::Accounts.value[%client.bl_id,%skill];
	%exp = getWord(%lev,1);
	%lev = getWord(%lev,0);
	while(%exp >= mPow(%lev + 1,2))
	{
		%levelups++;
		%lev++;
		%gain += %lev;
	}
	$DRPG::Accounts.value[%client.bl_id,%skill] = %lev SPC %exp;

	if(%levelups > 0)
	{
		if(%skill !$= "level")
		{
			messageClient(%client,'',"You have leveled up in \c6" @ strupr(getsubstr(%skill,0,1)) @ strlwr(getSubstr(%skill,1,strlen(%skill))) @ "\c0!" @ ((%levelups > 1) ? " (" @ %levelups @ "x)" : "") SPC "You are now level\c6" SPC %lev @ "\c0.");
			%client.addExp("level",%gain);
		} else {
			messageClient(%client,'',"You have leveled up!" @ ((%levelups > 1) ? " (" @ %levelups @ "x)" : "") SPC "You are now level\c6" SPC %lev @ "\c0.");
		}
	}
	%client.updateStats();
	if(isFunction("GameConnection","update" @ %skill) && %levelups > 0)
	{
		eval("%client.update" @ %skill @ "();");
	}
}
function GameConnection::addExp(%client,%skill,%gain)
{
	%skills = $DRPG::Skills::List;
	for(%i=0;%i<getfieldcount(%skills);%i++)
	{
		%field = getField(%skills,%i);
		if(%field $= %skill)
		{
			%x = 1;
			break;
		}
	}
	if(!%x)
	{
		%skill = "level";
	}
	if(%skill !$= "level")
	{
		$DRPG::Accounts.value[%client.bl_id,%skill] = getWord($DRPG::Accounts.value[%client.bl_id,%skill],0) SPC mClampF(getWord($DRPG::Accounts.value[%client.bl_id,%skill],1) + %gain,0,10000);
	} else {
		$DRPG::Accounts.value[%client.bl_id,%skill] = getWord($DRPG::Accounts.value[%client.bl_id,%skill],0) SPC getWord($DRPG::Accounts.value[%client.bl_id,%skill],1) + %gain;
	}
	%client.checkSkill(%skill);
}