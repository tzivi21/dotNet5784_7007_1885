
namespace Dal;


static internal class Config
{
    static readonly string s_data_config_xml = "data-config";
    internal static int NextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependencyId"); }
    internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }
    internal static DateTime startProjectDate { get => XMLTools.GetDates(s_data_config_xml, "StartProjectDate"); }
    internal static DateTime endProjectDate { get => XMLTools.GetDates(s_data_config_xml, "EndProjectDate"); }


}