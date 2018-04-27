# Asset Resolution System (ARS)

This a few extension methods to reduce some using statements and a few string manipulations needed to interface with a Linux system.

* **AppSettings** - Alters App.config file just using a string.
    * **SaveAppSetting** - Allows to save a config setting. EX "*AppSettingName*".SaveAppSetting("myNewValue");
    * **GetAppSetting** - Get a config setting. EX MyVar = "*AppSettingName*".GetAppSetting();
    * **GetConnectionString** - Gets a saved connection string. EX MyConString = "*DbCon*".GetConnectionString();
    
* **String Extensions**
    * **TruncateAtWord** - returns a string truncated at the nearest whole word that does not exceed the specified length of the whole string to be returned.
    * **TruncateAtWordToArray** - Similar to above, except that a string array of every word that does not exceed the of the whole string to be returned.
    * **PrependStringToSize** - will add a specified character to the beginning of a string until the whole string is a specified sized.
    * **DateTimeToUnixTime** - converts a DateTime to a long representing the equivalent Unix time.
    * **UnixTimeToDateTime** - converts a long representing Unix time to an equivalent DateTime.
    
* **Misc**
    * **IsLinux** - returns a bool indicating that the current operating system is Linux.