drpgAddArmor("WOODCUTTING_REWARD_CAPE","Woodcutting Cape","A cape that rewards you with bonus woodcutting experience.",25000,"base/client/ui/avatarIcons/pack/cape","0 0.5 0.2 1",1,"cape","0 0.5 0.2 1");
drpgAddArmor("MINING_REWARD_CAPE","Mining Cape","A cape that rewards you with bonus mining experience.",25000,"base/client/ui/avatarIcons/pack/cape","0 0 0 1",1,"cape","0 1 0 1");
drpgAddArmor("FARMING_REWARD_CAPE","Farming Cape","A cape that rewards you with bonus farming experience.",25000,"base/client/ui/avatarIcons/pack/cape","0.4 0.2 0 1",1,"cape","0.4 0.2 0 1");
drpgAddArmor("BUILDING_REWARD_CAPE","Building Cape","A cape that rewards you with bonus building experience.",25000,"base/client/ui/avatarIcons/pack/cape","1 0 0 1",1,"cape","1 0 0 1");
drpgAddArmor("CRAFTING_REWARD_CAPE","Crafting Cape","A cape that rewards you with bonus crafting experience.",25000,"base/client/ui/avatarIcons/pack/cape","1 1 1 1",1,"cape","1 1 1 1");
drpgAddArmor("LEGEND_REWARD_CAPE","Legend Cape","A cape that rewards you with bonus experience in everything.",100000,"base/client/ui/avatarIcons/pack/cape","0.8 0.8 0.1 1",1,"cape","0.8 0.8 0.1 1");
drpgAddArmor("FURDLE_CAPE","Furdle Cape","A cape that rewards you with being a faggot.",100000,"base/client/ui/avatarIcons/pack/cape","0 1 0 1",1,"cape","0 1 0 1");
package DRPG_RewardCapes
{
	function GameConnection::addExp(%client,%skill,%amount)
	{
		if(%skill !$= "level")
		{
			%pack = getField(strreplace($DRPG::Accounts.value[%client.bl_id,"equipment"],"|","\t"),1);
			%dbg = %client.getPlayerName() SPC "->" SPC %pack SPC "& \"" @ %skill @ "\"";
			if(%pack $= (strUpr(%skill) @ "_REWARD_CAPE"))
			{
				%dbg = %dbg SPC "(" @ %amount @ "*2)";
				%amount *= 2;
			} else if(%pack $= "CRAFTING_REWARD_CAPE" && (%skill $= "smithing" || %skill $= "fletching" || %skill $= "crafting"))
			{
				%dbg = %dbg SPC "(" @ %amount @ "*2)";
				%amount *= 2;
			} else if(%pack $= "LEGEND_REWARD_CAPE")
			{
				%dbg = %dbg SPC "(" @ %amount @ "*2)";
				%amount *= 2;
			} else if(%pack $= "FURDLE_CAPE")
			{
				%dbg = %dbg SPC "(" @ %amount @ "*5)";
				%amount *= 10;
			}
		}
		dbgo(%dbg);
		Parent::addExp(%client,%skill,%amount);
	}
};
activatePackage(DRPG_RewardCapes);

//Leader Pauldrons

drpgAddArmor("ELRAD_PAULDRONS","Elrad Pauldrons","Pauldrons for the Elrad Leader",3000*$DRPG::Prefs::PriceMultiplier["armor"],"base/client/ui/avatarIcons/secondpack/epaulets","1 0 0 1",2,"epaulets","1 0 0 1","",0.1);
drpgAddArmor("VANOTE_PAULDRONS","Vanote Pauldrons","Pauldrons for the Vanote Leader",3000*$DRPG::Prefs::PriceMultiplier["armor"],"base/client/ui/avatarIcons/secondpack/epaulets","0 1 1 1",2,"epaulets","0 1 1 1","",0.1);
drpgAddArmor("DERMA_PAULDRONS","Derma Pauldrons","Pauldrons for the Derma Leader",3000*$DRPG::Prefs::PriceMultiplier["armor"],"base/client/ui/avatarIcons/secondpack/epaulets","1 1 1 1",2,"epaulets","1 1 1 1","",0.1);
drpgAddArmor("OLONI_PAULDRONS","Oloni Pauldrons","Pauldrons for the Oloni Leader",3000*$DRPG::Prefs::PriceMultiplier["armor"],"base/client/ui/avatarIcons/secondpack/epaulets","0 1 0 1",2,"epaulets","0 1 0 1","",0.1);

//Leader Hats
drpgAddArmor("ELRAD_BICORN","Elrad Bicorn","ARRRRGGGHHHH.",45*$DRPG::Prefs::PriceMultiplier["apparel"],"base/client/ui/avatarIcons/hat/bicorn","1 0 0 1",0,"bicorn","1 0 0 1");
drpgAddArmor("VANOTE_BICORN","Vanote Bicorn","ARRRRGGGHHHH.",45*$DRPG::Prefs::PriceMultiplier["apparel"],"base/client/ui/avatarIcons/hat/bicorn","0 1 1 1",0,"bicorn","0 1 1 1");
drpgAddArmor("DERMA_BICORN","Derma Bicorn","ARRRRGGGHHHH.",45*$DRPG::Prefs::PriceMultiplier["apparel"],"base/client/ui/avatarIcons/hat/bicorn","1 1 1 1",0,"bicorn","1 1 1 1");
drpgAddArmor("OLONI_BICORN","Oloni Bicorn","ARRRRGGGHHHH.",45*$DRPG::Prefs::PriceMultiplier["apparel"],"base/client/ui/avatarIcons/hat/bicorn","0 1 0 1",0,"bicorn","0 1 0 1");