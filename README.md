# Agendamento - pacientes

Trabalho 1 de Desenvolvimento Web com .NET. Extensão da aplicação de agendamento para gerenciar pacientes.

Base de aula: [Agendamento, do professor Lucas Teodoro](https://github.com/teodorolucaas/DesenvolvimentoWebDotNet/tree/48938f08fd3927ce0ee4c7a0a0e2fb4bfb20d5f6/Agendamento). O projeto inicial e a funcionalidade de médicos vêm dessa base.

Tecnologias: ASP.NET Core MVC (.NET 9), Razor, Entity Framework Core 9 e PostgreSQL.

## Funcionalidades

- Listar pacientes com nome, CPF, telefone, endereço e data de nascimento.
- Inserir e editar pacientes com Data Annotations e mensagens de validação.
- Remover pacientes após confirmação em uma tela própria.
- Criar a tabela `Pacientes` com migration do EF Core.
- Popular três pacientes fictícios no ambiente de desenvolvimento.

## Executar

Pré-requisitos: SDK do .NET 9 e PostgreSQL em execução. O SDK deve incluir o runtime ASP.NET Core 9; somente o runtime 10 não executa aplicações `net9.0` por padrão.

Clone o repositório e entre na pasta do projeto:

```powershell
git clone https://github.com/jpbuganza2020-max/agendamento-pacientes.git
cd agendamento-pacientes/Agendamento
dotnet restore
```

No mesmo terminal PowerShell, configure a conexão com seu PostgreSQL. Substitua `SUA_SENHA` pela senha local. A variável vale para esse terminal e evita salvar a senha no Git:

```powershell
$env:ConnectionStrings__DefaultConnection = "Host=localhost;Port=5432;Database=AgendamentoPacientes;Username=postgres;Password=SUA_SENHA"
```

Instale a ferramenta de migrations, caso ainda não esteja instalada:

```powershell
dotnet tool install --global dotnet-ef --version 9.0.19
```

Se já houver outra versão instalada, use `dotnet tool update --global dotnet-ef --version 9.0.19 --allow-downgrade`. Então aplique as migrations e execute:

```powershell
dotnet ef database update
dotnet run --launch-profile http
```

Abra [a listagem de pacientes](http://localhost:5169/Paciente). O perfil `http` define o ambiente `Development`, no qual o `SeedingService` é executado. O banco precisa ser atualizado **antes** de iniciar a aplicação. O usuário PostgreSQL deve ter permissão para criar o banco, ou ele deve ser criado previamente.

No Visual Studio, abra `Agendamento/Agendamento.sln`. No Console do Gerenciador de Pacotes, com `Agendamento` selecionado como projeto padrão, o comando equivalente é `Update-Database`. Configure a conexão no ambiente usado pelo Visual Studio ou substitua localmente o marcador em `appsettings.json`, sem enviar sua senha ao GitHub.

As migrations já estão versionadas. `Add-Migration AdicionarPacientes` / `dotnet ef migrations add AdicionarPacientes` foi usado durante o desenvolvimento e não precisa ser executado novamente para rodar o projeto.

## Organização

| Arquivo ou pasta | Responsabilidade |
| --- | --- |
| `Models/Paciente.cs` | Propriedades, validações e mapeamento da data |
| `Models/Validations/` | Atributos de validação de CPF e data de nascimento |
| `Data/AppDbContext.cs` | Disponibiliza `DbSet<Paciente>` |
| `Migrations/` | Histórico da estrutura do banco |
| `Data/SeedingService.cs` | Dados iniciais de médicos e pacientes |
| `Services/PacienteService.cs` | Consultar, inserir, editar e remover no banco |
| `Controllers/PacienteController.cs` | Requisições GET/POST, validação e redirecionamento |
| `Views/Paciente/` | Listagem, cadastro, edição e confirmação de remoção |
| `Program.cs` | Registro dos serviços, middleware e rotas |

O fluxo segue as aulas: navegador → controller → service → contexto/EF Core → PostgreSQL. A controller envia os dados para uma view Razor. As operações do service são síncronas, com `ToList`, `Find`, `Add`, `Remove` e `SaveChanges`.

## Regras dos campos

| Campo | Regra |
| --- | --- |
| Nome | Obrigatório, de 3 a 100 caracteres |
| CPF | Obrigatório, exatamente 11 números, sem pontuação, com dígitos verificadores válidos |
| Telefone | Obrigatório, 10 ou 11 números, com DDD sem zero inicial |
| Endereço | Obrigatório, de 5 a 200 caracteres |
| Data de nascimento | Obrigatória, de 01/01/1900 até hoje |

CPF e telefone são textos. O CPF preserva zeros à esquerda e é validado pelo formato e pelos dois dígitos verificadores; sequências com todos os dígitos iguais são rejeitadas. Não há consulta à Receita Federal nem regra de unicidade de CPF. Os valores do seeding são exemplos para demonstração, com verificadores calculados; não representam uma confirmação de identidade.

Os atributos `CpfAttribute` e `DataNascimentoAttribute` centralizam essas regras no modelo. Os formulários verificam `ModelState.IsValid` antes de salvar. Fora do MVC, execute `Validator.ValidateObject` com `validateAllProperties: true`, pois `SaveChanges()` não executa as validações de Data Annotations.

`DataNascimento` é `DateTime?` para representar o formulário vazio. `[Required]` impede gravar sem data e `[Column(TypeName = "date")]` mantém apenas a data no PostgreSQL. A migration gera a coluna como `NOT NULL`.

O seeding verifica cada tabela com `Any()`. Reiniciar com dados existentes não os duplica. Se todos os pacientes forem removidos, a próxima inicialização em desenvolvimento recriará os três exemplos, conforme esse método ensinado em aula.

A integração das validações não altera o esquema do banco nem exige outra migration. Registros existentes são preservados. Caso seu banco ainda tenha os CPFs antigos de exemplo (`00000000001`, `00000000002` e `00000000003`), corrija o CPF ao editar esses pacientes: a nova validação rejeita esses valores. O seeding atualizado vale para tabelas ainda vazias.

## Validação da implementação

O projeto compilou sem erros ou avisos. As três migrations foram aplicadas em PostgreSQL, e o EF Core confirmou ausência de alterações de modelo pendentes. O CRUD e os casos inválidos foram exercitados por requisições HTTP, com conferência dos registros no banco.

Veja os cenários, resultados e limites do ambiente em [docs/TESTES.md](docs/TESTES.md).

## Referência

Projeto do professor Lucas Teodoro dos Santos, UNIP: [DesenvolvimentoWebDotNet/Agendamento](https://github.com/teodorolucaas/DesenvolvimentoWebDotNet/tree/48938f08fd3927ce0ee4c7a0a0e2fb4bfb20d5f6/Agendamento), revisão `48938f08fd3927ce0ee4c7a0a0e2fb4bfb20d5f6`. A base foi adaptada para acrescentar o gerenciamento de pacientes solicitado no Trabalho 1. As bibliotecas de terceiros mantêm seus arquivos de licença.
