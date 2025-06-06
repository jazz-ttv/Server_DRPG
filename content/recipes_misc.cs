// content/recipes_misc
// Metals
drpgAddRecipe("RECIPE_BRONZE","Bronze","Materials","smithing",1,"TIN_ORE 1\tCOPPER_ORE 1","%client.addItem(BRONZE,1);",0.7*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_SILVER","Silver","Materials","smithing",10,"SILVER_ORE 2","%client.addItem(SILVER,1);",1.2*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_GOLD","Gold","Materials","smithing",25,"GOLD_ORE 2","%client.addItem(GOLD,1);",2*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_DERMITE","Dermite","Materials","smithing",15,"DERMITE_ORE 2","%client.addItem(DERMITE,1);",4*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_IRON","Iron","Materials","smithing",40,"IRON_ORE 2","%client.addItem(IRON,1);",5*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_DESTINITE","Destinite","Materials","smithing",70,"DESTINITE_ORE 2","%client.addItem(DESTINITE,1);",9*$DRPG::Prefs::ExpMultiplier["smithing"]);

drpgAddRecipe("RECIPE_STEEL","Steel","Materials","smithing",60,"IRON 1\tCOAL 2","%client.addItem(STEEL,1);",6.5*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_BLACK_IRON","Black Iron","Materials","smithing",90,"IRON 2\tCOAL 2\tOBSIDIAN_COAL 1","%client.addItem(BLACK_IRON,1);",11*$DRPG::Prefs::ExpMultiplier["smithing"]);

// Crops
drpgAddRecipe("RECIPE_FLOUR","Flour","Materials","cooking",1,"WHEAT 2","%client.addItem(FLOUR,1);",1*$DRPG::Prefs::ExpMultiplier["cooking"]);
drpgAddRecipe("RECIPE_COOKED_TURKEY","Cooked Turkey","Meat","cooking",10,"TURKEY 1\tOAK_WOOD 1","%client.addItem(COOKED_TURKEY,1);",2*$DRPG::Prefs::ExpMultiplier["cooking"]);
drpgAddRecipe("RECIPE_COOKED_MUTTON","Cooked Mutton","Meat","cooking",25,"MUTTON 1\tOAK_WOOD 1","%client.addItem(COOKED_MUTTON,1);",4*$DRPG::Prefs::ExpMultiplier["cooking"]);
drpgAddRecipe("RECIPE_COOKED_BEEF","Cooked Beef","Meat","cooking",40,"BEEF 1\tOAK_WOOD 1","%client.addItem(COOKED_BEEF,1);",7*$DRPG::Prefs::ExpMultiplier["cooking"]);
drpgAddRecipe("RECIPE_COOKED_RAM","Cooked Ram","Meat","cooking",55,"RAM_MEAT 1\tOAK_WOOD 1","%client.addItem(COOKED_RAM,1);",10*$DRPG::Prefs::ExpMultiplier["cooking"]);

// Potions

drpgAddRecipe("RECIPE_REGEN_POTION","Regen Potion","Potions","alchemy",0,"ROSIS_HERB 1\tLIMBER_HERB 1","%client.addItem(REGEN_POTION,1);",1.4);
drpgAddRecipe("RECIPE_SPEED_POTION","Speed Potion","Potions","alchemy",5,"ACCURIS_HERB 1\tGUNDOR_HERB 1","%client.addItem(SPEED_POTION,1);",2.5);
drpgAddRecipe("RECIPE_DEFENSE_POTION","Defense Potion","Potions","alchemy",10,"DEFTORA_HERB 1","%client.addItem(DEFENSE_POTION,1);",3.5);
drpgAddRecipe("RECIPE_ATTACK_POTION","Attack Potion","Potions","alchemy",15,"DAMAC_HERB 1","%client.addItem(ATTACK_POTION,1);",4.75);

drpgAddRecipe("RECIPE_HLEATHER","Hardened Leather","Materials","smithing",80,"LEATHER 5\tCOAL 1","%client.addItem(HLEATHER,1);",1.4);