drpgAddArmor("LOSER_CAPE","Loser Cape","A cape that gives the perfect amount of experience, for the real heroes.",-10000,"base/client/ui/avatarIcons/pack/cape","1 0 1 1",1,"cape","1 0 1 1");
package DRPG_OtherCapes
{
	function GameConnection::addExp(%client,%skill,%amount)
	{
		if(%skill !$= "level")
		{
			%pack = getField(strreplace($DRPG::Accounts.value[%client.bl_id,"equipment"],"|","\t"),1);
			%dbg = %client.getPlayerName() SPC "->" SPC %pack SPC "& \"" @ %skill @ "\"";
			if(%pack $= "LOSER_CAPE")
			{
				%amount *= 0.2;
			}
		}
		dbgo(%dbg);
		Parent::addExp(%client,%skill,%amount);
	}
};
activatePackage(DRPG_OtherCapes);