using System.Collections.Generic;
using System.Linq;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommandRepository : ICommandRepository
    {
        List<Command> commands =  new List<Command>();
        public MockCommandRepository()
        {
            commands = new List<Command>
            {
                new Command {Id = 1, HowTo="Create dotnet core web api app",Line="dotnet new webapi -n [project name]",Platform="DotNet Core CLI" },
                new Command {Id= 2 , HowTo="Open VS code in current folder", Line="code .",Platform = "CLI"},
                new Command{Id = 3 , HowTo = "run dotnet core app on Kestrel",Line="dotnet run",Platform="CLI"}
            };
        }

        public void CreateCommand(Command command)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command command)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return commands;
        }

        public Command GetCommandById(int Id)
        {
            return commands.FirstOrDefault(c => c.Id == Id);
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

       
    }
}