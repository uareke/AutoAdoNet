# AutoAdoNet-
Gerado automatico de OBJETO apartir de um DataReader.

Sempre tinha que reenscrever codigo, deixando o codigo sempre enorme e repetitivo. Em todas as CRUDS que eu criava sempre ou sistema que eu dava manutenção em ADO.NET 
tinha que adicionar os codigos:


           try
             {
                 var watch = System.Diagnostics.Stopwatch.StartNew();
                 var TotalRegistros = 0;
                 var count = 1;
                 var sql = $@"SELCT * FROM TABELADADOSENTREGA";
                 var listagem = new List<ArmazemDocumentosDto>();

                 using (SqlConnection connection = new SqlConnection(Debugging.Environment.ConnectionString))
                 {
                     using (SqlCommand command = new SqlCommand(sql, connection))
                     {
                         command.CommandType = CommandType.Text;
                         command.CommandTimeout = 7200;
                         command.Parameters.Add(new SqlParameter("@STRCHAVEACESSOCTE", input.CHAVECTE));

                         connection.Open();
                         var dr = command.ExecuteReader();

                         while (dr.Read())
                         {
                             TotalRegistros++;
                             listagem.Add(new ArmazemDocumentosDto()
                             {
                                 ID = dr.GetString(dr.GetOrdinal("CHRTIPO")),
                                 CHRTIPO = dr.GetString(dr.GetOrdinal("CHRTIPO")), //string
                                 UNI = dr.GetString(dr.GetOrdinal("UNI")), //string
                                 LNGCONTROLE = dr.GetInt32(dr.GetOrdinal("LNGCONTROLE")),//int
                                 DTAEMISSAO = dr.GetDateTime(dr.GetOrdinal("DTAEMISSAO")), //date
                                 DTACANCEL = dr.GetDateTime(dr.GetOrdinal("DTACANCEL")), //date
                                 REMETENTE = dr.GetString(dr.GetOrdinal("REMETENTE")) //string
                             });
                         }
                     }
                 }
                 watch.Stop();
                 
Sempre repetindo codigo, criando parametros manuais dai pensei chega de tanta dor de cabeça, consegui resurmir todo esse codigo em apenas algumas linhas. E retornando a query diretamente dentro de um "objeto DTO".

Usando somente essas linhas:

            try
            {
                var dados = _helperService.ExecutaQueryReader<UserDto>(_querys.Select, new UserInput());
                return dados;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

![image](https://user-images.githubusercontent.com/18741973/159191273-34d8970a-1ac8-4976-a231-e4064d20067e.png)

Entendendo o codigo:

**_helperService**  Nome do meu serviço

**ExecutaQueryReader** Método que executa a query

**UserDto** Objeto que vai receber a pesquisa da query
  
**_querys.Select** tipo de query neste caso estou fazendo um select
  
**new UserInput** Parametr4os para a pesquisa.


