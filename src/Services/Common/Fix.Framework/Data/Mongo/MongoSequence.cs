using System;

namespace Fix.Data
{
    public class MongoSequence : IEntity
    {
        public string Id { get; set; }

        public object Sequence { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }


        public long CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public long? ModifiedBy { get; set; }
    }
}
