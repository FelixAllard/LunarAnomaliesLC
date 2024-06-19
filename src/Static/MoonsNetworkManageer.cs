using Unity.Netcode;

namespace LunarAnomalies;

public static class MoonsNetworkManageer
{
    [ClientRpc]
    public static void ExtendDeadlineClientRpc(int days)
    {
        float before = TimeOfDay.Instance.timeUntilDeadline;
        TimeOfDay.Instance.timeUntilDeadline += TimeOfDay.Instance.totalTime * days;
        TimeOfDay.Instance.UpdateProfitQuotaCurrentTime();
        TimeOfDay.Instance.SyncTimeClientRpc(TimeOfDay.Instance.globalTime, (int)TimeOfDay.Instance.timeUntilDeadline);
    }
}