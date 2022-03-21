using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAdoNet.Services.Services.Fornecedor.Querys
{
    public class FornecedorQuerys
    {
        public string Select
        {
            get
            {
                return @"SELECT [Id]
                              ,[Nome]
                              ,[Email]
                              ,[Ativo]
                          FROM [dbo].[Fornecedor]";
            }
        }

        public string Insert
        {
            get
            {
                return @"INSERT INTO [dbo].[Fornecedor]
                                   ([Nome]
                                   ,[Email]
                                   ,[Ativo])
                             VALUES
                                   (@Nome
                                   ,@Email
                                   ,@Ativo)";
            }
        }

        public string Update
        {
            get
            {
                return @"UPDATE [dbo].[Fornecedor]
                                   SET [Nome] = @Nome
                                      ,[Email] = @Email
                                      ,[Ativo] = @Ativo
                                 WHERE [Id] = @Id";
            }
        }

        public string Delete
        {
            get
            {
                return @"DELETE [dbo].[Fornecedor] WHERE [Id] = @Id";
            }
        }

    }
}
