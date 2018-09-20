# SolarEdge.net

c# code to handle data from a SolarEdge inverters and or meters. 

This is probably not best possible or most elegant solution, but it is working for now. You are very welcome to fork the repository and to make changes to the project.

The following projects exist in the solution:

SolarEdgeData
=============

Contains classes to hold the parsed SolarEdge data, type converters to display the values and a attribute classes which which explains the source of the data.

There are 2 classes for SolarEdge data:

* __SolarEdgeFullData__ has properties for all data points that can be received from the inverter and from the meter.
* __SolarEdgeBaseData__ has only a small subset of properties containing only the most important data.

The data classes are fully decorated with _Category_, _DisplayName_, _Description_ and _TypeConverter_ attributes for easy use in controls (e.g. propertygrid control) or other frameworks supporting them. In addition the data classes are also decorated with the _DataContract_ and _DataMember_ attributes so they can be used in WCF (windows communication foundation). 


SolarEdgeDataFetcher
====================
This project contains the DataFetcher class which encapsulates all reading functions to get the SolarEdge data from the inverter and/or the meter. When started the data fetcher will poll for new data at user definable intervals and map the data to SolarEdgeData objects as found in the SolarEdgeData project.

Since the SolarEdge inverters only accept a single ModBus.TCP connection at a time, this class is implemented as a singleton class to prevent accidental instanciation of several instances. Access the static DataFetcher.Instance property to get the singleton instance of the class.

The DataFetcher class has several config properties which should be set before calling Start().

Config properties
-----------------

* __RefreshIntervalMs__ - Gets or sets the refresh interval in ms. If the time required for reading and updating the data plus the MinIntervalBetweenUpdatesMs is larger than the RefreshIntervalMs, the RefreshIntervalMs will be overidden ny MinIntervalBetweenUpdatesMs plus the time required for the updates.
* __MinIntervalBetweenUpdatesMs__ - Gets or sets the minimum interval between updates in ms.
* __IPAdress__ - IP address of the SolarEdge inverter.
* __ModBusPort__ -  Gets or sets the modbus TCP port (typically 502).
* __ConnectionTimeoutMs__ -  Gets or sets the connection timeout in ms for the ModBus (default 2000ms).
* __ReadInverterData__ - Gets or sets a value indicating whether inverter data should be read (default True).
* __ReadMeterData__ - Gets or sets a value indicating whether inverter data should be read (default True).
* __DataUpdateMode__ - Gets or sets the data update mode. UpdateExistingObjects will update the properties of the existing data objects. If the data object do net yet exist new data object will be created. CreateNewObjects will create a new object on every data update.
* __DataInvalidAfterMs__ - The number of ms after which the DataIsValid property will be set to false if the data has not been updated. Values <=0 are disabling data invalidation.

Data Properties
---------------

* __SolarEdgeDataIsValid__ whether the SolarEdgeData properties contain valid data.
* __ConnectionEstablished__ indicates wether the connection to the modbus is established or not.
* __ConnectionLastEstablished__ contains the timestamp of the last time the connection was established. 
* __LastDataUpdate__ contains the timestamp of the last data update.
* __SolarEdgeFullData__ contains the SolarEdgeFullData object as defined in the SolarEdgeData project.
* __SolarEdgeBaseData__ contains the SolarEdgeBaseData object as defined in the SolarEdgeData project.
* __Started__ indicates if the data fetcher has been started.
* __ConnectionEstablishedCountLastHour__ indictates the number of time the modbus connection has been (re)established during the last hour. 

Events
------

* __SolarEdgeDataUpdated__ is fired after the solar edge data has been updated successfully.
* __SolarEdgeDataUpdateFailed__ is fired if a data update fails (e.g. due to lost connection, incorect respons from inverter, faulty data).
* __SolarEdgeDataIsValidChanged__ is fired when the value of the _SolarEdgeDataIsValid_ property has changed.

Methods
-------

* __Start()__ starts the data fetcher which will poll for new data at userdefineable intervals.
* __Stop()__ will stop the data fetcher.

SolarEdgeService
================

The SolarEdgeService a windows service which can also be started as a normal exe which uses the data fetcher to receive the latest data from the SolarEdge inverter and makes that data available through a WCF (windows communication foundation) service.

The behaviour, bindings and other settings of the WCF service can be adjusted in the app.config file.

This WCF service provides methods to get the latestes data and it also has methods to register for automatic updates when data updates are available. It is recommended to use the automatic update subscription functionality to ensure that the latest data is always available.

Depending on your windows version you might need to use the following statement to allow for proper function of the service (make sure you use the correct DOMAIN/user and to adjust the url if you change it in the app.config):

`netsh http add urlacl url=http://+:8735/SolarEdgeWCFService user=DOMAIN\user`


Misc
====

Own applications
----------------
If you want to use SolarEdge data resp. code from this project in your own application? There are 2 possible ways:

* Reference _SolarEdgeDataFetcher_ and use the _DataFetcher_ class from your own code. This is only recommended if you are sure that you wont need SolarEdge data in any other program (SolarEdge inverters only allow a single modmus.TCP connection).
* Install the _SolarEdgeService_ and  get the data through the provided WCF service functions.

Logging
-------

_Log4Net_ has been used to provide some logging of the activities in the projects. To activate the logging put a file named _Log4NetConfig.xml_ containing the config for log4net into the exe directory.