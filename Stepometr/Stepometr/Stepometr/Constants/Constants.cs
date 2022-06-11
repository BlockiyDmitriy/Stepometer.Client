namespace Stepometer.Constants
{
    public static partial class Constants
    {
        public static string BaseUrl = "https://10.0.2.2:44301";

        #region Data steps
        
        public static string DataStepsControllerName = "/api/DataSteps";

        public static string GetDataSteps = DataStepsControllerName + "/GetDataSteps";
        public static string GetDataStepsById = DataStepsControllerName + "/GetDataStepsById";
        public static string AddDataSteps = DataStepsControllerName + "/AddDataSteps";
        public static string UpdateDataStepsById = DataStepsControllerName + "/UpdateDataStepsById";
        public static string DeleteDataSteps = DataStepsControllerName + "/DeleteDataSteps";
        
        #endregion

        #region History user param
        
        public static string HistoryUserControllerName = "/api/HistoryUserParam";

        public static string GetHistoryUserParam = HistoryUserControllerName + "/GetHistoryUserParam";
        public static string GetHistoryUserParamById = HistoryUserControllerName + "/GetHistoryUserParamById";
        public static string AddHistoryUserParam = HistoryUserControllerName + "/AddHistoryUserParam";
        public static string UpdateHistoryUserParamById = HistoryUserControllerName + "/UpdateHistoryUserParamById";
        public static string DeleteHistoryUserParam = HistoryUserControllerName + "/DeleteHistoryUserParam";

        #endregion

        #region History stepometer
        
        public static string HistoryControllerName = "/api/History";

        public static string GetHistory = HistoryControllerName + "/GetHistory";
        public static string GetHistoryById = HistoryControllerName + "/GetHistoryById";
        public static string AddHistory = HistoryControllerName + "/AddHistory";
        public static string UpdateHistoryById = HistoryControllerName + "/UpdateHistoryById";
        public static string DeleteHistoryById = HistoryControllerName + "/DeleteHistoryById";

        #endregion

        #region Auth

        public static string AuthorizationControllerName = "/api/Account";

        public static string Register = AuthorizationControllerName + "/Register";
        public static string Login = "/Token";
        public static string GetToken = "/Token";

        #endregion
    }
}