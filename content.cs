// Content plugin interface. So we don't have to do all that ridiculous shit

// Weapons/Tools
function drpgAddTool(%tag,%name,%desc,%value,%icon,%item)
{
	if(!isObject(%item) || %item.getClassName() !$= "ItemData")
	{
		return 0;
	}
	$DRPG::Items::Name[%tag] = %name;
	$DRPG::Items::Desc[%tag] = %desc;
	$DRPG::Items::Use[%tag] = "%client.equip(" @ %tag @ ");";
	$DRPG::Items::Value[%tag] = %value;
	$DRPG::Items::Icon[%tag] = %icon;
	$DRPG::Items::Colour[%tag] = %item.colorShiftColor;
	$DRPG::Items::EquipSlot[%tag] = "TOOL";
	$DRPG::Items::EquipItem[%tag] = %item;

	return 1;
}

// Armor/Clothing
function drpgAddArmor(%tag,%name,%desc,%value,%icon,%color,%slot,%nodes,%colors,%decal,%def)
{
	if(getFieldCount(%nodes) == 0 || getFieldCount(%colors) == 0 || (%slot < 0 || (%slot >= 4 && %slot <= 8) || %slot > 10))
	{
		return 0;
	}
	$DRPG::Items::Name[%tag] = %name;
	$DRPG::Items::Desc[%tag] = %desc;
	$DRPG::Items::Use[%tag] = "%client.equip(" @ %tag @ ");";
	$DRPG::Items::Value[%tag] = %value;
	$DRPG::Items::Icon[%tag] = %icon;
	$DRPG::Items::Colour[%tag] = %color;
	$DRPG::Items::EquipSlot[%tag] = %slot;
	$DRPG::Items::NodeList[%tag] = %nodes;
	$DRPG::Items::NodeColors[%tag] = %colors;
	if(stripos(%nodes,"chest") != -1 || stripos(%nodes,"femchest") != -1)
	{
		$DRPG::Items::Decal[%tag] = %decal;
		$DRPG::Items::EquipDefense[%tag] = %def;
	} else {
		$DRPG::Items::EquipDefense[%tag] = %decal;
	}

	return 1;
}

// Crafting Recipes
function drpgAddRecipe(%tag,%name,%cat,%skill,%level,%ingredients,%result,%exp)
{
	%str = "\t" @ $DRPG::Crafting::RecipeList @ "\t";
	if(stripos(%str,"\t" @ %tag @ "\t") != -1 || getFieldCount(%ingredients) == 0)
	{
		return 0;
	}
	$DRPG::Crafting::RecipeName[%tag] = %name;
	$DRPG::Crafting::RecipeSkill[%tag] = %skill;
	$DRPG::Crafting::RecipeLevel[%tag] = %level;
	$DRPG::Crafting::RecipeCategory[%tag] = %cat;
	$DRPG::Crafting::RecipeIngredients[%tag] = %ingredients;
	$DRPG::Crafting::RecipeResult[%tag] = %result;
	$DRPG::Crafting::RecipeEXP[%tag] = %exp;

	$DRPG::Crafting::RecipeList = trim($DRPG::Crafting::RecipeList TAB %tag);

	return 1;
}

// Ordinary items
function drpgAddItem(%tag,%name,%desc,%value,%icon,%color,%use)
{
	if(%use $= "")
	{
		%use = -1;
	}
	$DRPG::Items::Name[%tag] = %name;
	$DRPG::Items::Desc[%tag] = %desc;
	$DRPG::Items::Value[%tag] = %value;
	$DRPG::Items::Icon[%tag] = %icon;
	$DRPG::Items::Colour[%tag] = %color;
	$DRPG::Items::Use[%tag] = %use;

	return 1;
}

// Ammo
function drpgAddAmmo(%tag,%name,%desc,%value,%color,%type,%projectile)
{
	if(!isObject(%projectile) || %projectile.getClassName() !$= "ProjectileData")
	{
		return 0;
	}
	%icon = "Add-Ons/Server_DRPG/icons/ammo_" @ %type @ ".png";
	if(!isFile(%icon))
	{
		%icon = "base/client/ui/brickIcons/unknown.png";
	}
	$DRPG::Items::Name[%tag] = %name;
	$DRPG::Items::Desc[%tag] = %desc;
	$DRPG::Items::Use[%tag] = "%client.equip(" @ %tag @ ");";
	$DRPG::Items::Value[%tag] = %value;
	$DRPG::Items::Icon[%tag] = %icon;
	$DRPG::Items::Colour[%tag] = %color;
	$DRPG::Items::EquipSlot[%tag] = 1;
	$DRPG::Items::NodeList[%tag] = "quiver";
	$DRPG::Items::NodeColors[%tag] = "0.4 0.196 0 1";
	$DRPG::Items::AmmoProjectile[%tag] = %projectile;
	$DRPG::Items::AmmoType[%tag] = %type;

	return 1;
}
//fuck you americans for fucking up my grammar. it's colour not color jesus christ
function drpgAddCrop(%shape,%name,%level,%gives,%source,%exp,%growSize,%growSpeed,%plantCount,%plantRadius,%plantVar,%scale,%threshold,%color)
{
	if(isObject(nameToID("DRPGFarming_" @ %name)) || $DRPG::Crops::Shape[%name] !$= "")
		return 0;
	%shape = "Add-Ons/Server_DRPG/shapes/" @ %shape @ ".dts";
	if(!isFile(%shape))
		return 0;
	$DRPG::Crops::Shape[%name] = %shape;
	$DRPG::Crops::RequiredLevel[%name] = %level;
	$DRPG::Crops::Gives[%name] = %gives;
	$DRPG::Crops::Source[%name] = %source;
	$DRPG::Crops::EXP[%name] = %exp;
	$DRPG::Crops::GrowSize[%name] = %growSize;
	$DRPG::Crops::GrowSpeed[%name] = %growSpeed;
	$DRPG::Crops::PlantCount[%name] = %plantCount;
	$DRPG::Crops::PlantRadius[%name] = %plantRadius;
	$DRPG::Crops::PlantVar[%name] = %plantVar;
	$DRPG::Crops::Scale[%name] = %scale;
	$DRPG::Crops::Threshold[%name] = %threshold;
	$DRPG::Crops::Color[%name] = %color;
	datablock StaticShapeData(DRPGFarming_Crop)
	{
		shapefile = %shape;
		DRPG_isCrop = 1;
		DRPG_requiredLevel = %level;
		DRPG_gives = %gives;
		DRPG_source = %source;
		DRPG_exp = %exp;
		growSize = %growSize;
		growSpeed = %growSpeed;
		plantCount = %plantCount;
		plantRadius = %plantRadius;
		plantVar = %plantVar;
		initScale = %scale;
		threshold = %threshold;
		color = %color;
	};
	if(!isObject(DRPGFarming_Crop))
		return 0;
	$DRPG::Crops::CropFromSeed[%source] = DRPGFarming_Crop.getID();
	%safeName = strReplace(%name," ","_");
	DRPGFarming_Crop.setName("DRPGFarming_" @ %safeName);
	return 1;
}

// Now, execute all the fukken content files!
for(%file = findFirstFile("./content/*.cs");isFile(%file);%file = findNextFile("./content/*.cs"))
{
	exec(%file);
}
for(%file = findFirstFile("config/server/DRPG/content/*.cs");isFile(%file);%file = findNextFile("config/server/DRPG/content/*.cs"))
{
	exec(%file);
}