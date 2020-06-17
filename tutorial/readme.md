# Criando uma WebAPI com dotnet core e C#

Neste tutorial, será criado uma WebAPI REST, utilizando dotnet core e C#.


**Agenda**

 - Resultado final esperado
 - Preparando o ambiente
   - instalar o Visual Studio Code (VSCode)
   - instalar o SDK do dotnet core
   - instalar o plugin do C# no VSCode
   - instalar docker (para rodar PostgreSQL, Mysql, MongoDB e também a aplicação WebAPI)
   - instalando clientes para os bancos de dados PostgreSQL, Mysql, MongoDB
 - Criando a WebAPI (sem persistir os dados)
   - observando os padrões de projeto Repository, Service Layer
 - Persistir os dados da API no PostgreSQL
   - rodando o PostgreSQL no docker
   - criando camada de persitência com ADO.NET
   - criando camada de persistência com Dapper (microframework ORM)
   - criando camada de persistência com EntityFramework (framework ORM)
 - Persistir os dados da API no Mysql
   - rodando o Mysql no docker
   - criando camada de persitência com ADO.NET
   - criando camada de persistência com Dapper (microframework ORM)
   - criando camada de persistência com EntityFramework (framework ORM)
 - Persistir os daods da API no MongoDB - Banco de Dados não relacional
 - Preparando WebAPI para rodar no docker

## Resultado final esperado

## Preparando o ambiente

Nesta etapa vamos:
 - [instalar o Visual Studio Code (VSCode)](https://code.visualstudio.com/)
 - [instalar o SDK do dotnet core](https://dotnet.microsoft.com/download)
 - instalar o plugin do C# no VSCode
 - instalar docker (para rodar PostgreSQL, Mysql, MongoDB e também a aplicação WebAPI)
 - instalando clientes para os bancos de dados PostgreSQL, Mysql, MongoDB

