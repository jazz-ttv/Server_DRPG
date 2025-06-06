if(!isObject(chestItem))
{
datablock ItemData(chestItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system
	shapeFile = "./shapes/chest.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	uiName = "";
	iconName = "";
	doColorShift = false;
	canDrop = true;
};
}
function chestItem::onPickup(%this,%obj,%player)
{
	if(isObject(%player.client) && %obj.refuse != %player && !isObject(%player.director))
	{
		if(%player.client.addItem(%obj.gives,%obj.amount))
			%obj.delete();
	}
}
