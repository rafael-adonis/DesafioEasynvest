# Desafio Easynvest

## :white_check_mark: Visão Geral
O problema proposto no desafio poderia ter uma solução mais simples, mas como a idéia do desafio era demonstrar um pouco do conhecimento em padrões de mercado e boas práticas, a solução foi elaborada de uma forma um pouco mais complexa, porém de fácil manutenção e compreensão.

O problema proposto expôe 3 endpoints diferentes (Tesouro Direto, Renda Fixa e Fundos) para consulta de valores e é esperado um endpoint que retorne o valor total dos investimentos e liste todos os investimentos aplicando algumas regras para o cálculo de resgate, IR e rentabilidade.

## :cake: DDD ou Arquitetura em Camadas? 

__Apesar do paradigma criado na comunidade de desenvolvimento de software, DDD não é um arquitetura em camadas.__ 
DDD é um conceito arquitetural e não uma receita para todos os problemas de sistemas, ele pode ser aplicado desde uma solução simples em 3 camadas com o *table module pattern* até uma arquitetura mais complexa como *Onion, Hexagonal, Ports and Adapters* entre outros.

Sendo assim escolhi elaborar a solução em algumas camadas seguindo alguns princípios do DDD respeitando a responsabilidade de cada camada:

1. **Application/WebAPI**: Esta camada representa o ponto de acesso da aplicação, é a camada mais externa da aplicação, ela possui como responsabilidade expor o endpoint para a consulta dos investimentos consolidados, como também é responsável por realizar o carregamento da aplicação como um todo, esta aplicação deve ter a autonomia de receber e redirecionar as solicitações via web requests como também orquestrar a configuração de todo o ambiente da aplicação.

2. **Domain**: É o coração da aplicação, a camada mais interna e de certa forma a mais importante, responsável por modelar as entidades, segregar funcionalidades para todo o sistema através de interfaces, realizar o mapeamento de dados e executar as regras de negócio. 

3. **Infrastructure**: É uma camada de suporte, responsável por agrupar *features* que o domínio consome de forma a separar melhor responsabilidades. Assim o domínio pode se manter mais leve e focado no negócio e delegar responsabilidades para infra, tais como: acesso a dados, enfileiramento de requisições e outros.
4. **Utilities**: É também uma camada de suporte porém mais leve, responsável por agrupar funcionalidades que qualquer uma das outras camadas possa vir a utilizar, porém deve haver cuidado para que soluções de Infra não acabem migrando para essa camada, o que causaria um forte acoplamento das demais camadas com questões de Infra.

## :exclamation: Instruções para execução
* O projeto está configurado com 3 profiles no launchSettings.json, recomendo que utilize o profile *SelfHosted* para a execução e projeto Easynvest.Api como *Startup project*.

* Todos os profiles configurados estão configurados com o *Swagger*.

* As informações de HealthChecks está configurada na URL: https://localhost:5001/healthchecks-ui#/healthchecks

## :hammer_and_wrench: Padrões, API's e Ferramentas

* Asynchronous Programming: Todas as WebRequests são assíncronas e transmitem o CancellationToken até as camadas mais internas permitindo que solicitações long running seja canceladas, garantindo melhor performance ao servidor.

* Interface Segregation Principle: Os métodos e comportamentos que precisam ser acessados por outros projetos são expostos através de Interfaces bem definidas com escopo limitado. 

* Depnedency Injection: O projeto possui uma classe capaz de ser plugada na startup.cs para configurar o ServiceCllection e resolver todas as dependências.

* Observer Pattern / Notification Pattern: É possivel uitilizar notificações por toda a aplicação desde que sua interface seja injetada nos construtores. 

* FluentValidator: Para auxiliar nas validações de entidades.

* Swagger: Além permitir a interação com a API via sandbox também serve para documentar e especificar métodos, parâmetros, modelos.

* HealthChecking: Auxilia na monitoria da aplicação, no caso implementado apenas para a API, mas pode ser usado para diversos recursos.
Configurado no endereço: https://localhost:5001/healthchecks-ui#/healthchecks
https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks

## :dart: Testes 

Foram criados dois projetos de testes utilizando-se Xunit, um projeto para testes de unidade e outro para testes de integração.
Neste caso os testes de unidade basicamente testam as validações das entidades.
Já os testes de integração estão divididos em testes relacionados as operações dos Handlers etc

## :pray: Agradecimento 
Agradeço gentilmente a oportunidade de demonstrar um pouco de conhecimento nesse teste e também pelo interesse em meu perfil!


