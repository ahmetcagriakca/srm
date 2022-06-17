namespace Fix.Searching
{
    //public class ElasticService<TIx> : IElasticService<TIx> where TIx : BaseIndex
    //{
    //    public ElasticService(IIndexInitializer locator)
    //    {
    //        IndexDB = locator.Init<TIx>();
    //    }

    //    public TIx IndexDB { get; set; }

    //    public IEnumerable<T> All<T>() where T : class, IDocType
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool Delete<T>(Guid id) where T : class, IDocType
    //    {
    //        var response = IndexDB.Client.Delete<T>(id, d => d
    //          .Index(IndexDB.Name)
    //          .Type<T>());

    //        return response.IsValid;
    //    }

    //    public T Get<T>(Guid id) where T : class, IDocType
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IIndexResponse Insert<T>(T type) where T : class, IDocType
    //    {
    //        return IndexDB.Client.Index(type, i => i
    //               .Index(IndexDB.Name)
    //               .Type(type.Name)
    //               .Id(type.Id)
    //               .Refresh(Elasticsearch.Net.Refresh.False));
    //    }

    //    public IEnumerable<T> Search<T>(SearchModel model) where T : class, IDocType
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Update<T>(T type) where T : class, IDocType
    //    {
    //        var response = IndexDB.Client.Index(type, i => i
    //               .Index(IndexDB.Name)
    //               .Type(type.Name)
    //               .Id(type.Id)
    //               .Refresh(Elasticsearch.Net.Refresh.False));

    //    }

    //    public async Task<IIndexResponse> InsertAsync<T>(T type) where T : class, IDocType
    //    {
    //        return await IndexDB.Client.IndexAsync(type, i => i
    //                 .Index(IndexDB.Name)
    //                 .Type(type.Name)
    //                 .Id(type.Id)
    //                 .Refresh(Elasticsearch.Net.Refresh.False));

    //    }

    //}
}
