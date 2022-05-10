namespace Stepometer.Constants
{
    public static partial class Constants
    {
        public static string BaseUrl = "https://10.0.2.2:44301";

        public static string DataStepsControllerName = "/api/DataSteps";

        public static string GetDataSteps = DataStepsControllerName + "/GetDataSteps";
        public static string GetDataStepsById = DataStepsControllerName + "/GetDataStepsById";
        public static string AddDataSteps = DataStepsControllerName + "/AddDataSteps";
        public static string UpdateDataStepsById = DataStepsControllerName + "/UpdateDataStepsById";
        public static string DeleteDataSteps = DataStepsControllerName + "/DeleteDataSteps";

        public static string AuthorizationControllerName = "/api/Account";

        public static string Register = AuthorizationControllerName + "/Register";
        public static string Login = "/Token";
        public static string GetToken = "/Token";
    }
}