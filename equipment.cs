$DRPG::Equipment::Slot["Head"] = 0;
$DRPG::Equipment::Slot["Pack"] = 1;
$DRPG::Equipment::Slot["Pack2"] = 2;
$DRPG::Equipment::Slot["Body"] = 3;
$DRPG::Equipment::Slot["Tool1"] = 4;
$DRPG::Equipment::Slot["Tool2"] = 5;
$DRPG::Equipment::Slot["Tool3"] = 6;
$DRPG::Equipment::Slot["Tool4"] = 7;
$DRPG::Equipment::Slot["Tool5"] = 8;
$DRPG::Equipment::Slot["Gloves"] = 9;
$DRPG::Equipment::Slot["Boots"] = 10;

package DRPG_Equipment
{
	function GameConnection::spawnPlayer(%client)
	{
		Parent::spawnPlayer(%client);
		%player = %client.player;
		if(isObject(%player))
		{
			%nation = $DRPG::Accounts.value[%client.BL_ID,"nation"];
			if(%nation $= "")
				%player.setShapeNameColor("0.375 0.375 0.375");
			else
				%player.setShapeNameColor($DRPG::Nations::ColorName[%nation]);
			%player.noAppearanceChange = 1;
			%client.applyEquipment();
		}
	}
	function GameConnection::applyBodyParts(%client)
	{
		if(%client.player.noAppearanceChange)
		{
			return;
		}
		Parent::applyBodyParts(%client);
	}
	function GameConnection::applyBodyColors(%client)
	{
		if(%client.player.noAppearanceChange)
		{
			%client.player.setFaceName(%client.faceName);
			return;
		}
		Parent::applyBodyColors(%client);
	}
	function serverCmdDropTool(%client)
	{
		return;
	}
	// Heh
	function Player::canUseAgile(%player)
	{
		%gloves = getfield(strreplace($DRPG::Accounts.value[%player.client.bl_id,"equipment"],"|","\t"),9);
		if(%gloves $= "SPIDER_GLOVES")
		{
			return 1;
		}
		return 0;
	}
	function Player::getClimbEnergyUse(%player)
	{
		return 5;
	}
	function Player::getHorizJumpForce(%player)
	{
		return 10;
	}
	function Player::getVertJumpForce(%player)
	{
		return 8;
	}
};
activatePackage(DRPG_Equipment);

function Player::applyDefault(%player,%slot)
{
	%skinColor = %player.client.headColor;
	switch(%slot)
	{
	case 0:
		// Head
		// Don't hide or unhide anything
	case 1:
		// Pack
		// Don't hide or unhide anything
	case 2:
		// Shoulders
		// Don't hide or unhide anything
	case 3:
		// Body
		%player.unHideNode("chest");
		%player.unHideNode("pants");
		%player.unHideNode("larm");
		%player.unHideNode("rarm");
		%player.setNodeColor("chest",%skinColor);
		%player.setNodeColor("pants",%skinColor);
		%player.setNodeColor("larm",%skinColor);
		%player.setNodeColor("rarm",%skinColor);
		%player.setDecalName("AAA-None");
	case 9:
		// Gloves
		%player.unHideNode("lhand");
		%player.unHideNode("rhand");
		%player.setNodeColor("lhand",%skinColor);
		%player.setNodeColor("rhand",%skinColor);
	case 10:
		// Boots
		%player.unHideNode("lshoe");
		%player.unHideNode("rshoe");
		%player.setNodeColor("lshoe",%skinColor);
		%player.setNodeColor("rshoe",%skinColor);
	}
}
function GameConnection::applyEquipment(%client)
{
	%player = %client.player;
	if(!isObject(%player))
		return;

	%player.hideNode("ALL");
	%player.unHideNode("headSkin");
	%equips = strreplace($DRPG::Accounts.value[%client.bl_id,"equipment"],"|","\t");

	for(%i=0;%i<11;%i++)
	{
		%item = getField(%equips,%i);

		if(%i >= 4 && %i <= 8)
		{
			// Tool slot
			%img = $DRPG::Items::EquipItem[%item];
			if(isObject(%img))
			{
				if(%img.getClassName() !$= "ItemData")
				{
					%img = 0;
				}
			} else {
				%img = 0;
			}
			if(%img !$= "0")
			{
				%img = %img.getID();
			}
			%player.tool[%i - 4] = %img;
			messageClient(%client,'MsgItemPickup',"",%i - 4,%img);
			if(%player.currTool == %i - 4 && %img == 0)
			{
				servercmdunusetool(%client);
			}
			continue;
		}

		if($DRPG::Items::Name[%item] $= "")
		{
			%player.applyDefault(%i);
			continue;
		}
		%nodes = $DRPG::Items::NodeList[%item];
		%colors = $DRPG::Items::NodeColors[%item];
		%nodeCount = getFieldCount(%nodes);
		%colorCount = getFieldCount(%colors);

		if(%colorCount < 1 || %nodeCount < 1)
		{
			%player.applyDefault(%i);
			continue;
		}
		for(%x=0;%x<%nodeCount;%x++)
		{
			%player.unHideNode(getField(%nodes,%x));
			if(%x >= %colorCount)
			{
				%player.setNodeColor(getField(%nodes,%x),getField(%colors,0));
			} else {
				%player.setNodeColor(getField(%nodes,%x),getField(%colors,%x));
			}
		}
		if(%i == 3)
		{
			if($DRPG::Items::Decal[%item] !$= "")
			{
				%player.setDecalName($DRPG::Items::Decal[%item]);
			} else {
				%player.setDecalName("AAA-None");
			}
		}
	}

}
function GameConnection::equip(%client,%item)
{
	if($DRPG::Items::Name[%item] $= "" || !isObject(%player = %client.player))

		return -1;

	%slot = $DRPG::Items::EquipSlot[%item];
	if(%slot $= "" || %slot > 10)
		return -1;

	%equips = strreplace($DRPG::Accounts.value[%client.bl_id,"equipment"],"|","\t");

	if(%slot $= "TOOL")
	{
		for(%i=4;%i<9;%i++)
		{
			%field = getField(%equips,%i);
			if(%field $= "")
			{
				%client.removeItem(%item,1);
				bottomprint(%client,"\c0Equipped \c6" @ $DRPG::Items::Name[%item],2);
				commandToClient(%client,'updateEquipment',%i,$DRPG::Items::Icon[%item],$DRPG::Items::Colour[%item]);
				%equips = setField(%equips,%i,%item);
				%x = 1;
				break;
			}
		}
		if(!%x)
		{
			bottomprint(%client,"\c6You must unequip a tool before you can equip this.",2);
		}
	} else {
		%field = getField(%equips,%slot);
		%client.removeItem(%item,1);
		if(%field !$= "")
		{
			%client.addItem(%field,1);
		}
		%field = %item;
		%equips = setField(%equips,%slot,%field);

		//  * * * NOT SURE HERE * * *
		// %slot is the int slot not the slot name
		// Change if necessary
		bottomprint(%client,"\c0Equipped \c6" @ $DRPG::Items::Name[%item],2);
		commandToClient(%client,'updateEquipment',%slot,$DRPG::Items::Icon[%item],$DRPG::Items::Colour[%item]);
	}

	$DRPG::Accounts.value[%client.bl_id,"equipment"] = strreplace(%equips,"\t","|");

	%client.applyEquipment();
}
function serverCmdUnequip(%client,%slot)
{
	%equips = strreplace($DRPG::Accounts.value[%client.bl_id,"equipment"],"|","\t");
	%field = getField(%equips,%slot);
	if(%field $= "")
		return;

	%rem = %client.addItem(%field,1);
	if(%rem)
	{
		bottomprint(%client,"\c0Unequipped \c6" @ $DRPG::Items::Name[%field],2);
		%equips = setField(%equips,%slot,"");
		commandToClient(%client,'updateEquipment',%slot,"","");
	}
	$DRPG::Accounts.value[%client.bl_id,"equipment"] = strreplace(%equips,"\t","|");
	%client.applyEquipment();
}

// Items moved to content/items_clothes, content/items_weapons and content/items_tools
