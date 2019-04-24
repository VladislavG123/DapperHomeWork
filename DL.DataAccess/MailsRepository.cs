using DL.DataAccess.Abstract;
using DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using Dapper;

namespace DL.DataAccess
{
    public class MailsRepository : IRepository<Mail>
    {
        private DbConnection _connection;

        public MailsRepository()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["appConnection"].ConnectionString;

            _connection = new SqlConnection(connectionString);
        }

        public void Add(Mail item)
        {
            var sqlQuery = "insert into Mails (Id, CreationDate, DeletedDate, Theme, Text, ReceiverId) values (@Id, @CreationDate, @DeletedDate, @Theme, @Text, @ReceiverId)";
            var result = _connection.Execute(sqlQuery, item);
            if (result < 1) throw new Exception("Запись не вставлена");
        }

        public void Delete(Guid id)
        {
            Update(new Mail { Id = id, DeletedDate = DateTime.Now });
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public ICollection<Mail> GetAll()
        {
            var sqlQuery = "select * from Mails";
            return _connection.Query<Mail>(sqlQuery).AsList();
        }

        public void Update(Mail item)
        {
            var sqlQuery = "update Mails set ";
            if (item.DeletedDate != null)
            {
                sqlQuery += "DeletedDate = @DeletedDate ";
            }
            if (item.ReceiverId != null)
            {
                sqlQuery += "ReceiverId = @ReceiverId ";
            }
            if (item.Text != null)
            {
                sqlQuery += "Text = @Text ";
            }
            if (item.Theme != null)
            {
                sqlQuery += "Theme = @Theme";
            }
            sqlQuery += " where Id = @Id ";

            var result = _connection.Execute(sqlQuery, item);

            if (result < 1) throw new Exception("Запись не обновлена!");
        }
    }
}
