using System.ComponentModel;

namespace AppProj.Domain.Enums
{
    public enum ReportTypesEnum
    {
        [Description("HR Circulars")]
        HRCirculars = 1,
        [Description("HNPP Reports")]
        HNPPReports = 2,
        [Description("Internal SitReps")]
        InternalSIT = 3,
        [Description("External SitReps")]
        ExternalSIT = 4
    }
}