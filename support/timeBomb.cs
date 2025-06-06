function minToMS(%mins)
{
	return (%mins * 1000) * 60;
}
function msToMin(%ms)
{
	return (%ms / 1000) / 60;
}
function timeBomb::onAdd(%this)
{
	if(%this.requiredTicks $= "" || %this.tickMinutes $= "")
		return %this.delete();
	%this.ticks = 0;
	if(%this.go)
		%this.start();
}
function timeBomb::onRemove(%this)
{
	if(isEventPending(%this.tick))
		cancel(%this.tick);
	%this.onDestroy(getSimTime());
}
function timeBomb::start(%this)
{
	if(isEventPending(%this.tick))
	{
		cancel(%this.tick);
		%this.onHalt(getSimTime());
		%this.ticks = 0;
	}
	%this.onStart(getSimTime());
	%this.tick = %this.schedule(minToMS(%this.tickMinutes),"process");
	return 1;
}
function timeBomb::process(%this)
{
	if(isEventPending(%this.tick))
	{
		cancel(%this.tick);
	}
	%this.ticks++;
	if(%this.ticks >= %this.requiredTicks)
	{
		%this.ticks = 0;
		%this.onTick(getSimTime());
	}
	%this.tick = %this.schedule(minToMS(%this.tickMinutes),"process");
	return 1;
}
function timeBomb::end(%this)
{
	if(isEventPending(%this.tick))
	{
		cancel(%this.tick);
		%this.onHalt(getSimTime());
		%this.ticks = 0;
		return 1;
	}
	return 0;
}
function timeBomb::onStart(%this,%stamp)
{
	return;
}
function timeBomb::onTick(%this,%stamp)
{
	return;
}
function timeBomb::onHalt(%this,%stamp)
{
	return;
}
function timeBomb::onDestroy(%this,%stamp)
{
	return;
}