using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAdoNet.Services.Services.User.Querys
{
    public class UserQuerys
    {

        public string Select
        {
            get
            {
                return @"SELECT [Id]
                                  ,[Name]
                                  ,[DateBirth]
                                  ,[Gender]
                                  ,[Email]
                                  ,[Active]
                              FROM [dbo].[User]";
            }

        }

        public string Insert
        {
            get
            {
                return @"INSERT INTO [dbo].[User]
                                       ([Name]
                                       ,[DateBirth]
                                       ,[Gender]
                                       ,[Email]
                                       ,[Active])
                                 VALUES
                                       (@Name
                                       ,@DateBirth
                                       ,@Gender
                                       ,@Email
                                       ,@Active)
                        SELECT SCOPE_IDENTITY()";
            }

        }

        public string Update
        {
            get
            {
                return @"UPDATE [dbo].[User]
                           SET [Name] = @Name
                              ,[DateBirth] = @DateBirth
                              ,[Gender] = @Gender
                              ,[Email] = @Email
                              ,[Active] = @Active
                         WHERE [Id] = @Id";
            }
        }

        public string Delete
        {
            get
            {
                return @"DELETE [dbo].[User] WHERE [Id] = @Id";
            }
        }

    }
}
