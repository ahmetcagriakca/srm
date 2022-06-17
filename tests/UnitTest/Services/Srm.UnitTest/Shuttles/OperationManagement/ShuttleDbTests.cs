using Fix.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Srm.UnitTest.Facades;
using SRM.Data.Models.Shuttles;
using SRM.Domain.Shuttles.OperationManagement.Services;
using SRM.Services.Api.BaseModel;
using SRM.Services.Api.Shuttles.OperationManagement.Controllers;
using System.Collections;
using System.Linq;
using Xunit;

namespace Srm.UnitTest.Shuttles.OperationManagement
{
    public class ShuttleDbTests : TestBase
    {
        /// <summary>
        /// Database connecting to remote database
        /// </summary>
        public ShuttleDbTests() : base(false)
        {

        }


        [Theory]
        [InlineData(10852, 11)]
        [InlineData(11145, 9)]
        public void Shuttle_Operation_Students_Test(long shuttleOperationId, int expected)
        {
            var shuttleStudentOperationRepository = ContainerManager.Resolve<IRepository<ShuttleStudentOperation>>();

            var shuttleStudents = shuttleStudentOperationRepository.Table
                .Include(en => en.ShuttleOperation)
                .ThenInclude(en => en.LocationRegion)
                .Include(en => en.StudentOperationLocations)
                .Where(en => en.ShuttleOperation.Id == shuttleOperationId);

            var result = shuttleStudents.Count();
            Assert.True(expected == result);

            var studentLocationList = shuttleStudents.Select(en => new
            {
                en.Student,
                en.StudentOperationLocations.First().LocationX,
                en.StudentOperationLocations.First().LocationY

            });
        }

        [Theory]
        [InlineData(10852, 11)]
        [InlineData(11145, 9)]
        public void Shuttle_Operation_Students_Controller_Test(long shuttleOperationId, int expected)
        {
            var shuttleService = ContainerManager.Resolve<IShuttleService>();
            ShuttleController shuttleController = new ShuttleController(shuttleService);
            var shuttleStudents = shuttleController.GetShuttleOperationStudentLocations(shuttleOperationId) as OkObjectResult;
            var result = shuttleStudents.Value as BaseServiceResponse;
            Assert.True(result.ResultValue is IEnumerable);

            if (result.ResultValue is IEnumerable)
            {
                var list = Enumerable.ToList<dynamic>(result.ResultValue);
                Assert.True(expected == list.Count);
            }
        }
    }

}
