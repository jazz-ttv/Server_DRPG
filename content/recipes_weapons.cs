// content/recipes_weapons
// Shortswords
drpgAddRecipe("RECIPE_BRONZE_SWORD","Bronze Sword","Swords","smithing",5,"OAK_WOOD 2\tBRONZE 5","%client.addItem(BRONZE_SWORD,1);",1.1*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_DERMITE_SWORD","Dermite Sword","Swords","smithing",20,"MAPLE_WOOD 2\tDERMITE 5","%client.addItem(DERMITE_SWORD,1);",1.3*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_IRON_SWORD","Iron Sword","Swords","smithing",40,"MAPLE_WOOD 2\tIRON 5","%client.addItem(IRON_SWORD,1);",2.8*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_STEEL_SWORD","Steel Sword","Swords","smithing",60,"YEW_WOOD 2\tSTEEL 5","%client.addItem(STEEL_SWORD,1);",5*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_DESTINITE_SWORD","Destinite Sword","Swords","smithing",75,"MOONWELL_WOOD 2\tDESTINITE 5","%client.addItem(DESTINITE_SWORD,1);",7.6*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_BIRON_SWORD","Black Iron Sword","Swords","smithing",95,"MOONWELL_WOOD 2\tBLACK_IRON 5","%client.addItem(BIRON_SWORD,1);",14*$DRPG::Prefs::ExpMultiplier["smithing"]);

// Darts
drpgAddRecipe("RECIPE_OAK_DART","Darts (Oak)","Archery","fletching",1,"OAK_WOOD 1","%client.addItem(ARROW_DART,1);",0.7*$DRPG::Prefs::ExpMultiplier["fletching"]);
drpgAddRecipe("RECIPE_WILLOW_DART","Darts (Willow)","Archery","fletching",5,"WILLOW_WOOD 1","%client.addItem(ARROW_DART,3);",1.05*$DRPG::Prefs::ExpMultiplier["fletching"]);
drpgAddRecipe("RECIPE_MAPLE_DART","Darts (Maple)","Archery","fletching",15,"MAPLE_WOOD 1","%client.addItem(ARROW_DART,4);",1.39*$DRPG::Prefs::ExpMultiplier["fletching"]);
drpgAddRecipe("RECIPE_YEW_DART","Darts (Yew)","Archery","fletching",40,"YEW_WOOD 1","%client.addItem(ARROW_DART,5);",1.65*$DRPG::Prefs::ExpMultiplier["fletching"]);

// Tipped arrows
drpgAddRecipe("RECIPE_ARROW_BRONZE","Bronze Tipped Arrows","Archery","fletching",5,"ARROW_DART 2\tBRONZE 1","%client.addItem(ARROW_BRONZE,2);",1.05*$DRPG::Prefs::ExpMultiplier["fletching"]);
drpgAddRecipe("RECIPE_ARROW_SILVER","Silver Tipped Arrows","Archery","fletching",15,"ARROW_DART 2\tSILVER 1","%client.addItem(ARROW_SILVER,2);",1.65*$DRPG::Prefs::ExpMultiplier["fletching"]);
drpgAddRecipe("RECIPE_ARROW_IRON","Iron Tipped Arrows","Archery","fletching",25,"ARROW_DART 2\tIRON 1","%client.addItem(ARROW_IRON,2);",3.1*$DRPG::Prefs::ExpMultiplier["fletching"]);
drpgAddRecipe("RECIPE_ARROW_DERMITE","Dermite Tipped Arrows","Archery","fletching",15,"ARROW_DART 2\tDERMITE 1","%client.addItem(ARROW_DERMITE,2);",2.35*$DRPG::Prefs::ExpMultiplier["fletching"]);
drpgAddRecipe("RECIPE_ARROW_STEEL","Steel Tipped Arrows","Archery","fletching",45,"ARROW_DART 2\tSTEEL 1","%client.addItem(ARROW_STEEL,2);",3.8*$DRPG::Prefs::ExpMultiplier["fletching"]);
drpgAddRecipe("RECIPE_ARROW_DESTINITE","Destinite Tipped Arrows","Archery","fletching",65,"ARROW_DART 2\tDESTINITE 1","%client.addItem(ARROW_DESTINITE,2);",5.5*$DRPG::Prefs::ExpMultiplier["fletching"]);

// Bows
drpgAddRecipe("RECIPE_OAK_BOW","Oak Bow","Archery","fletching",1,"OAK_WOOD 5","%client.addItem(OAK_BOW,1);",0.95*$DRPG::Prefs::ExpMultiplier["fletching"]);
drpgAddRecipe("RECIPE_WILLOW_BOW","Willow Bow","Archery","fletching",5,"WILLOW_WOOD 5","%client.addItem(WILLOW_BOW,1);",1.75*$DRPG::Prefs::ExpMultiplier["fletching"]);
drpgAddRecipe("RECIPE_MAPLE_BOW","Maple Bow","Archery","fletching",15,"MAPLE_WOOD 5","%client.addItem(MAPLE_BOW,1);",2.3*$DRPG::Prefs::ExpMultiplier["fletching"]);
drpgAddRecipe("RECIPE_YEW_BOW","Yew Bow","Archery","fletching",40,"YEW_WOOD 5","%client.addItem(YEW_BOW,1);",3.1*$DRPG::Prefs::ExpMultiplier["fletching"]);
drpgAddRecipe("RECIPE_MOONWELL_BOW","Moonwell Bow","Archery","fletching",75,"MOONWELL_WOOD 5","%client.addItem(MOONWELL_BOW,1);",4.8*$DRPG::Prefs::ExpMultiplier["fletching"]);

// Blunderbusses
drpgAddRecipe("RECIPE_BRONZE_BLUNDERBUSS","Bronze Blunderbuss","Archery","smithing",10,"OAK_WOOD 1\tBRONZE 10","%client.addItem(BRONZE_BLUNDERBUSS,1);",1.25*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_DERMITE_BLUNDERBUSS","Dermite Blunderbuss","Archery","smithing",25,"MAPLE_WOOD 1\tDERMITE 10","%client.addItem(DERMITE_BLUNDERBUSS,1);",1.45*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_IRON_BLUNDERBUSS","Iron Blunderbuss","Archery","smithing",45,"MAPLE_WOOD 1\tIRON 10","%client.addItem(IRON_BLUNDERBUSS,1);",2.95*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_STEEL_BLUNDERBUSS","Steel Blunderbuss","Archery","smithing",65,"YEW_WOOD 1\tSTEEL 10","%client.addItem(STEEL_BLUNDERBUSS,1);",5.15*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_DESTINITE_BLUNDERBUSS","Destinite Blunderbuss","Archery","smithing",80,"MOONWELL_WOOD 1\tDESTINITE 10","%client.addItem(DESTINITE_BLUNDERBUSS,1);",7.8*$DRPG::Prefs::ExpMultiplier["smithing"]);

// Bullets
drpgAddRecipe("RECIPE_BULLET_BRONZE","Bronze Bullets","Archery","smithing",9,"BRONZE 1","%client.addItem(BULLET_BRONZE,2);",1.15*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_BULLET_SILVER","Silver Bullets","Archery","smithing",19,"SILVER 1","%client.addItem(BULLET_SILVER,2);",1.75*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_BULLET_IRON","Iron Bullets","Archery","smithing",38,"IRON 1","%client.addItem(BULLET_IRON,2);",3.2*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_BULLET_DERMITE","Dermite Bullets","Archery","smithing",22,"DERMITE 1","%client.addItem(BULLET_DERMITE,2);",2.435*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_BULLET_STEEL","Steel Bullets","Archery","smithing",63,"STEEL 1","%client.addItem(BULLET_STEEL,2);",3.9*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_BULLET_DESTINITE","Destinite Bullets","Archery","smithing",78,"DESTINITE 1","%client.addItem(BULLET_DESTINITE,2);",5.6*$DRPG::Prefs::ExpMultiplier["smithing"]);

//Spears
drpgAddRecipe("RECIPE_WOOD_SPEAR","Wood Spear","Spears","fletching",1,"OAK_WOOD 10","%client.addItem(WOOD_SPEAR,1);",1*$DRPG::Prefs::ExpMultiplier["fletching"]);
drpgAddRecipe("RECIPE_BRONZE_SPEAR","Bronze Spear","Spears","smithing",10,"OAK_WOOD 5\tBRONZE 5","%client.addItem(BRONZE_SPEAR,1);",1.2*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_DERMITE_SPEAR","Dermite Spear","Spears","smithing",30,"MAPLE_WOOD 5\tDERMITE 5","%client.addItem(DERMITE_SPEAR,1);",2*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_IRON_SPEAR","Iron Spear","Spears","smithing",50,"MAPLE_WOOD 5\tIRON 5","%client.addItem(IRON_SPEAR,1);",3.5*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_STEEL_SPEAR","Steel Spear","Spears","smithing",70,"YEW_WOOD 5\tSTEEL 5","%client.addItem(STEEL_SPEAR,1);",6*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_DESTINITE_SPEAR","Destinite Spear","Spears","smithing",90,"MOONWELL_WOOD 5\tDESTINITE 5","%client.addItem(DESTINITE_SPEAR,1);",10*$DRPG::Prefs::ExpMultiplier["smithing"]);
drpgAddRecipe("RECIPE_BIRON_SPEAR","Black Iron Spear","Spears","smithing",100,"MOONWELL_WOOD 5\tBLACK_IRON 5","%client.addItem(BIRON_SPEAR,1);",17*$DRPG::Prefs::ExpMultiplier["smithing"]);