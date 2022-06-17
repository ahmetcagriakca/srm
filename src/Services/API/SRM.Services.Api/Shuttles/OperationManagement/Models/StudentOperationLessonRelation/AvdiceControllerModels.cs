namespace SRM.Services.Api.Shuttles.OperationManagement.Models.StudentOperationLessonRelation
{

    public class AvdiceControllerModels
    {

    }

    public class SetAdviceToOperationRequest
    {
        /// <summary>
        /// ÖğrenciId
        /// </summary>
        /// <returns></returns>
        public long StudentId { get; set; }

        /// <summary>
        /// Servis operasyon Id
        /// </summary>
        /// <returns></returns>
        public long ShuttleOperationId { get; set; }
    }
}