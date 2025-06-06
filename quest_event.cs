function GameConnection::startQuest(%client,%string)
{
	%check = 1;

	for(%a = 0; %a < getFieldCount($VARSAVE::Accounts[%client.BL_ID,"Quests"]); %a++)
	{
		if(getField($VARSAVE::Accounts[%client.BL_ID,"Quests"],%a) $= %string)
			%check = 0;
	}
	
	if(%check)
	{
		messageClient(%client,'',"\c0You have taken on the quest \c6" @ %string);
	}
}

function GameConnection::endQuest(%client,%string)
{
	messageClient(%client,'',"\c0You have completed the quest \c6" @ %string);
	$VARSAVE::Accounts[%client.BL_ID,"Quests"] = $VARSAVE::Accounts[%client.BL_ID,"Quests"] TAB %string;

}

registerOutputEvent(GameConnection, startQuest, "string 50 50");
registerOutputEvent(GameConnection, endQuest, "string 50 50");