function DRPGShop::buy(%shop,%client,%item,%count)
{
	%shopi = strreplace(%shop.items,"|","\t");
	for(%i=0;%i<getFieldCount(%shopi);%i++)
	{
		%field = getField(%shopi,%i);
		%test = getWord(%field,0);
		%num = getWord(%field,1);
		if(%test $= %item)
		{
			%found = 1;
			if(%num < %count)
			{
				%count = %num;
			}
		}
	}
	if(!%found)
	{
		return;
	}
	%price = ($DRPG::Items::Value[%item] * $DRPG::Prefs::BuyMultiplier) * %count;
	if(%client.hasPlastic(%price))
	{
		if(%client.addItem(%item,%count))
		{
			%client.removePlastic(%price);
			%shop.removeItem(%item,%count);
		}
	}
}
function DRPGShop::sell(%shop,%client,%item,%count)
{
	%price = $DRPG::Items::Value[%item] * %count;
	if(%shop.hasPlastic(%price) && %client.hasItem(%item,%count))
	{
		%shop.addItem(%item,%count);
		%shop.removePlastic(%price);
		%client.addPlastic(%price);
		%client.removeItem(%item,%count);
	}
}
function DRPGShop::open(%shop,%client)
{
	%shopi = strreplace(%shop.items,"|","\t");
	commandToClient(%client,'clearShop');
	for(%i=0;%i<getFieldCount(%shopi);%i++)
	{
		%item = getField(%shopi,%i);
		%count = getWord(%item,1);
		%item = getWord(%item,0);
		%price = $DRPG::Items::Value[%item] * $DRPG::Prefs::BuyMultiplier;
		%item = $DRPG::Items::Name[%item];
		
		commandToClient(%client,'updateShop',%item,%count,%price);
	}
}
function GameConnection::openShop(%client,%nation)
{
	if(!isObject($DRPG::Nations::Shop[%nation]))
	{
		if(isFile("config/server/DRPG/shop_" @ %nation @ ".cs"))
		{
			exec("config/server/DRPG/shop_" @ %nation @ ".cs");
		} else {
			$DRPG::Nations::Shop[%nation] = new ScriptObject() { class = DRPGShop;nation = %nation; };
		}
	}
	$DRPG::Nations::Shop[%nation].open(%client);
}
function DRPGShop::addPlastic(%shop,%amount)
{
	$DRPG::Nations::Wealth[%shop.nation] += %amount;
}
function DRPGShop::hasPlastic(%shop,%amount)
{
	%nation = %shop.nation;
	%free = %shop.plasticProduce;
	%plastic = $DRPG::Nations::Wealth[%nation] + %free;
	if(%plastic > %amount)
	{
		return 1;
	}
	return 0;
}
function DRPGShop::removePlastic(%shop,%amount)
{
	%free = %shop.plasticProduce;
	%freed = mClampF(%amount - %free,0,%amount);
	if(%freed > 0)
	{
		%shop.plasticProduce = 0;
		$DRPG::Nations::Wealth[%shop.nation] -= %freed;
	} else {
		%shop.plasticProduce -= %amount;
	}
}
function DRPGShop::tick(%shop)
{
	%amount = $DRPG::Nations::Wealth[%shop.nation] / $DRPG::Prefs::ProduceRatio;
	%shop.plasticProduce = %amount;
}
function DRPGShop::addItem(%shop,%item,%count)
{
	%shopi = strreplace(%shop.items,"|","\t");
	for(%i=0;%i<getFieldCount(%shopi);%i++)
	{
		%field = getField(%shopi,%i);
		%name = getWord(%field,0);
		%num = getWord(%field,1);
		if(%name $= %item)
		{
			%field = %item SPC (%num + %count);
			return;
		}
	}
	%shopi = trim(%shopi TAB %item SPC %count);
	%shop.items = strreplace(%shopi,"\t","|");
}
function DRPGShop::removeItem(%shop,%item,%count)
{
	%shopi = strreplace(%shop.items,"|","\t");
	%r = 0;
	for(%i=0;%i<getFieldCount(%shopi);%i++)
	{
		%field = getField(%shopi,%i);
		%name = getWord(%field,0);
		%num = getWord(%field,1);
		if(%name $= %item && !%r)
		{
			if(%num > %count)
			{
				%field = %item SPC (%num - %count);
			} else {
				%field = "";
			}
			%r = 1;
		}
		%shopi = trim(%shopi TAB %field);
	}
	%shop.items = strreplace(%shopi,"\t","|");
	return %r;
}