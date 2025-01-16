Store API

Visão geral A API Store é um aplicativo back-end criado usando a estrutura .NET 8. Ele aplica as melhores práticas em Domain-Driven Design (DDD), CQRS e arquitetura limpa. O aplicativo foi projetado para cenários do mundo real e inclui testes robustos e conteinerização para fácil implantação.

Apresenta Design Orientado a Domínio (DDD) para modelar lógica de negócios complexa. CQRS para separação de comandos e consultas. MediatR para manipulação de comandos, consultas e notificações. Migrações de banco de dados com Entity Framework Core. Testes abrangentes usando xUnit, Moq e FluentAssertions. ErrorOr para tratamento de erros padronizado. Suporte Docker para conteinerização.

Tecnologias utilizadas: .NET 8 Entity Framework Core MediatR xUnit, Moq, FluentAssertions ErrorOr Docker

Primeiros passos de Pré-requisitos Certifique-se de ter o seguinte instalado:
.NET SDK 8 Docker SQL Server

Instalação primeiro clone o repositório:
git clone https://github.com/marcelovbusto/Order.git
cd store

Restaurar dependências:
dotnet restore

Configure a string de conexão do seu banco de dados em: appsettings.json or appsettings.Development.json: appsettings.Development.json
{
    "ConnectionStrings": {
        "SqlServer": "Server=localhost;Database=store_db;Trusted_Connection=True;TrustServerCertificate=True;"
    }
}

Uso em execução local de migrações de banco de dados:
dotnet ef database update --project Infrastructure --startup-project Store.API

Inicie o aplicativo
dotnet run --project Store.API

Acesse a documentação da API Swagger em:
https://localhost:5001/swagger

Executando com Docker Build e execute os contêineres Docker:
docker-compose up

O aplicativo estará acessível em:
http://localhost:5000

Executar todos os testes de unidade com:
dotnet test

O projeto usa: xUnit para testes unitários. Moq para zombar de dependências. FluentAssertions para afirmações legíveis.

Arquitetura A aplicação segue o padrão de arquitetura limpa, estruturada em camadas: Apresentação: Trata solicitações HTTP e interações do usuário. Aplicativo: contém comandos, consultas e lógica de negócios. Domínio: lógica de negócios central, agregações e eventos de domínio. Infraestrutura: Acesso a dados, serviços externos e persistência.
