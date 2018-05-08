namespace SolarEdgeDataFetcher
{
    /// <summary>
    /// Defines the data update mode for the DataFetcher
    /// </summary>
    public enum DataUpdateModeEnum
    {
        /// <summary>
        /// Update existing objects will update the properties of the existing data objects. If the data object do net yet exist new data object will be created.
        /// </summary>
        UpdateExistingObjects,

        /// <summary>
        /// New data objects will be created on every data update.
        /// </summary>
        CreateNewObjects
    }
}