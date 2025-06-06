
//player doesnt need to be set, just do %player.regen
//%health is amount of health regen'd per tick
//%interval is the time between regen ticks
//%total is the total time of regen
//%counter should always be set to 0 when first called
function regen(%player,%health,%interval,%total,%counter)
{
	if(!isObject(%player)) return;
	
	if(%counter < %total)
	{
		%damage = %player.getDamageLevel();
		%player.setDamageLevel(%damage - %health);
		%counter += %interval;
		schedule(%interval,0,regen,%player,%health,%interval,%total,%counter);
	}
}


//same thing as above
function defpotion(%player,%total,%counter)
{
	if(!isObject(%player)) return;

	if(%counter < %total)
	{
		%player.client.defpotion = 1;
		%counter += 1000;
		schedule(1000,0,defpotion,%player,%total,%counter);
	}
	else
		%player.client.defpotion = 0;
}

//same thing as above
function atkpotion(%player,%total,%counter)
{
	if(!isObject(%player)) return;

	if(%counter < %total)
	{
		%player.client.atkpotion = 1;
		%counter += 1000;
		schedule(1000,0,atkpotion,%player,%total,%counter);
	}
	else
		%player.client.atkpotion = 0;
}

//same thing as above
function speedpotion(%player,%total,%counter)
{
	if(!isObject(%player)) return;

	if(%counter < %total)
	{
		%player.client.speedpotion = 1;
		%counter += 1000;
		schedule(1000,0,speedpotion,%player,%total,%counter);
	}
	else
		%player.client.speedpotion = 0;
}