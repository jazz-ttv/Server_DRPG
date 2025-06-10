// content/items_resources.cs
// Wood
drpgAddItem("OAK_WOOD","Oak Wood","A block of oak wood.",1*$DRPG::Prefs::PriceMultiplier["wood"],"log","0.4 0.2 0 1",-1);
drpgAddItem("WILLOW_WOOD","Willow Wood","A block of willow wood.",3*$DRPG::Prefs::PriceMultiplier["wood"],"log","0 0.5 0.25 1",-1);
drpgAddItem("MAPLE_WOOD","Maple Wood","A block of maple wood.",8*$DRPG::Prefs::PriceMultiplier["wood"],"log","0.9 0.3 0.1 1",-1);
drpgAddItem("YEW_WOOD","Yew Wood","A block of yew wood.",19*$DRPG::Prefs::PriceMultiplier["wood"],"log","0.9 0.9 0 1",-1);
drpgAddItem("MOONWELL_WOOD","Moonwell Wood","A block of moonwell wood.",42*$DRPG::Prefs::PriceMultiplier["wood"],"log","1 1 1 1",-1);

// Ore
drpgAddItem("TIN_ORE","Tin Ore","A chunk of tin ore.",1*$DRPG::Prefs::PriceMultiplier["ore"],"ore","0.5 0.5 0.5 1",-1);
drpgAddItem("COPPER_ORE","Copper Ore","A chunk of copper ore.",1*$DRPG::Prefs::PriceMultiplier["ore"],"ore","0.9 0.3 0 1",-1);
drpgAddItem("SILVER_ORE","Silver Ore","A chunk of silver ore.",6*$DRPG::Prefs::PriceMultiplier["ore"],"ore","0.9 0.9 0.9 1",-1);
drpgAddItem("GOLD_ORE","Gold Ore","A chunk of gold ore.",25*$DRPG::Prefs::PriceMultiplier["ore"],"ore","0.9 0.9 0 1",-1);
drpgAddItem("IRON_ORE","Iron Ore","A chunk of iron ore.",38*$DRPG::Prefs::PriceMultiplier["ore"],"ore","0.73 0.62 0.52 1",-1);
drpgAddItem("DERMITE_ORE","Dermite Ore","A chunk of dark dermite ore.",8*$DRPG::Prefs::PriceMultiplier["ore"],"ore","0.3 0 0.1 1",-1);
drpgAddItem("DESTINITE_ORE","Destinite Ore","A chunk of destinite ore.",60*$DRPG::Prefs::PriceMultiplier["ore"],"ore","0.8 0.2 0.5 1",-1);

drpgAddItem("COAL","Coal","A chunk of dusty black coal.",20*$DRPG::Prefs::PriceMultiplier["ore"],"ore","0.1 0.1 0.1 1",-1);
drpgAddItem("OBSIDIAN_COAL","Obsidian Coal","A chunk of deep black coal. You could swear it's humming slightly...",40*$DRPG::Prefs::PriceMultiplier["ore"],"ore","0 0 0 1",-1);

// Bars
drpgAddItem("BRONZE","Bronze","A bar of bronze.",5*$DRPG::Prefs::PriceMultiplier["metal"],"bar","0.4 0.2 0 1",-1);
drpgAddItem("SILVER","Silver","A bar of silver.",18*$DRPG::Prefs::PriceMultiplier["metal"],"bar","0.9 0.9 0.9 1",-1);
drpgAddItem("GOLD","Gold","A bar of gold.",62*$DRPG::Prefs::PriceMultiplier["metal"],"bar","0.9 0.9 0 1",-1);
drpgAddItem("DERMITE","Dermite","A bar of dermite. It got a lot paler during smelting.",20*$DRPG::Prefs::PriceMultiplier["metal"],"bar","0.5 0.3 0.3 1",-1);
drpgAddItem("IRON","Iron","A bar of iron.",80*$DRPG::Prefs::PriceMultiplier["metal"],"bar","0.73 0.62 0.52 1",-1);
drpgAddItem("DESTINITE","Destinite","A bar of destinite.",200*$DRPG::Prefs::PriceMultiplier["metal"],"bar","0.8 0.2 0.5 1",-1);
drpgAddItem("HLEATHER","Hardened Leather","Hardened leather.",50,"bar","0.73 0.62 0.52 1",-1);
drpgAddItem("LEATHER","Leather","A strip of cow hide.",10,"base/client/ui/brickIcons/unknown","1 1 1 1",-1);

drpgAddItem("STEEL","Steel","A bar of steel.",140*$DRPG::Prefs::PriceMultiplier["metal"],"bar","0.8 0.8 0.8 1",-1);
drpgAddItem("BLACK_IRON","Black Iron","A bar of... iron? It's solid black and weighs a ton...",225*$DRPG::Prefs::PriceMultiplier["metal"],"bar","0 0 0 1",-1);

// Seeds
drpgAddItem("WHEAT_SEED","Wheat Seed","A wheat seed. It might sprout when planted.",7*$DRPG::Prefs::PriceMultiplier["seeds"],"seed","0.9 0.9 0.6 1","%client.plantSeed(WHEAT_SEED);");
drpgAddItem("TURNIP_SEED","Turnip Seed","A turnip seed. It might sprout when planted.",38*$DRPG::Prefs::PriceMultiplier["seeds"],"seed","0.8 0.2 0.5 1","%client.plantSeed(TURNIP_SEED);");
drpgAddItem("TOMATO_SEED","Tomato Seed","A tomato seed. It might sprout when planted.",66*$DRPG::Prefs::PriceMultiplier["seeds"],"seed","1 0 0 1","%client.plantSeed(TOMATO_SEED);");
drpgAddItem("CARROT_SEED","Carrot Seed","A carrot seed. It might sprout when planted.",130*$DRPG::Prefs::PriceMultiplier["seeds"],"seed","0.9 0.3 0 1","%client.plantSeed(CARROT_SEED);");
drpgAddItem("DAGORIC_SEED","Dagoric Seed","A dagoric plant seed. It may just bring fear when planted.",500*$DRPG::Prefs::PriceMultiplier["seeds"],"seed","0.3 1 0 1","%client.plantSeed(DAGORIC_SEED);");

// Crops
drpgAddItem("WHEAT","Wheat","A bushel of wheat.",14*$DRPG::Prefs::PriceMultiplier["crops"],"base/client/ui/brickIcons/unknown","1 1 1 1",-1);
drpgAddItem("TURNIP","Turnip","A turnip.",28*$DRPG::Prefs::PriceMultiplier["crops"],"base/client/ui/brickIcons/unknown","1 1 1 1","%client.addExp(\"woodcutting\",0.5);%client.removeItem(TURNIP,1);");
drpgAddItem("TOMATO","Tomato","A tomato.",56*$DRPG::Prefs::PriceMultiplier["crops"],"base/client/ui/brickIcons/unknown","1 1 1 1","%client.addExp(\"mining\",0.5);%client.removeItem(TOMATO,1);");
drpgAddItem("CARROT","Carrot","A carrot. Don't eat it, horses like it.",120*$DRPG::Prefs::PriceMultiplier["crops"],"base/client/ui/brickIcons/unknown","1 1 1 1","if(isObject(%client.player)){if(!%client.player.isRidingHorse()){%client.player.startRidingHorse();%client.removeItem(CARROT,1);}}");
drpgAddItem("DAGORIC","Dagoric Fruit","Spikey ball of Dagoric Fruit. It looks poisonous. When wind blows through, you can hear screams of terror.",250*$DRPG::Prefs::PriceMultiplier["crops"],"base/client/ui/brickIcons/unknown","1 1 1 1","if(isObject(%client.player)){if(!%client.player.isRidingDragon()){%client.player.startRidingDragon();%client.removeItem(DAGORIC,1);}}");

//Cooking
drpgAddItem("TURKEY", "Turkey Meat", "Meat of a turkey.",5,"base/client/ui/brickIcons/unknown","1 1 1 1",-1);
drpgAddItem("COOKED_TURKEY", "Cooked Turkey Meat", "Meat of a turkey. (Cooked)",7.5,"base/client/ui/brickIcons/unknown","1 1 1 1","%client.player.setDamageLevel(%client.player.getDamageLevel() - 10); %client.removeItem(\"COOKED_TURKEY\",\"1\");");
drpgAddItem("MUTTON", "Mutton", "Meat of a goat.",10,"base/client/ui/brickIcons/unknown","1 1 1 1",-1);
drpgAddItem("COOKED_MUTTON", "Cooked Mutton", "Meat of a goat. (Cooked)",15,"base/client/ui/brickIcons/unknown","1 1 1 1","%client.player.setDamageLevel(%client.player.getDamageLevel() - 25); %client.removeItem(\"COOKED_MUTTON\",\"1\");");
drpgAddItem("BEEF", "Beef", "Meat of a cow.",25,"base/client/ui/brickIcons/unknown","1 1 1 1",-1);
drpgAddItem("COOKED_BEEF", "Cooked Beef", "Meat of a cow. (Cooked)",35,"base/client/ui/brickIcons/unknown","1 1 1 1","%client.player.setDamageLevel(%client.player.getDamageLevel() - 50); %client.removeItem(\"COOKED_BEEF\",\"1\");");
drpgAddItem("RAM_MEAT", "Ram Meat", "Meat of a ram. Gross.",35,"base/client/ui/brickIcons/unknown","1 1 1 1",-1);
drpgAddItem("COOKED_RAM", "Cooked Ram Meat", "Meat of a ram. Gross. (Cooked)",50,"base/client/ui/brickIcons/unknown","1 1 1 1","%client.player.setDamageLevel(%client.player.getDamageLevel() - 100); %client.removeItem(\"COOKED_RAM\",\"1\");");
drpgAddItem("BULL_MEAT", "Bull Meat", "Meat of a bull. Gross. Unable to Cook.",65,"base/client/ui/brickIcons/unknown","1 1 1 1",-1);
drpgAddItem("FLOUR","Flour","A pack of flour.",8*$DRPG::Prefs::PriceMultiplier["crops"],"base/client/ui/brickIcons/unknown","1 1 1 1",-1);

//Herbs
drpgAddItem("LIMBER_HERB","Limber Root","A root used for regeneration potions.",5*$DRPG::Prefs::PriceMultiplier["crops"], "Add-Ons/Brick_Plant/1x1stem", "1 1 1 1",-1);
drpgAddItem("ROSIS_HERB","Rosis Flower","A flower used for regeneration potions.",10*$DRPG::Prefs::PriceMultiplier["crops"], "Add-Ons/Brick_Plant/1x1flowers", "1 1 1 1",-1);
drpgAddItem("ACCURIS_HERB","Accuris Flower","A flower used for speed potions.",25*$DRPG::Prefs::PriceMultiplier["crops"], "Add-Ons/Brick_Plant/2x2flower", "1 1 1 1",-1);
drpgAddItem("DEFTORA_HERB","Deftora Grass","Grass used for defense potions.",50*$DRPG::Prefs::PriceMultiplier["crops"], "Add-Ons/Brick_Plant/seaweed", "1 1 1 1",-1);
drpgAddItem("DAMAC_HERB","Damac Petals","Petals used for attack potions.",75*$DRPG::Prefs::PriceMultiplier["crops"], "Add-Ons/Brick_Plant/2x2petals", "1 1 1 1",-1);
drpgAddItem("GUNDOR_HERB","Gundor Leaves","Leaves used for speed potions",100*$DRPG::Prefs::PriceMultiplier["crops"], "Add-Ons/Brick_Plant/3x4leaves", "1 1 1 1",-1);

//Potions
drpgAddItem("ATTACK_POTION","Attack Potion","Used to increase damage output.",50,"base/client/ui/brickIcons/unknown","1 1 1 1","atkpotion(%client.player,180000,0); %client.removeItem(\"ATTACK_POTION\",\"1\");");
drpgAddItem("DEFENSE_POTION","Defense Potion","Used to decrease damage taken.",50,"base/client/ui/brickIcons/unknown","1 1 1 1","defpotion(%client.player,180000,0); %client.removeItem(\"DEFENSE_POTION\",\"1\");");
drpgAddItem("SPEED_POTION","Speed Potion","Used to decrease mining/woodcutting time.",50,"base/client/ui/brickIcons/unknown","1 1 1 1","speedpotion(%client.player,180000,0); %client.removeItem(\"SPEED_POTION\",\"1\");");
drpgAddItem("REGEN_POTION","Regen Potion","Used to regenerate health.",50,"base/client/ui/brickIcons/unknown","1 1 1 1","regen(%client.player,10,10000,180000,0); %client.removeItem(\"REGEN_POTION\",\"1\");");

drpgAddItem("FEATHER_DROP","Feather","A useless feather.",15,"base/client/ui/brickIcons/unknown","1 1 1 1",-1);
drpgAddItem("HORN_DROP","Ram Horn","A useless horn.",50,"base/client/ui/brickIcons/unknown","1 1 1 1",-1);
drpgAddItem("ROTTEN_MEAT_DROP","Rotten Meat","A pile of useless rotten meat.",1,"base/client/ui/brickIcons/unknown","1 1 1 1",-1);

drpgAddItem("SCROLL_SKELE","Scroll of SKELE","A Scroll that summons a SKELE of Elrad",300,"base/client/ui/brickIcons/unknown","0.9 0.9 0.6 1","spawnSKELE(%client);");

drpgAddItem("BLACK_MIST","Black Mist","When the wind blows through, it whispers 'Long Live Elrad!'",1,"base/client/ui/brickIcons/unknown","0 0 0 1",-1);