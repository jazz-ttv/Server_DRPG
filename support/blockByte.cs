//------
//blockByte v0.9
//Fast and simple database system by Zack "Zack0Wack0" Corr
//Free to use - just give credits where applicable
//---EVENTS---
//blockByte::onAdd (EVENT)
//"When the database object is created"
//%this (blockByte): The database object that was just created
//Zack0Wack0 11:17 PM 11/01/2011
function blockByte::onAdd(%this)
{
	if(isFile(%this.dataBaseFile))
		%this.importDatabase(%this.dataBaseFile,%this.dataBaseFormat);
	if(%this.doAutoSave)
		%this.autoSave(1);
}
//blockByte::onRemove (EVENT)
//"When the database object is removed"
//%this (blockByte): The database object that is being removed
//Zack0Wack0 11:17 PM 11/01/2011
function blockByte::onRemove(%this)
{
	cancel(%this.autoSave);
}
//blockByte::onException (EVENT)
//"When the database object is thrown an exception ever by the third party or the original code"
//%this (blockByte): The database object that the exception was called on
//%type (string): Exception type (can be anything but preferably keep to the default ones)
//%msg (string): Exception message (a detailed description of the exception and why it was caused)
//Zack0Wack0 1:15 AM 12/01/2011
function blockByte::onException(%this,%type,%msg)
{
	if(%this.debug)
		echo("\c2blockByte Exception (" @ %type @ "): \c1" @ %msg);
}
//---EXCEPTIONS---
//General
//ValueAlreadyExists
//ValueNonExistant
//KeyAlreadyExists
//KeyNonExistant
//FileNonExistant
//FileUnwritable
//DatabaseIsBlank
//!PROTIP: CHECK THE RETURN VALUE OF EVERY DATABASE FUNCTION YOU CALL FOR ADDITIONAL ERROR INFORMATION. ALL FUNCTIONS RETURN 0 ON FAIL, 1 ON SUCCESS AND -1 FOR WHAT THE FUCK HAPPENED.!
//blockByte::throwException (METHOD)
//"Force an exception on the database (which will call the onException event obviously)"
//%this (blockByte): The database object that the exception is being called on
//%type (string): Exception type (can be anything but preferably keep to the default ones)
//%msg (string): Exception message (a detailed description of the exception and why it was caused)
//Zack0Wack0 1:17 AM 12/01/2011
function blockByte::throwException(%this,%type,%msg)
{
	%this.lastExceptionType = %type;
	%this.lastExceptionMsg = %msg;
	%this.onException(%type,%msg);
}
//---AUTOSAVE---
//blockByte::autoSave (METHOD)
//"A loop that saves the database (databaseFile, databaseFormat and doAutoSave must be set to actually do anything)"
//%this (blockByte): The database
//%force (bool): Was this command called just to start the loop?
//Zack0Wack0 11:20 PM 11/01/2011
function blockByte::autoSave(%this,%force)
{
	cancel(%this.autoSave);
	if(%this.doAutoSave)
	{	
		if(!%force)
			%this.exportDatabase(%this.dataBaseFile,%this.dataBaseFormat);
		%this.autoSave = %this.schedule(%this.autoSaveInterval,"autoSave");
	}
}
//---VALUES---
//blockByte::addValue (METHOD)
//"Adds a value (column) to the database (duplicates are not allowed)"
//%this (blockByte): The database values are to be added to
//%value (string): The name of the value that is to be added
//%default (any): The default value to be set when a key is added to the database
//Zack0Wack0 11:24 PM 11/01/2011
function blockByte::addValue(%this,%value,%default)
{
	if(%this.valueExists(%value))
	{
		%this.throwException(ValueAlreadyExists,"An attempt to add value '" @ %value @ "' failed. A value with the same name already exists in this blockByte database.");
		return 0;
	}
	if(%this.valueList $= "")
		%this.valueList = %value;
	else
		%this.valueList = %this.valueList TAB %value;
	%this.valueDefault[%value] = %default;
	return 1;
}
//blockByte::valueExists (METHOD)
//"Check if a value (column) already exists on the database"
//%this (blockByte): The database object to check the value at
//%value (string): Name of the value to check
//Zack0Wack0 11:26 PM 11/01/2011
function blockByte::valueExists(%this,%value)
{
	for(%i=0;%i<getFieldCount(%this.valueList);%i++)
	{
		if(getField(%this.valueList,%i) $= %value)
			return 1;
	}
	return 0;
}
//blockByte::updateValues (METHOD)
//"Loops through database and updates any blank key values"
//%this (blockByte): The database
function blockByte::updateValues(%this)
{
	for(%i=0;%i<getFieldCount(%this.keyList);%i++)
	{
		%key = getField(%this.keyList,%i);
		for(%v=0;%v<getFieldCount(%this.valueList);%v++)
		{
			%value = getField(%this.valueList,%v);
			%default = %this.valueDefault[%value];
			if(%this.value[%key,%value] $= "" && %default !$= "")
				%this.value[%key,%value] = %default;
		}
	}
}
//blockByte::removeValue (METHOD)
//"Remove a value (column) from the database"
//%this (blockByte): The database values are to be removed from
//%value (string): Name of the value to remove
//Zack0Wack0 11:27 PM 11/01/2011
function blockByte::removeValue(%this,%value)
{
	if(!%this.valueExists(%value))
	{
		%this.throwException(ValueNonExistant,"An attempt to remove value '" @ %value @ "' failed. A value with this name does not exist in this blockByte database.");
		return 0;
	}
	for(%i=0;%i<getFieldCount(%this.valueList);%i++)
	{
		if(getField(%this.valueList,%i) $= %value)
		{
			if(%i $= 0)
				%this.valueList = getFields(%this.valueList,1,getFieldCount(%this.valueList));
			else if(%i $= getFieldCount(%this.valueList)-1)
				%this.valueList = getFields(%this.valueList,0,%i-1);
			else
				%this.valueList = getFields(%this.valueList,0,%i-1) TAB getFields(%this.valueList,%i+1,getFieldCount(%this.valueList));
			return 1;
		}
	}
	return 0;
}
//blockByte::wipeValue (METHOD)
//"Completely removes all existance of the value (column) in the database, including from all keys (rows)
//%this (blockByte): The database
//%value (string): Name of the value to wipe
//%handler (function): If not null this will be called with the old value and key so you can basically add a new value. You probably don't need this, but me and Clock needed it for DRPG.
function blockByte::wipeValue(%this,%value,%handler)
{
	if(!%this.valueExists(%value))
	{
		%this.throwException(ValueNonExistant,"An attempt to wipe value '" @ %value @ "' failed. A value with this name does not exist in this blockByte database.");
		return 0;
	}
	if(%this.removeValue(%value))
	{
		for(%i=0;%i<getFieldCount(%this.keyList);%i++)
		{
			%key = getField(%this.keyList,%i);
			if(%this.value[%key,%value] !$= "")
			{
				%old = %this.value[%key,%value];
				%this.value[%key,%value] = "";
				if(isFunction(%handler))
					call(%handler,%key,%old);
			}
		}
		return 1;
	}
	return 0;
}
//---KEYS---
//blockByte::addKey (METHOD)
//"Adds a key (row) to the database"
//%this (blockByte): The database the key is to be added to
//%key (any): The key to be added to the database
//Zack0Wack0 11:29 PM 11/01/2011
function blockByte::addKey(%this,%key)
{
	if(%this.keyExists(%key))
	{
		%this.throwException(KeyAlreadyExists,"An attempt to add key '" @ %key @ "' failed. That key already exists in this blockByte database.");
		return 0;
	}
	if(%this.keyList $= "")
		%this.keyList = %key;
	else
		%this.keyList = %this.keyList TAB %key;
	for(%i=0;%i<getFieldCount(%this.valueList);%i++)
		%this.value[%key,getField(%this.valueList,%i)] = %this.valueDefault[getField(%this.valueList,%i)];
	return 1;
}
//blockByte::keyExists (METHOD)
//"Checks if a key (row) already exists in the database"
//%this (blockByte): The database the key is to be checked with
//%key (any): The key to be checked with the database
//Zack0Wack0 11:30 PM 11/01/2011
function blockByte::keyExists(%this,%key)
{
	for(%i=0;%i<getFieldCount(%this.keyList);%i++)
	{
		if(getField(%this.keyList,%i) $= %key)
			return 1;
	}
	return 0;
}
//blockByte::removeKey (METHOD)
//"Removes a key (row) from the database"
//%this (blockByte): The database the key is to be removed from
//%key (any): The key to be removed from the database
//Zack0Wack0 11:32 PM 11/01/2011
function blockByte::removeKey(%this,%key)
{
	if(!%this.keyExists(%key))
	{
		%this.throwException(KeyNonExistant,"An attempt to remove key '" @ %key @ "' failed. That key does not exist in this blockByte database.");
		return 0;
	}
	for(%i=0;%i<getFieldCount(%this.keyList);%i++)
	{
		if(getField(%this.keyList,%i) $= %key)
		{
			if(%i $= 0)
				%this.keyList = getFields(%this.keyList,1,getFieldCount(%this.keyList));
			else if(%i $= getFieldCount(%this.keyList)-1)
				%this.keyList = getFields(%this.keyList,0,%i-1);
			else
				%this.keyList = getFields(%this.keyList,0,%i-1) TAB getFields(%this.keyList,%i+1,getFieldCount(%this.keyList));
			return 1;
		}
	}
	return 0;
}
//blockByte::resetKey (METHOD)
//"Resets the key's (row) values to what they are when the key is originally added"
//%this (blockByte): The database that the key is being reset in
//%key (any): The key to be reset
//Zack0Wack0 11:33 PM 11/01/2011
function blockByte::resetKey(%this,%key)
{
	if(!%this.keyExists(%key))
	{
		%this.throwException(KeyNonExistant,"An attempt to reset key '" @ %key @ "' failed. That key does not exist in this blockByte database.");
		return 0;
	}
	for(%i=0;%i<getFieldCount(%this.valueList);%i++)
		%this.value[%key,getField(%this.valueList,%i)] = %this.valueDefault[getField(%this.valueList,%i)];
	return 1;
}
//---DATA---
//blockByte::resetDatabase (METHOD)
//"Resets the entire database (removes all values AND keys)"
//%this (blockByte): The database that is to be reset
//Zack0Wack0 11:34 PM 11/01/2011
function blockByte::resetDatabase(%this)
{
	for(%i=0;%i<getFieldCount(%this.keyList);%i++)
	{
		%key = getField(%this.keyList,%i);
		for(%i=0;%i<getFieldCount(%this.valueList);%i++)
		{
			%value = getField(%this.valueList,%i);
			%this.value[%key,%value] = "";
			%this.valueDefault[%value] = "";
		}
	}
	%this.keyList = "";
	%this.valueList = "";
	return 1;
}
//blockByte::importDatabase (METHOD)
//"Import keys (row) and values (column) from a supported database file"
//%this (blockByte): The database that is importing a file
//%path (file): The path to the supported database file that is being imported into this database
//%type (string): The type of supported database format
//Zack0Wack0 11:36 PM 11/01/2011
function blockByte::importDatabase(%this,%path,%type)
{
	%file = new FileObject();
	if(!isFile(%path))
	{
		%file.delete();
		%this.throwException(FileNonExistant,"An attempt to import a database file failed. blockByte was unable to locate the file.");
		return 0;
	}
	%this.resetDatabase();
	%file.openForRead(%path);
	switch$(%type)
	{
		case "CSV":
			%line = strReplace(strReplace(strReplace(%file.readLine(),"'",""),"\"",""),"\,","\t");
			for(%i=1;%i<getFieldCount(%line);%i++)
			{
				%args = strReplace(getField(%line,%i),"=","\t");
				%this.addValue(collapseEscape(getField(%args,0)),collapseEscape(getField(%args,1)));
			}
			while(!%file.isEOF())
			{
				%line = strReplace(strReplace(strReplace(%file.readLine(),"'",""),"\"",""),"\,","\t");
				%key = getField(%line,0);
				for(%i=1;%i<getFieldCount(%line);%i++)
				{
					%value = getField(%this.valueList,%i-1);
					%this.value[%key,%value] = collapseEscape(getField(%line,%i));
				}
			}
		default:
			while(!%file.isEOF())
			{
				%line = %file.readLine();
				%function = getSubStr(%line,0,1);
				%args = strReplace(getSubStr(%line,2,strLen(%line)-3),"\,","\t");
				switch$(%function)
				{
					case "V":
						for(%i=0;%i<getFieldCount(%args);%i++)
						{
							%arg = strReplace(getField(%args,%i),"=","\t");
							%value[%i+1] = getField(%arg,0);
							%this.addValue(getField(%arg,0),getField(%arg,1));
						}
					case "K":
						%key = getField(%args,0);
						%this.addKey(%key);
						for(%i=1;%i<getFieldCount(%args);%i++)
							%this.value[%key,%value[%i]] = getField(%args,%i);
				}
			}
	}
	%file.close();
	%file.delete();
	return 1;
}
//blockByte::exportDatabase (METHOD)
//"Export keys (row) and values (column) to a supported database file"
//%this (blockByte): The database that is exporting a file
//%path (file): The path to the file that the database is attempting to export
//%type (string): The type of supported database format
//Zack0Wack0 11:37 PM 11/01/2011
function blockByte::exportDatabase(%this,%path,%type)
{
	%file = new FileObject();
	if(!isWriteableFileName(%path))
	{
		%this.throwException(FileUnwritable,"An attempt to export a database file failed. blockByte does not have permission to write to the specified export path.");
		%file.delete();
		return 0;
	}
	if(%this.valueList $= "" || %this.keyList $= "")
	{
		%this.throwException(DatabaseIsBlank,"An attempt to export a database file failed. This blockByte database is blank.");
		%file.delete();
		return 0;
	}
	%file.openForWrite(%path);
	switch$(%type)
	{
		case "CSV":
			%line = "key";
			for(%i=0;%i<getFieldCount(%this.valueList);%i++)
			{
				%value = getField(%this.valueList,%i);
				%line = %line @ "," @ %value @ "=" @ %this.valueDefault[%i];
			}
			%file.writeLine(expandEscape(%line));
			for(%i=0;%i<getFieldCount(%this.keyList);%i++)
			{
				%key = getField(%this.keyList,%i);
				%line = %key;
				for(%a=0;%a<getFieldCount(%this.valueList);%a++)
				{
					%value = %this.value[%key,getField(%this.valueList,%a)];
					%line = %line @ "," @ %value;
				}
				%file.writeLine(expandEscape(%line));
			}
		case "XML":
			%file.writeLine("<?xml version=\"1.0\">\n<values>");
			for(%i=0;%i<getFieldCount(%this.valueList);%i++)
			{
				%value = getField(%this.valueList,%i);
				%file.writeLine(expandEscape("<value>\n<name>" @ %value @ "</name>\n<default>" @ %this.valueDefault[%value] @ "</default>\n</value>"));
			}
			%file.writeLine("</values>\n<keys>");
			for(%i=0;%i<getFieldCount(%this.keyList);%i++)
			{
				%key = getField(%this.keyList,%i);
				%file.writeLine(expandEscape("<key>\n<name>" @ %key @ "</name>"));
				for(%i=0;%i<getFieldCount(%this.valueList);%i++)
				{
					%value = getField(%this.valueList,%i);
					%file.writeLine(expandEscape("<field>" @ %this.value[%key,%value] @ "</field>"));
				}
				%file.writeLine("</key>");
			}
			%file.writeLine("</keys>");
		case "SQL":
			%file.writeLine("CREATE TABLE IF NOT EXISTS `blockByte` (");
			%file.writeLine("  `key` varchar(1024) NOT NULL default '',");
			for(%i=0;%i<getFieldCount(%this.valueList);%i++)
			{
				%value = getField(%this.valueList,%i);
				if(%i == getFieldCount(%this.valueList) - 1)
					%file.writeLine(expandEscape("  `" @ %value @ "` varchar(1024) NOT NULL default '" @ %this.valueDefault[%value] @ "'"));
				else
					%file.writeLine(expandEscape("  `" @ %value @ "` varchar(1024) NOT NULL default '" @ %this.valueDefault[%value] @ "'\,"));
			}
			%file.writeLine(");");
			for(%i=0;%i<getFieldCount(%this.keyList);%i++)
			{
				%key = getField(%this.keyList,%i);
				%args = "(" @ %key;
				for(%a=0;%a<getFieldCount(%this.valueList);%a++)
				{
					%value = getField(%this.valueList,%a);
					if(%value !$= "")
					{
						if(%a == getFieldCount(%this.valueList) - 1)
							%args = %args @ ", '" @ %value @ "')";
						else
							%args = %args @ ", '" @ %value @ "'";
					}
				}
				%file.writeLine(expandEscape("INSERT INTO `blockByte` VALUES " @ %args @ ";"));
			}
		default:
			%line = "V(";
			for(%i=0;%i<getFieldCount(%this.valueList);%i++)
			{
				%value = getField(%this.valueList,%i);
				if(%i == getFieldCount(%this.valueList) - 1)
					%line = %line @ %value @ "=" @ %this.valueDefault[%value] @ ")";
				else
					%line = %line @ %value @ "=" @ %this.valueDefault[%value] @ "\,";
			}
			%file.writeLine(expandEscape(%line));
			for(%i=0;%i<getFieldCount(%this.keyList);%i++)
			{
				%key = getField(%this.keyList,%i);
				%line = "K(" @ %key @ "\,";
				for(%a=0;%a<getFieldCount(%this.valueList);%a++)
				{
					%value = getField(%this.valueList,%a);
					if(%value !$= "")
					{
						if(%a == getFieldCount(%this.valueList) - 1)
							%line = %line @ %this.value[%key,%value] @ ")";
						else
							%line = %line @ %this.value[%key,%value] @ "\,";
					}
				}
				%file.writeLine(expandEscape(%line));
			}
	}
	%file.close();
	%file.delete();
	return 1;
}