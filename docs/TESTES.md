# Verificação do CRUD de pacientes

Verificação realizada em 16/09/2026.

## Ambiente e resultados

- Projeto: `net9.0`; build com SDK 10.0.400, sem erros ou avisos no projeto Agendamento.
- Execução: runtime ASP.NET Core 9.0.20.
- EF Core 9.0.19 e Npgsql.EntityFrameworkCore.PostgreSQL 9.0.4, preservados da base do professor.
- Banco: PostgreSQL 18.6, instância separada de teste, com dados fictícios.
- Migrations aplicadas: `CriacaoInicial`, `AdicionarDataAnnotation` e `AdicionarPacientes`.
- `dotnet ef migrations has-pending-model-changes`: nenhuma alteração pendente.
- 67 verificações HTTP/SQL passaram. A contagem inclui a confirmação, após cada envio inválido, de que nenhum registro foi gravado.
- As quatro telas foram revisadas no navegador; o envio do formulário vazio exibiu as cinco mensagens de obrigatoriedade.

| Cenário | Resultado |
| --- | --- |
| Listar os três pacientes do seeding | Nomes, campos e datas exibidos corretamente |
| Reiniciar a aplicação | Sem duplicar médicos ou pacientes |
| Iniciar com médicos existentes e pacientes vazios | Pacientes populados corretamente |
| Enviar campos vazios, curtos ou longos demais | Formulário reapresentado; banco preservado |
| CPF com letras ou quantidade incorreta de dígitos | Rejeitado |
| Telefone com letras ou quantidade incorreta de dígitos | Rejeitado |
| Data vazia, impossível, anterior a 1900 ou futura | Rejeitada |
| Inserir paciente válido | Registro persistido e redirecionamento para a listagem |
| Enviar um Id no cadastro | Id enviado ignorado; banco gera a identidade |
| Abrir edição | Campos preenchidos, incluindo data no formato do input |
| Editar com campos inválidos | Registro original preservado |
| Editar paciente válido | Nome, data e demais alterações persistidos |
| Id do formulário diferente do Id da rota | HTTP 400 |
| Editar/remover registro inexistente | HTTP 404 |
| Abrir confirmação de remoção via GET | Nenhuma exclusão |
| Confirmar remoção via POST | Exclusão persistida e redirecionamento |
| POST sem token antiforgery em inserir/editar/remover | HTTP 400; banco preservado |
| Abrir listagem original de médicos | HTTP 200 |
| Carregar Bootstrap, jQuery e scripts de validação | HTTP 200 |

## Roteiro manual

1. Configure a conexão, aplique as migrations e execute em `Development`, conforme o README.
2. Abra `/Paciente` e confira os três pacientes fictícios.
3. Clique em **Inserir paciente** e envie o formulário vazio. Confira as mensagens nos cinco campos.
4. Cadastre um exemplo com nome, CPF de 11 números, telefone com DDD, endereço e data passada.
5. Confira o novo registro na listagem e atualize a página. O cadastro não deve ser reenviado.
6. Clique em **Editar**, altere nome e data e salve. Confira os valores na listagem.
7. Clique em **Remover** e depois em **Cancelar**. O paciente deve continuar cadastrado.
8. Abra novamente **Remover** e confirme. O paciente deve desaparecer.
9. Reinicie a aplicação. Enquanto a tabela tiver registros, o seeding não insere novamente os exemplos.

## Limites da verificação

Os testes HTTP usaram o assembly real da aplicação e um host auxiliar fora do repositório. Esse host manteve as chaves antiforgery em memória porque o ambiente de execução não tinha acesso ao armazenamento de chaves do perfil Windows. A verificação dos tokens permaneceu ativa. A persistência de chaves do perfil Windows e o certificado HTTPS local não foram validados nesse ambiente. Os arquivos da aplicação mantêm a configuração padrão do ASP.NET Core.

O projeto é um exercício acadêmico sem autenticação, conforme a base das aulas. O teste não representa uma avaliação de implantação para uso com dados reais de pacientes.
