exec("./support/blockByte.cs");
$DRPG::Skills::List = "melee\tarchery\tdefense\tsmithing\tfletching\tcooking\twoodcutting\tmining\tfarming\tbuilding\thunting\talchemy";
forceRequiredAddOn("Player_FarmAnimals");
if(!isObject($DRPG::Accounts))
{
	$DRPG::Accounts = new ScriptObject()
	{
		class = blockByte;
		dataBaseFile = "config/server/DRPG/accounts.db";
		databaseFormat = "";
		doAutoSave = false;
		autoSaveInterval = 600000;
	};
	$DRPG::Accounts.addValue("inventory","TOOL_AXE 1|TOOL_PICKAXE 1||||||||||||||||||||||||||||");
	$DRPG::Accounts.addValue("level","1 0");
	$DRPG::Accounts.addValue("nation","");
	$DRPG::Accounts.addValue("plastic",0);
	for(%i=0;%i<getFieldCount($DRPG::Skills::List);%i++)
	{
		%skill = getField($DRPG::Skills::List,%i);
		$DRPG::Accounts.addValue(%skill,"1 0");
	}
	$DRPG::Accounts.addValue("equipment","|||LEATHER_CLOTHES|||||||LEATHER_SHOES");
	$DRPG::Accounts.addValue("recipesUnlocked", 0);
}