if(!isObject(PKTDungeon))
{
	new ScriptObject(PKTDungeon)
	{
		class = dungeonGenerator;
		name = "Pirate King's Tomb";
	};
	PKTDungeon.addRoomType("Add-Ons/Server_DRPG/dungeons/PKT/0.dr");
	PKTDungeon.addRoomType("Add-Ons/Server_DRPG/dungeons/PKT/1.dr");
	PKTDungeon.addRoomType("Add-Ons/Server_DRPG/dungeons/PKT/2.dr");
	PKTDungeon.spawnDungeon("0 0 -1000",48,9);
}