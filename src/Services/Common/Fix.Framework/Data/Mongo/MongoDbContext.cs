using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace Fix.Data
{
    public class MongoDbContext : IDisposable
    {
        public IMongoClient Client;
        public IMongoDatabase Database;
        private readonly MongoDbSettings _settings;


        public MongoDbContext(MongoDbSettings settings)
        {
            _settings = settings;
            Client = new MongoClient(_settings.ConnectionString);
            Database = Client.GetDatabase(_settings.DatabaseName);
            CreateMongoRepositories(Client, Database);
        }

        public bool CheckConnection()
        {
            bool isMongoLive = Database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);
            return isMongoLive;
        }

        private void CreateMongoRepositories(IMongoClient client, IMongoDatabase database)
        {
            //Contacts = new MongoRepository<Contact, int>(client, database, httpContext);
            //Users = new MongoRepository<User, int>(client, database, httpContext);
            //Roles = new MongoRepository<Role, int>(client, database, httpContext);
            //Logs = new MongoRepository<Log, int>(client, database, httpContext);
            //Pages = new MongoRepository<Page, int>(client, database, httpContext);
            //Watchers = new MongoRepository<Watcher, int>(client, database, httpContext);
            //Projects = new MongoRepository<Project, int>(client, database, httpContext);
            //Deploys = new MongoRepository<Deploy, int>(client, database, httpContext);
            //Processes = new MongoRepository<Process, int>(client, database, httpContext);
            //ApplicationParameters = new MongoRepository<ApplicationParameter, int>(client, database, httpContext);
            //BugItems = new MongoRepository<BugItem, int>(client, database, httpContext);
            //ToDos = new MongoRepository<ToDo, int>(client, database, httpContext);
            //ToDoTypes = new MongoRepository<ToDoType, int>(client, database, httpContext);
            //ToDoActions = new MongoRepository<ToDoAction, int>(client, database, httpContext);
            //ToDoComments = new MongoRepository<ToDoComment, int>(client, database, httpContext);
        }
        public void SaveChanges()
        {

        }

        //public virtual MongoRepository<Contact, int> Contacts { get; set; }
        //public virtual MongoRepository<User, int> Users { get; set; }
        //public virtual MongoRepository<Role, int> Roles { get; set; }
        //public virtual MongoRepository<Log, int> Logs { get; set; }
        //public virtual MongoRepository<Page, int> Pages { get; set; }
        //public virtual MongoRepository<Watcher, int> Watchers { get; set; }
        //public virtual MongoRepository<Project, int> Projects { get; set; }
        //public virtual MongoRepository<Deploy, int> Deploys { get; set; }
        //public virtual MongoRepository<Process, int> Processes { get; set; }
        //public virtual MongoRepository<ApplicationParameter, int> ApplicationParameters { get; set; }
        //public virtual MongoRepository<BugItem, int> BugItems { get; set; }
        //public virtual MongoRepository<ToDo, int> ToDos { get; set; }
        //public virtual MongoRepository<ToDoType, int> ToDoTypes { get; set; }
        //public virtual MongoRepository<ToDoAction, int> ToDoActions { get; set; }
        //public virtual MongoRepository<ToDoComment, int> ToDoComments { get; set; }

        public void Dispose()
        {
        }
    }
}
