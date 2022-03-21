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
  
**new UserInput** Parametros para a pesquisa.


-----
<h1> Criando um novo Serviço para uso: </h1>

Dentro do projeto *AutoAdoNet.Services -> Services*

![image](https://user-images.githubusercontent.com/18741973/159192868-1839edb1-2a00-4a44-99f0-e09cb722701c.png)

Crie a pasta para o novo serviço: No meu caso *Fornecedor*.

![image](https://user-images.githubusercontent.com/18741973/159192907-c8f15a59-8750-40f0-9448-37a9d7c5fef7.png)

Dentro da pasta fornecedor vamos criar 4 novas pastas:

- Dto
- Input
- Querys
- Services

![image](https://user-images.githubusercontent.com/18741973/159192976-ea48ccdf-84c8-4ca5-95ce-26aa2438c431.png)

Depois de criar a estrutura do serviço, vamos criar o Model Fornecedor dentro do projeto *AutoAdoNet.Models*

![image](https://user-images.githubusercontent.com/18741973/159193035-9c01bfc0-387d-43a2-bec6-3ff07b2e870a.png)

Eu poderia criar diretamente dentro do projeto o *FornecedorModel*, mas para organização de codigo vamos criar uma pasta e a classe.

![image](https://user-images.githubusercontent.com/18741973/159193140-87f3bcae-40c9-4a3c-9295-18d03c6cbf59.png)

Dentro de nosso model vamos criar as propriedades:

![image](https://user-images.githubusercontent.com/18741973/159193220-67327ca5-f43e-4504-94b1-2905770eba0b.png)

<h4> Desculpe mas a parte de geração de tabela automatica ainda estou trabalhando nela, por isso vou deixar o script aqui </h4>

![image](https://user-images.githubusercontent.com/18741973/159193476-ee311866-586b-4cfd-8331-efd2c3f17513.png)

Agora vamos voltar para nosso service. Vamos criar nosso *FornecedorDto* e *FornecedorInput*.

![image](https://user-images.githubusercontent.com/18741973/159193678-371e659b-87a3-4361-b22a-21dc6e6fd50b.png)

Suas propriedades são as seguintes:

<h3> FornecedorDto </h3>
![image](https://user-images.githubusercontent.com/18741973/159193705-644e4af0-ada2-4153-8399-15bdd18ee071.png)

<h3> FornecedorInput </h3
![image](https://user-images.githubusercontent.com/18741973/159193747-c0810c8f-f28c-4687-b395-c6513c5037fb.png)

           
           

