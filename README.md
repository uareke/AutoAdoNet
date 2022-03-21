![AutoAdoNet](https://user-images.githubusercontent.com/18741973/159269539-678ac171-cf1a-4272-ac90-c779eef9b158.png)
---
[![GitHub issues](https://img.shields.io/github/issues/uareke/AutoAdoNet-)](https://github.com/uareke/AutoAdoNet-/issues)
[![GitHub forks](https://img.shields.io/github/forks/uareke/AutoAdoNet-)](https://github.com/uareke/AutoAdoNet-/network)
[![GitHub stars](https://img.shields.io/github/stars/uareke/AutoAdoNet-)](https://github.com/uareke/AutoAdoNet-/stargazers)
![GitHub](https://img.shields.io/github/license/uareke/AutoAdoNet)
![Linguagem](https://img.shields.io/static/v1?label=ASP.NET&message=Core%205.0&color=gren)
![Banco de dados](https://img.shields.io/static/v1?label=DataBase&message=MSSQL%20Server&color=gren)
![GitHub Org's stars](https://img.shields.io/github/stars/uareke/AutoAdoNet-?style=social)
![GitHub release (latest by date including pre-releases)](https://img.shields.io/github/v/release/uareke/AutoAdoNet-?include_prereleases)


![Badge em Desenvolvimento](http://img.shields.io/static/v1?label=STATUS&message=EM%20DESENVOLVIMENTO/EVOLUÇÃO&color=GREEN&style=for-the-badge)



### Tópicos 

:small_blue_diamond: [Descrição do projeto](#descrição-do-projeto)

:small_blue_diamond: [Funcionalidades](#funcionalidades)

:small_blue_diamond: [Dependências](#dependências)

:small_blue_diamond: [Serviços](#serviços)

:small_blue_diamond: [WEB API](#web-api)


## Descrição do projeto 

<p align="justify">
Com o tempo fazendo manutenção ou criando sistemas baseados em ADO.NET, cheguei à conclusão que todos os serviços eu tinha que digitar sempre o mesmo código.  

```C# {.line-numbers}
try 
  { 
      var watch = System.Diagnostics.Stopwatch.StartNew(); 
      var TotalRegistros = 0; 
      var count = 1; 
      var sql = $@"Select * from Users WHERE id = @id"; 
      var listagem = new List<UsuariosDto>(); 
      using (SqlConnection connection = new SqlConnection(Debugging.Environment.ConnectionString)) 
      { 
          using (SqlCommand command = new SqlCommand(sql, connection)) 
          { 
              command.CommandType = CommandType.Text; 
              command.CommandTimeout = 7200; 
              command.Parameters.Add(new SqlParameter("@id", input.id)); 
              connection.Open(); 
              var dr = command.ExecuteReader(); 
              while (dr.Read()) 
              { 
                  TotalRegistros++; 
                  listagem.Add(new UsuariosDto () 
                  { 
                       Id = dr.GetInt32(dr.GetOrdinal("LNGPOLOCOLETA")), 
                       Nome = dr. GetString (dr.GetOrdinal("Nome")), 
                       . 
                       . 
                       . 
                       . 
                       . 
                       Ativo= dr.GetBoolean(dr.GetOrdinal(" Ativo ")) 
                 }); 
              } 
          } 
      } 
      watch.Stop(); 
      return new LIst< UsuariosDto >(); 
  } 
 catch (Exception ex) 
  { 
      throw new Exception(ex.Message); 
  } 
``` 
Tenho mesmo que todo CRUD de serviços digitar esse monte de linhas? Será que não consigo reduzir o código para ficar mais limpo e fácil de compreender? Sem contar com os campos que tenho que mapear manualmente do DataReader. Fui criar uma nova feature para um cliente onde a tabela dele tinha 87 campos. :dizzy_face::dizzy_face:
  
 Falei ah chega de ficar me matando hora de colocar a cabeça para funcionar.:exploding_head:.
  Então a ideia surgiu!!!:bulb::bulb::bulb::bulb:
  
  Se eu criar um serviço onde eu somente informo minha query e parametros é o sistema faz tudo automaticamente e já mapeia os dados dentro de um DTO?
  
  Foi dai que surgiu a ideia desse projeto AutoAdoNet.
  
</p>

## Funcionalidades
* [IHelperService](#iHelperService)
    * [ExecutaQueryReader](#executaQueryReader)
    * [ExecutaQuery](#executaQueryReader)           
    * [DRMapToList](#executaQueryReader)
    * [CreateSqlParameter](#executaQueryReader)
    * [GeraQuery](#executaQueryReader)
  
           
           
### IHelperService
           Serviço que faz toda a magica funcionar, ela contem os métodos..
           
### ExecutaQueryReader (Método publico)
           
Método que executa comandos DQL no banco de dados. (Select)
       
Modo de uso:
           
![image](https://user-images.githubusercontent.com/18741973/159326129-0a151966-5469-4b40-aa8b-be58d20d9d43.png)
           
           
```C#
 try
 {
     var input = new UserInput() { Id = Id };
     var dados = _helperService.ExecutaQueryReader<UserDto>(_querys.Select, input);
     return dados;

 }
 catch (Exception ex)
 {
     throw new Exception(ex.Message);
 }
```
 
![image](https://user-images.githubusercontent.com/18741973/159328699-3fcfe574-06cc-41c1-8d93-cfb03b419b2a.png)

           
           
### ExecutaQuery  (Método publico)
           
Método que executa comandos DML no banco de dados. (Insert , Update e Delete)
           

### DRMapToList  (Método privado)
           
Método que recebe um DataReader e transfoma o mesmo em um objeto.
           

### CreateSqlParameter (Método privado)
           
(Método privado)
           
### GeraQuery (Método privado)
           
Método experimentar ainda em fase de aprimoramento que gera comandos DQL automaticamente.


           
## WEB API


## Casos de Uso

Explique com mais detalhes como a sua aplicação poderia ser utilizada. O uso de **gifs** aqui seria bem interessante. 

Exemplo: Caso a sua aplicação tenha alguma funcionalidade de login apresente neste tópico os dados necessários para acessá-la.


Se quiser, coloque uma amostra do banco de dados 

## Iniciando/Configurando banco de dados

Se for necessário configurar algo antes de iniciar o banco de dados insira os comandos a serem executados 

## Linguagens, dependencias e libs utilizadas :books:

- [React](https://pt-br.reactjs.org/docs/create-a-new-react-app.html)
- [React PDF](https://react-pdf.org/)

...

Liste as tecnologias utilizadas no projeto que **não** forem reconhecidas pelo Github 

## Resolvendo Problemas :exclamation:

Em [issues]() foram abertos alguns problemas gerados durante o desenvolvimento desse projeto e como foram resolvidos. 

## Tarefas em aberto

Se for o caso, liste tarefas/funcionalidades que ainda precisam ser implementadas na sua aplicação

:memo: Tarefa 1 

:memo: Tarefa 2 

:memo: Tarefa 3 

## Desenvolvedores/Contribuintes :octocat:

Liste o time responsável pelo desenvolvimento do projeto

| [<img src="https://avatars.githubusercontent.com/u/18741973?s=400&u=85f4ce3e928db7a1bb4f16942c3c95c788cf239b&v=4" width=115><br><sub>Alex S.M. de Araujo</sub>](https://github.com/uareke) | | |
| :---: | :---: | :---: 




## Licença 

The [MIT License](https://github.com/uareke/AutoAdoNet/blob/main/LICENSE) (MIT)

Copyright :copyright: 2022 - AutoAdoNet


























































###TESTE
