using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public interface ICommandRepository
    {
        //CRUD ops contract

        IEnumerable<Command> GetAllCommands();
        Command GetCommandById(int Id);
        void CreateCommand(Command command);
        void DeleteCommand(Command command);
        bool SaveChanges();

    }
}