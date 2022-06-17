using Fix.Data;

namespace SRM.Data.Models.Planing
{
    public class PlanBase<T> : FixEntity<T>
    {
        public PlanStatus Status { get; set; }


    }

    /// <summary>
    /// Plan boşta , onaylandı ya da iptal edildi.
    /// </summary>
    public enum PlanStatus
    {
        Idled = 0,
        Planed = 1,
        Approved = 2,
        Denied = 3
    }
}
