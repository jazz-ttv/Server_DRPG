// Recipes moved content/recipes_misc and content/recipes_weapons

function serverCmdSetRecipesUnlocked(%client, %cond)
{
	if(!%client.isAdmin)
    {
		return;
    }

	if(%cond == 0)
    {
		centerPrint(%client, "Recipes Reset", 3);
		$DRPG::Accounts.value[%client.BL_ID, "recipesUnlocked"] = 0;
	}
	else
    {
		centerPrint(%client, "Recipes Unlocked", 3);
		$DRPG::Accounts.value[%client.BL_ID, "recipesUnlocked"] = 1;
	}
	%client.updateCrafting();
}
function GameConnection::updateCrafting(%client)
{
	for(%a=0;%a<getFieldCount($DRPG::Crafting::RecipeList);%a++)
	{
		%recipe = getField($DRPG::Crafting::RecipeList,%a);
		%skill = $DRPG::Crafting::RecipeSkill[%recipe];
		%level = %client.getSkillLevel(%skill);
		if($DRPG::Crafting::RecipeLevel[%recipe] <= %level || $DRPG::Accounts.value[%client.BL_ID, "recipesUnlocked"] == 1)
		{
			%name = $DRPG::Crafting::RecipeName[%recipe];
			%clevel = strCapitalize(%skill) SPC $DRPG::Crafting::RecipeLevel[%recipe];
			%cat = $DRPG::Crafting::RecipeCategory[%recipe];
			%reqs = "";
			for(%i=0;%i<getFieldCount($DRPG::Crafting::RecipeIngredients[%recipe]);%i++)
			{
				%field = getField($DRPG::Crafting::RecipeIngredients[%recipe],%i);
				%reqs = trim(%reqs TAB $DRPG::Items::Name[getWord(%field,0)] SPC "x" @ getWord(%field,1));
			}
			commandtoclient(%client,'updateCrafting',%recipe,%name,%cat,%clevel,%reqs);
		}
	}
}
function GameConnection::updateSmithing(%client)
{
	%client.updateCrafting();
}
function GameConnection::updateFletching(%client)
{
	%client.updateCrafting();
}
function GameConnection::updateCooking(%client)
{
	%client.updateCrafting();
}
function GameConnection::updateAlchemy(%client)
{
	%client.updateCrafting();
}
function serverCmdCraft(%client,%recipe,%count)
{
	%count = mFloor(%count);
	if(%count < 1)
	{
		%count = 1;
	}
	%skill = $DRPG::Crafting::RecipeSkill[%recipe];
	%level = %client.getSkillLevel(%skill);
	if($DRPG::Crafting::RecipeName[%recipe] $= "" || $DRPG::Crafting::RecipeLevel[%recipe] > %level || !isObject(%client.player))
		return;

	for(%i=0;%i<getFieldCount($DRPG::Crafting::RecipeIngredients[%recipe]);%i++)
	{
		%field = getField($DRPG::Crafting::RecipeIngredients[%recipe],%i);
		%item = getWord(%field,0);
		%num = getWord(%field,1);
		%numitems += %num;
		if(!%client.hasItem(%item,%num))
		{
			centerPrint(%client,"You do not have all the ingredients.",3);
			return 0;
		}
	}
	%time = mClampF((%numitems * 500) - (mFloor(%level / 15) * 100),1000,60000);

	%client.craftCount = %count;
	%client.craftingPos = %client.player.getPosition();
	cancel(%client.craftNext);
	commandToClient(%client,'craftingTime',%time);
	%client.craftNext = %client.schedule(%time,"craft",%recipe);
}
function GameConnection::craft(%client,%recipe)
{
	cancel(%client.craftNext);
	%skill = $DRPG::Crafting::RecipeSkill[%recipe];
	%level = %client.getSkillLevel(%skill);
	if($DRPG::Crafting::RecipeName[%recipe] $= "" || $DRPG::Crafting::RecipeLevel[%recipe] > %level || !isObject(%client.player))
		return;

	%pos = %client.player.getPosition();
	if(%pos !$= %client.craftingPos)
		return;

	for(%i=0;%i<getFieldCount($DRPG::Crafting::RecipeIngredients[%recipe]);%i++)
	{
		%field = getField($DRPG::Crafting::RecipeIngredients[%recipe],%i);
		%item = getWord(%field,0);
		%num = getWord(%field,1);
		%numitems += %num;
		if(!%client.hasItem(%item,%num))
		{
			centerPrint(%client,"You do not have all the ingredients.",3);
			return 0;
		}
	}
	for(%i=0;%i<getFieldCount($DRPG::Crafting::RecipeIngredients[%recipe]);%i++)
	{
		%field = getField($DRPG::Crafting::RecipeIngredients[%recipe],%I);
		%item = getWord(%field,0);
		%num = getWord(%field,1);
		%client.removeItem(%item,%num);
	}
	eval($DRPG::Crafting::RecipeResult[%recipe]);
	%exp = $DRPG::Crafting::RecipeExp[%recipe];

	if(getPerkLevel($DRPG::Accounts.value[%client.BL_ID,nation],CraftingEXP) == 1)
		%exp *= 1.25;
	else if(getPerkLevel($DRPG::Accounts.value[%client.BL_ID,nation],CraftingEXP) == 2)
		%exp *= 1.40;
	else if(getPerkLevel($DRPG::Accounts.value[%client.BL_ID,nation],CraftingEXP) == 3)
		%exp *= 2;

	%client.addExp(%skill,%exp);

	if(%client.craftCount-- > 0)
	{
		%time = mClampF((%numitems * 500) - (mFloor(%client.getSkillLevel(%skill) / 15) * 100),1000,60000);
		commandToClient(%client,'craftingTime',%time);
		%client.craftNext = %client.schedule(%time,"craft",%recipe);
	}
}