
SET IDENTITY_INSERT Monografia.dbo.Algoritmo ON
Insert into Monografia.dbo.Algoritmo(Codigo,Descricao)
Select Codigo,Descricao from  Monografia2.dbo.Algoritmo
SET IDENTITY_INSERT Monografia.dbo.Algoritmo OFF

SET IDENTITY_INSERT Monografia.dbo.Algoritmo ON
Insert into Monografia.dbo.Elemento(ValorMinimo,ValorIdeal,ValorMaximo,Descricao)
Select ValorMinimo,ValorIdeal,ValorMaximo,Descricao from Monografia2.dbo.Elemento
SET IDENTITY_INSERT Monografia.dbo.Algoritmo OFF

SET IDENTITY_INSERT Monografia.dbo.DadosTreinamento ON
Insert into Monografia.dbo.DadosTreinamento (Codigo,Dados,Elemento,FiguraOriginal)
Select Codigo,Dados,Elemento,FiguraOriginal from Monografia2.dbo.DadosTreinamento
SET IDENTITY_INSERT Monografia.dbo.DadosTreinamento OFF

SET IDENTITY_INSERT Monografia.dbo.DadosValidacao ON
Insert into Monografia.dbo.DadosValidacao (Codigo,Dados,Elemento,FiguraOriginal)
Select Codigo,Dados,Elemento,FiguraOriginal from Monografia2.dbo.DadosValidacao
SET IDENTITY_INSERT Monografia.dbo.DadosValidacao OFF

SET IDENTITY_INSERT Monografia.dbo.DadosTreinamento OFF
Insert into Monografia.dbo.DadosTreinamento (Codigo,Dados,Elemento,FiguraOriginal)
SET IDENTITY_INSERT Monografia.dbo.DadosTreinamento OFF

SET IDENTITY_INSERT Monografia.dbo.DadosTreinamento OFF
Update Monografia.dbo.DadosTreinamento set Codigo = Codigo -1