# AutoAdoNet-
Gerado automatico de OBJETO apartir de um DataReader.

-  Criando um novo Serviço para uso
-  



Sempre tinha que reenscrever codigo, deixando o codigo sempre enorme e repetitivo. Em todas as CRUDS que eu criava sempre ou sistema que eu dava manutenção em ADO.NET 
tinha que adicionar os codigos:

```C#
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
```                 
                 
Sempre repetindo codigo, criando parametros manuais dai pensei chega de tanta dor de cabeça, consegui resurmir todo esse codigo em apenas algumas linhas. E retornando a query diretamente dentro de um "objeto DTO".

Usando somente essas linhas:

```C#
            try
            {
                var dados = _helperService.ExecutaQueryReader<UserDto>(_querys.Select, new UserInput());
                return dados;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
```

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

FornecedorDto
```C#
    public class FornecedorDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
    }
```    
    
FornecedorInput
```C#
    public class FornecedorDto
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool? Ativo { get; set; }
    }
```    

Dentro da pasta *Querys* vamos criar o arquivo *FornecedorQuerys*;

![image](https://user-images.githubusercontent.com/18741973/159194076-6b836160-c751-4c93-a193-dcacbe766a00.png)

Vamos codificar nossas querys:

```C#
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
                                   ,@Ativo)
                        SELECT SCOPE_IDENTITY()";
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
```
    
Agora vamos criar nossa interface e nossa classe para o serviço de fornecedor.

![image](https://user-images.githubusercontent.com/18741973/159194417-f5d83f9a-73d9-4f07-a7d5-e5a64588f6fa.png)


A codificação do *IFornecedorService*, deve ficar assim:

```C#
 public interface IFornecedorService
    {

        Task<List<FornecedorDto>> Get();

        Task<List<FornecedorDto>> Get(int Id);

        Task<int> Insert(FornecedorInput input);

        Task<int> Update(FornecedorInput input);

        Task<int> Delete(FornecedorInput input);

    }
```
           
Vamos implementar nosso serviço com nossa Interface:

```C#
  public class FornecedorService : IFornecedorService
    {
        public Task<int> Delete(FornecedorInput input)
        {
            throw new NotImplementedException();
        }

        public Task<List<FornecedorDto>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<List<FornecedorDto>> Get(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Insert(FornecedorInput input)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(FornecedorInput input)
        {
            throw new NotImplementedException();
        }
    }
``` 

Agora vem o pulo do GATO :cat:.
Toda chamada de serviço eu tinha que declarar 

```
      using (SqlConnection connection = new SqlConnection(Debugging.Environment.ConnectionString))
      {
          using (SqlCommand command = new SqlCommand(sql, connection))
    ..
    ..
    ..
    .. BLA BLA BLA
```   

Com o serviço *IHelperService*, ja pronto tudo isso. Evito ter que ficar toda hora reescrevendo esse codigo. Agora posso simplesmente passar 2 linhas de comando que vai funcionar.

Vamos implementar o metodo GET para mostrar todos os registros na tabela Fornecedor.

```C#
        public async Task<List<FornecedorDto>> Get()
        {

            try
            {
                var dados = _helperService.ExecutaQueryReader<FornecedorDto>(_querys.Select, new FornecedorInput());
                return dados;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
```        

Ok Ok, antes que voçê me xingue tem 9 linhas, é que estou tratando qualquer tipo de erro. :grinning:

![image](https://user-images.githubusercontent.com/18741973/159250987-d50215c2-c4d1-42d0-b999-6701327a0ac0.png)


Veja que temos alguns erros, vamos resolver isso.
- Primeiro fazer inject do nosso serviço *IHelperService*
- Depois declarar using *FornecedorInput*
- Depois instanciar nossa classe de Querys *_querys*

```C#
        #region INJECAO DEPENDENCIA
        private readonly IHelperService _helperService;
        private readonly FornecedorQuerys _querys;

        public FornecedorService(
               IHelperService helperService
               )
        {
            _helperService = helperService;
            _querys = new FornecedorQuerys();
        }
        #endregion
```        

![image](https://user-images.githubusercontent.com/18741973/159250132-1fa9c78a-9e86-4b78-875c-395639c05719.png)

Não precisa se preocupar com o *Container DI* da injeção de dependencia, já deixei o caminho das pedras prontos utilizando [SimpleInject](https://simpleinjector.org/).
Onde dentro do *Startup* ele já detecta todas as classes e injeta automaticamente dentro de nosso container.

![image](https://user-images.githubusercontent.com/18741973/159249783-f479823a-3069-4da4-8bdb-90b341bc69db.png)


Agora implemente o metodo *Get* que vai parametros:

```C#
        public async Task<List<FornecedorDto>> Get(int Id)
        {

            try
            {
                var dados = _helperService.ExecutaQueryReader<FornecedorDto>(_querys.Select, new FornecedorInput() { Id = Id });
                return dados;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
```        
Agora que vem o pulo do gato 2 :cat::cat:, onde os parametros passados vão virar os parametros para nossa pesquisa.
Nossa query de pesquisa para o metodo *GET* é:
```SQL
SELECT [Id]
        ,[Nome]
        ,[Email]
        ,[Ativo]
    FROM [dbo].[Fornecedor]
```                          

Em nossa chamada estamos enviando o parametro :

![image](https://user-images.githubusercontent.com/18741973/159252860-1313d068-c313-44c2-bc41-a3764d966b9d.png)

O sistema vai automaticamente gerar a query :

```SQL
SELECT [Id]
        ,[Nome]
        ,[Email]
        ,[Ativo]
    FROM [dbo].[Fornecedor]
    WHERE [Id] = @Id
```

Vamos implementar os metodos;
- Insert
- Update
- Delete

```C#
    public async Task<int> Insert(FornecedorInput input)
        {

            try
            {
                var dados = _helperService.ExecutaQuery<FornecedorDto>(_querys.Insert, input, QueryType.Insert);
                return dados;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<int> Update(FornecedorInput input)
        {
            try
            {
                var dados = _helperService.ExecutaQuery<FornecedorDto>(_querys.Update, input, QueryType.Update);
                return dados;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Delete(FornecedorInput input)
        {
            try
            {
                var dados = _helperService.ExecutaQuery<FornecedorDto>(_querys.Delete, input, QueryType.Delete);
                return dados;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
```        

Para os comandos DML: *INSERT*, *UPDATE* e *DELETE* utilizamos um metodo diferente:

```C#
_helperService.ExecutaQuery<FornecedorDto>(_querys.Insert, input, QueryType.Insert);
```

![image](https://user-images.githubusercontent.com/18741973/159257565-55c768fd-dccd-4a07-b2fb-5bb9c46561de.png)

Com isso já temos um serviço pronto para ser consumido por API ou UI MVC.

----

