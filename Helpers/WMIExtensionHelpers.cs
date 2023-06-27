using System.Management;

internal static class WMIExtensionHelpers
{
    public static ManagementObject First(this ManagementObjectSearcher searcher)
    {
        ManagementObject result = null;
        foreach (ManagementObject item in searcher.Get())
        {
            result = item;
            break;
        }
        return result;
    }
}