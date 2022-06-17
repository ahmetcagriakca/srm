using System;

namespace Fix.Searching
{
    public interface IDocType
    {
        Guid Id { get; set; }
        string Name { get; }
    }
}
