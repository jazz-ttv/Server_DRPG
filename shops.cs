registerOutputEvent(GameConnection,"openShop","string 200 200");
registerOutputEvent(GameConnection,"closeShop","");
function serverCmdBuy(%client,%item,%count)
{
	%count = mFloor(%count);
	if(%count < 1)
	{
		%count = 1;
	}
	if(%client.lastBuyingTime+0.05 > getSimTime()/1000)
		return;
	if(!%client.inShop)	
		return;
	for(%i=0;%i<getFieldCount(%client.shopItems);%i++)
	{
		%data = getField(%client.shopItems,%i);
		if(%item $= %data)
			%found = 1;
	}
	%cost = $DRPG::Items::Value[%item] * $DRPG::Prefs::BuyMultiplier;
	if(%found)
	{
		if(!%client.hasPlastic(%cost * %count))
		{
			centerPrint(%client,"You don't have enough plastic!",3);
			return;
		}
		%client.removePlastic(%cost * %count);
		%client.addItem(%item,%count);
		%client.lastBuyingTime = getSimTime()/1000;
	}
}
function serverCmdSell(%client,%slot,%count)
{
	if(!%client.inShop)	
		return;
	%count = mFloor(%count);
	if(%count < 1)
	{
		%count = 1;
	}
	%inventory = strReplace($DRPG::Accounts.value[%client.BL_ID,"inventory"],"|","\t");
	%item = getField(%inventory,%slot);
	if(%item $= "")
		return;
	%num = mClampF(getWord(%item,1),1,%count);
	%plastic = $DRPG::Items::Value[getWord(%item,0)] * %num;
	%client.addPlastic(%plastic);
	%item = setWord(%item,1,getWord(%item,1) - %count);
	if(getWord(%item,1) <= 0)
	{
		%item = "";
	}
	%inventory = setField(%inventory,%slot,%item);
	$DRPG::Accounts.value[%client.BL_ID,"inventory"] = strReplace(%inventory,"\t","|");
	%client.updateInventory();
}
function GameConnection::openShop(%client,%items)
{
	%items = strReplace(%items,"|","\t");
	commandtoclient(%client,'clearshop');
	for(%i=0;%i<getFieldCount(%items);%i++)
	{
		commandtoclient(%client,'updateshop',getField(%items,%i),$DRPG::Items::Name[getField(%items,%i)],$DRPG::Items::Value[getField(%items,%i)] * $DRPG::Prefs::BuyMultiplier);
	}
	commandtoclient(%client,'openshop');
	%client.shopItems = %items;
	%client.inShop = 1;
}
function serverCmdCloseShop(%client)
{
	%client.inShop = 0;
}
function GameConnection::closeShop(%client)
{
	commandtoclient(%client,'closeshop');
}