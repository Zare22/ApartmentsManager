using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DAL
{
    public static class RepoFactory
    {
        public static IRepository GetRepository() => new DatabaseRepository();
    }
}
