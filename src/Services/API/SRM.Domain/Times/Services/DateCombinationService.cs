using Fix.Data;
using SRM.Data.Models.Times;
using System.Linq;

namespace SRM.Domain.Times.Services
{
    public class DateCombinationService : IDateCombinationService
    {
        private readonly IRepository<DateCombination> dateCombinationRepository;

        public DateCombinationService(IRepository<DateCombination> dateCombinationRepository)
        {
            this.dateCombinationRepository = dateCombinationRepository;
        }
        public virtual IQueryable<DateCombination> Get()
        {
            return dateCombinationRepository.GetAllWithoutRestriction();
        }
        private int calculateCombinationCount(int combinationValue, int combinationNumber)
        {
            int combinationCount = 1;
            for (int i = 0; i < combinationNumber; i++)
            {
                combinationCount *= (combinationValue - i);
            }

            for (int i = 0; i < combinationNumber; i++)
            {
                combinationCount /= i + 1;
            }
            return combinationCount;
        }
        public bool CreateDateCombination()
        {
            //        List<string> permutations = new List<string>();
            //        IEnumerable<List<Pair>> query =
            //from permutation in permutations
            //select colors.Zip(permutation, (color, food) => new Pair(color, food)).ToList();
            //        int value = 7;
            //        var matris = new int[127, value];
            //        int index = 0;
            //        for (int i = 1; i <= value; i++)
            //        {

            //            int combinationCount = calculateCombinationCount(value, i);
            //            int firstValue = 0;
            //            int indexMod = 0;
            //            var array = new int[index,i,value];
            //            for (int j = 0; j < combinationCount; j++)
            //            {

            //                DateCombination dateCombination = new DateCombination();
            //                var selectedDate = i;

            //                int current = 0;
            //                for (int h = firstValue; h < firstValue+selectedDate; h++)
            //                {
            //                    if (h == firstValue)
            //                    {
            //                        current = firstValue;
            //                    }
            //                    else
            //                    {
            //                        current = h + (indexMod % (value - (firstValue)));

            //                    }
            //                    matris[index, current] = 1;

            //                }
            //                index++;
            //                indexMod++;
            //                if (current == 6)
            //                {
            //                    firstValue++;
            //                    indexMod = 0;
            //                }
            //                //dateCombinationRepository.Add(dateCombination);

            //            }
            //            for (int k = 0; k < 127; k++)
            //            {
            //                string text ="";
            //                for (int l = 0; l < 7; l++)
            //                {
            //                    text += matris[k, l] + " ";
            //                }
            //                    StaticLogger.WriteToFile(text);
            //            }

            //        }
            //throw new NotImplementedException();
            return true;
        }

        public bool HasDateCombination()
        {
            return Get().Count() > 0;
        }

        public DateCombination GetDateCombination(DateCombination dateCombination)
        {
            var entity = Get().FirstOrDefault(en =>
            en.Monday == dateCombination.Monday &&
            en.Tuesday == dateCombination.Tuesday &&
            en.Wednesday == dateCombination.Wednesday &&
            en.Thursday == dateCombination.Thursday &&
            en.Friday == dateCombination.Friday &&
            en.Saturday == dateCombination.Saturday &&
            en.Sunday == dateCombination.Sunday
            );
            return entity;
        }
    }
}
