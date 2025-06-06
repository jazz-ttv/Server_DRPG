package DRPG_hunting
{
	function holeBotDamage(%this, %obj, %source, %a, %b, %c)
	{
			parent::holeBotDamage(%this, %obj, %source, %a, %b, %c);
			%level = %source.client.getSkillLevel("hunting");
			if(%level < %this.requiredLevel)
			{
				centerPrint(%source.client,"You must be level \c6" @ %this.requiredLevel @ "\c0 hunting to skin this animal.",3);
				return;
			}
			
			if(%obj.hCalledBotDeath)
			{
				%exp = %this.hEXP;
				%source.client.addExp("hunting",%exp);
			
				%amount = getRandom(%this.lowDrop,%this.highDrop);
				%id = %this.dropID;
				%chance = getRandom(1,3);
				if(%chance == 1)
				{
					%source.client.addItem(%this.secondDropID, "1");
				}
				%source.client.addItem(%id,%amount);
			}
	}
};
activatePackage("DRPG_hunting");