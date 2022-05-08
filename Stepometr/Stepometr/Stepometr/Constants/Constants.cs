namespace Stepometer.Constants
{
    public static partial class Constants
    {
        public static string BaseUrl = "https://10.0.2.2:44301/api";

        public static string DataStepsControllerName = "/DataSteps";

        public static string GetDataSteps = DataStepsControllerName + "/GetDataSteps";
        public static string GetDataStepsById = DataStepsControllerName + "/GetDataStepsById";
        public static string AddDataSteps = DataStepsControllerName + "/AddDataSteps";
        public static string UpdateDataStepsById = DataStepsControllerName + "/UpdateDataStepsById";
        public static string DeleteDataSteps = DataStepsControllerName + "/DeleteDataSteps";

        public static string AuthorizationControllerName = "/Account";

        public static string RegisterControllerName = AuthorizationControllerName + "/Register";
    }
}