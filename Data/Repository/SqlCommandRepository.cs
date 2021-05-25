using Commander.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Data.Repository
{
    public class SqlCommandRepository : ICommandRepository
    {
        private readonly CommanderDbContext _dbcontext;

        public SqlCommandRepository(CommanderDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public void CreateCommand(Command command)
        {
            _dbcontext.Add(command);
        }

        public void DeleteCommand(Command command)
        {
            _dbcontext.Remove(command);
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands =  _dbcontext.Commands.ToList();
            return commands;
        }

        public Command GetCommandById(int Id)
        {
            var command =  _dbcontext.Commands.Find(Id);
            return command;
        }

        public bool SaveChanges()
        {
            return _dbcontext.SaveChanges() >= 0;
        }

    }
}
