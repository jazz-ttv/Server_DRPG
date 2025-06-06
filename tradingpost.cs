// Dysfuckthisshit
return;

function DRPGTradingPost::addListing(%this,%item,%count,%priceper,%client)
{
	%id = %client.bl_id;
	%name = %client.getPlayerName();
	if(%priceper < 1)
	{
		commandToClient(%client,'DRPG_TPMsg',"You must enter a valid price per unit.");
		return;
	}
	if(%client.hasItem(%item,%count))
	{
		%client.removeItem(%item,%count);
		%this.listingName[%this.listings] = %name;
		%this.listingID[%this.listings] = %id;
		%this.listingItem[%this.listings] = %item;
		%this.listingCount[%this.listings] = %count;
		%this.listingPrice[%this.listings] = %priceper;
		%this.listings++;
		commandToClient(%client,'DRPG_TPMsg',"Your listing has been added.");
	} else {
		commandToClient(%client,'DRPG_TPMsg',"You do not have that many of that item to sell.");
	}
}
function DRPGTradingPost::buyListing(%this,%id,%client)
{
	%price = %this.listingPrice[%id] * %this.listingCount[%id];
	if($DRPG::Accounts.value[%client.bl_id,"plastic"] >= %price && %price > 0)
	{
		if(%client.addItem(%this.listingItem[%id],%this.listingCount[%id]))
		{
			%client.removePlastic(%price);
			commandToClient(%client,'DRPG_TPMsg',"You have purchased " @ %this.listingCount[%id] SPC %this.listingItem[%id] @ ".");
			if(isObject(%tar = findClientByBl_id(%this.listingID[%id])))
			{
				messageClient(%tar,'',"\c3" @ %client.getPlayerName() SPC "\c6purchased your listing for" SPC %this.listingCount[%id] SPC %this.listingItem[%id] @ ".");
			}
			%this.removeListing(%id);
		} else {
			commandToClient(%client,'DRPG_TPMsg',"You do not have enough inventory space to do that.");
		}
	} else {
		commandToClient(%client,'DRPG_TPMsg',"You cannot afford to purchase this listing.");
	}
}
function DRPGTradingPost::removeListing(%this,%id)
{
	for(%i=%id;%i<%this.listings;%i++)
	{
		%this.listingName[%i] = %this.listingName[%i + 1];
		%this.listingID[%i] = %this.listingID[%i + 1];
		%this.listingItem[%i] = %this.listingItem[%i + 1];
		%this.listingCount[%i] = %this.listingCount[%i + 1];
		%this.listingPrice[%i] = %this.listingPrice[%i + 1];
	}
	%this.listings--;
}
function DRPGTradingPost::cancelListing(%this,%id,%client)
{
	if(%client.bl_id == %this.listingID[%id])
	{
		if(%client.addItem(%this.listingItem[%id],%this.listingCount[%id]))
		{
			commandToClient(%client,'DRPG_TPMsg',"Your listing has been removed.");
			%this.removeListing(%id);
		} else {
			commandToClient(%client,'DRPG_TPMsg',"You do not have enough inventory space to do that.");
		}
	} else {
		commandToClient(%client,'DRPG_TPMsg',"That listing does not belong to you.");
	}
}

function serverCmdTPBuyCancelListing(%client,%id)
{
	if(isObject(DRPGTradingPost))
	{
		DRPGTradingPost.buyCancelListing(%id,%client);
	}
}
function serverCmdTPAddListing(%client,%item,%count,%price)
{
	if(isObject(DRPGTradingPost))
	{
		DRPGTradingPost.addListing(%item,%count,%price,%client);
	}
}
function serverCmdDRPG_TPGetListings(%client,%start)
{
	if(%start >= DRPGTradingPost.listings || !isObject(DRPGTradingPost))
	{
		%cl.tradingPostStart = 0;
		return;
	}
	%cl.tradingPostStart = %start;
	%tp = DRPGTradingPost;
	for(%i=%start;%i<%start + 50;%i++)
	{
		commandToClient(%client,'DRPG_TPList',%i,%tp.listingItem[%i],%tp.listingCount[%i],%tp.listingPrice[%i],%tp.listingName[%i]);
	}
}
function GameConnection::openTradingPost(%cl)
{
	if(!isObject(DRPGTradingPost))
	{
		return;
	}
	%cl.tradingPostStart = 0;
	commandToClient(%cl,'DRPG_TPUpdate',DRPGTradingPost.listings);
}