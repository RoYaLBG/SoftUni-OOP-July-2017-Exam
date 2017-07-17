namespace Minedraft.Common
{
    public static class Messages
    {
        public const string PropertyViolation = "{0} is not registered, because of it's {1}";

        public const string RegisterSuccess = "Successfully registered {0} {1} - {2}";

        public const string ModeSuccess = "Successfully changed working mode to {0} Mode";

        public const string DaySuccess = 
            "A day has passed.\r\nEnergy Provided: {0}\r\nPlumbus Ore Mined: {1}";

        public const string CheckFail = "No element found with id - {0}";

        public const string ShutdownSuccess =
            "System Shutdown\r\nTotal Energy Stored: {0}\r\nTotal Mined Plumbus Ore: {1}";
    }
}

