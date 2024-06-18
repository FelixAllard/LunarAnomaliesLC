using Unity.Netcode;

namespace LunarAnomalies.External
{
    public class ExtendDeadlineScript : BaseCommand
    {
        internal const string NAME = "Extend Deadline";
        internal const string ENABLED_SECTION = $"Enable {NAME}";
        internal static ExtendDeadlineScript instance;
        void Start()
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        

        internal static int GetTotalCost()
        {
            return UpgradeBus.Instance.PluginConfiguration.EXTEND_DEADLINE_PRICE + UpgradeBus.Instance.PluginConfiguration.EXTEND_DEADLINE_ADDITIONAL_PRICE_PER_QUOTA * TimeOfDay.Instance.timesFulfilledQuota;
        }

        internal int GetTotalCostPerDay(int days)
        {
            int daysExtended = GetDaysExtended();
            int totalCost = 0;
            for(int i = 0; i < days; i++)
            {
                totalCost += GetTotalCost() + daysExtended * UpgradeBus.Instance.PluginConfiguration.EXTEND_DEADLINE_ADDITIONAL_PRICE_PER_DAY;
                daysExtended++;
            }
            return totalCost;
        }

        public static int GetDaysExtended()
        {
            return UpgradeBus.Instance.daysExtended;
        }

        public static void SetDaysExtended(int daysExtended)
        {
            UpgradeBus.Instance.daysExtended = daysExtended;
        }

        public static new void RegisterCommand()
        {
            SetupGenericCommand<ExtendDeadlineScript>(NAME);
        }
    }
}
