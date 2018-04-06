# prmToolkit.XLog
Projeto responsável por gerenciar logs de forma simples


### Installation - prmToolkit.XLog

Para instalar, abra o prompt de comando Package Manager Console do seu Visual Studio e digite o comando abaixo:

Instalando pacote Nuget
```sh
Install-Package prmToolkit.XLog.SqlServer
```


### Como usar

Após instalar o pacote nuget:

```sh
 //Criar uma instancia do log passando o nome da aplicação e string de conexão do banco de dados
 var log = new Log("prmToolkit.XLog", @"Server=.\sqlexpress;Database=Log;Trusted_Connection=True;");

//Formas de gravar o log

log.Save("Gravando meu log"); //Grava como information
log.Save("Gravando meu log", Domain.Enum.EnumMessageType.Error); //Grava como Error ou Warning selecionado no Enum
log.SaveAsync("Gravando meu log"); //Grava de forma assincrona

```
