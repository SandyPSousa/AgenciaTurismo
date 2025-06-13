# ✈️ Agência de Turismo - Sistema de Gerenciamento

Sistema web desenvolvido em ASP.NET Core para gerenciamento de uma agência de turismo, incluindo controle de clientes, destinos, pacotes e reservas.

---

## ✅ Funcionalidades

* **Gerenciamento Completo (CRUD):**
    * Cadastro de Clientes, Países, Cidades, Pacotes Turísticos e Reservas.
    * Relacionamento de um-para-muitos (País > Cidades) e muitos-para-muitos (Pacotes > Cidades).
* **Regras de Negócio Avançadas:**
    * Validação de reservas para impedir overbooking (excesso de lotação).
    * Bloqueio de reservas para pacotes cuja data já passou.
    * Validação para impedir que um mesmo cliente reserve o mesmo pacote duas vezes.
* **Lógica de Exclusão (Soft Delete):**
    * Os registros não são apagados permanentemente do banco, garantindo a integridade e o histórico dos dados.
* **Autenticação & Autorização:**
    * Sistema de login simples com usuário e senha fixos no código.
    * Proteção de todas as páginas de cadastro, permitindo acesso apenas a usuários autenticados.
* **Recursos Avançados de C#:**
    * Uso de `Delegates` e `Func` para cálculos de negócio.
    * Implementação de `Events` para notificar quando a capacidade de um pacote é atingida.
    * Log de operações (criação de reserva) em console, arquivo e memória utilizando `multicast delegates`.

---

## 🛠️ Tecnologias

* **Backend:** C#, .NET 8
* **Framework:** ASP.NET Core Razor Pages
* **Banco de Dados:** Entity Framework Core 8 (Code-First) com SQL Server
* **Frontend:** HTML, CSS, Bootstrap 5

---

## ⚡ Como Executar

**1. Pré-requisitos:**
* .NET 8 SDK
* SQL Server (Express ou outra versão)

**2. String de Conexão:**
* No arquivo `appsettings.json`, ajuste a `DefaultConnection` para apontar para a sua instância do SQL Server.
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR_SQL_AQUI;Database=AgenciaTurismoDB;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False"
}
3. Banco de Dados:

No Visual Studio, abra o Package Manager Console e execute o comando para criar o banco e as tabelas:
PowerShell

update-database
4. Executar:

Pressione F5 no Visual Studio ou execute dotnet run no terminal.
🔑 Credenciais de Acesso
Usuário: admin
Senha: 123mudar
👨‍💻 Autor
Desenvolvido por [Seu Nome Aqui].
