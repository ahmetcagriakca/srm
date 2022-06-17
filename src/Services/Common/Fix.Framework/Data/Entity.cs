using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fix.Data
{
    public class ParameterEntity<T> : FixEntity<T>, IActivable
    {
        public bool IsActive { get; set; }
    }
    public partial class FixEntity<T> : BaseEntity<T>, ICorporable, IDeletable
    {
        [Column(Order = 2)]
        public long CompanyId { get; set; }

        public bool IsDelete { get; set; }
    }

    public class ApplicationParameterEntity<T> : AuthEntity<T>
    {
    }

    public partial class AuthEntity<T> : BaseEntity<T>, IActivable, IDeletable
    {
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }

    public class BaseEntity<T> : ITraceable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public T Id { get; set; }

        [Column(Order = int.MaxValue)]
        public DateTime CreatedOn { get; set; }

        [Column(Order = int.MaxValue - 1)]
        public long CreatedBy { get; set; }

        [Column(Order = int.MaxValue - 2)]
        public DateTime? ModifiedOn { get; set; }

        [Column(Order = int.MaxValue - 3)]
        public long? ModifiedBy { get; set; }

        [Column(Order = int.MaxValue)]
        public byte[] RowVersion { get; set; }

    }

    public interface IActivable : IEntity
    {
        bool IsActive { get; set; }
    }

    public interface IDeletable : IEntity
    {
        bool IsDelete { get; set; }
    }

    public interface ICorporable : IEntity
    {
        long CompanyId { get; set; }
    }

    public interface ITraceable : IEntity
    {
        byte[] RowVersion { get; set; }
        DateTime CreatedOn { get; set; }
        long CreatedBy { get; set; }
        DateTime? ModifiedOn { get; set; }
        long? ModifiedBy { get; set; }
    }
}
