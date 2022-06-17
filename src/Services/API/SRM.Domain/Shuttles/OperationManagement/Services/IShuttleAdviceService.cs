using Fix;
using System.Collections.Generic;

namespace SRM.Domain.Shuttles.OperationManagement.Services
{
    public interface IShuttleAdviceService : IDependency
    {
        /// <summary>
        /// Öğrenci operasyon önerileri
        /// </summary>    
        GetAdviceResult GetAdvices(GetAdviceRequest adviceRequest);
        /// <summary>
        /// Öğrenci önerisini operasyona çevirme
        /// </summary>
        /// <param name="studentId">Öğrenci Id</param>
        /// <param name="shuttleOperationId">Operasyon Id</param>
        void SetAdviceToOperation(long studentId, long shuttleOperationId);
        object GetAdvices2();
    }

    public class GetAdviceRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public long ShuttleOperationId { get; set; }
        public List<MapsCorner> MapsCorners { get; set; }

    }

    public class GetAdviceResult
    {
        public object Advices { get; set; }
        public int TotalCount { get; set; }
    }

    public class MapsCorner
    {
        public double LocationX { get; set; }
        public double LocationY { get; set; }


    }

}