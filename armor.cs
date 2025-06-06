package DRPGArmor
{
	function Armor::Damage(%data,%player,%attacker,%pos,%damage,%type)
	{
		if(%type == $DamageType::Fall || %type == $DamageType::Impact || %player.inSafeField || %attacker.inSafeField || ($DRPG::Accounts.value[%attacker.client.bl_id,"level"] !$= "" && getWord($DRPG::Accounts.value[%attacker.client.bl_id,"level"],0) < 10) || ($DRPG::Accounts.value[%player.client.bl_id,"level"] !$= "" && getWord($DRPG::Accounts.value[%player.client.bl_id,"level"],0) < 10) || ($DRPG::Accounts.value[%player.client.bl_id,"nation"] $= $DRPG::Accounts.value[%attacker.client.bl_id,"nation"] && $DRPG::Accounts.value[%player.client.bl_id,"nation"] !$= ""))
			return Parent::Damage(%data,%player,%attacker,%pos,%damage,%type);
		if(isObject(%player.director))
		{
			%skill = %player.director.mobType[%player.mobType,"DEFENSE"];
			%def = %player.director.mobType[%player.mobType,"DEFENSE"] / 100;
			%defscale = mClampF((%skill - 20) * 100,-0.2,0.8);
			%def += %def * %defscale;
			%scale = mClampF(1 - %def,0,1);
			%damage *= %scale;
			Parent::Damage(%data,%player,%attacker,%pos,%damage,%type);
			return;
		}
		else if(%type == $DamageType::DRPGGun || !isObject(%player.client) || %player.client == %attacker.client)
		{
			Parent::Damage(%data,%player,%attacker,%pos,%damage,%type);
			return;
		}
		%playerl = getWord($DRPG::Accounts.value[%player.client.bl_id,"level"],0);
		if(%playerl !$= "" && %playerl < 10)
			return;
		%equips = strreplace($DRPG::Accounts.value[%player.client.bl_id,"equipment"],"|","\t");
		%def = 0;
		for(%i=0;%i<=10;%i++)
		{
			if(%i >= 4 && %i <= 8)
			{
				continue;
			}
			%item = getfield(%equips,%i);
			if(%item $= "")
			{
				continue;
			}
			%def += $DRPG::Items::EquipDefense[%item];
		}
		%skill = %player.client.getSkillLevel("defense");
		%defscale = mClampF((%skill - 20) * 100,-0.2,0.8);
		%def += %def * %defscale;
		%scale = mClampF(1 - %def,0,1);
		%pre = %damage;
		%damage *= %scale;
		%post = (%pre) / 25;
		%player.client.addExp("defense",%post*$DRPG::Prefs::ExpMultiplier["defense"]);
		Parent::Damage(%data,%player,%attacker,%pos,%damage,%type);
	}
};
activatePackage(DRPGArmor);