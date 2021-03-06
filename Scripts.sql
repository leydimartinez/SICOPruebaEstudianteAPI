USE [SICOPrueba]
GO
/****** Object:  StoredProcedure [dbo].[Get_AllStudentData]    Script Date: 19/08/2021 7:33:14 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Get_AllStudentData] 
AS
BEGIN
	select * from Estudiante ORDER BY Id desc
END


USE [SICOPrueba]
GO
/****** Object:  StoredProcedure [dbo].[Save_StudentData]    Script Date: 19/08/2021 7:33:17 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Leydi Martinez
-- Description:	Guardar datos estudiante
-- =============================================
ALTER PROCEDURE [dbo].[Save_StudentData]
	 @TipoIdentificacion varchar(2),
      @Identificacion varchar(15),
      @Nombre1 varchar(20),
      @Nombre2 varchar(20),
      @Apellido1 varchar(20),
      @Apellido2 varchar(20),
      @Email varchar(50),
      @Celular varchar(20),
      @Direccion varchar(50),
      @Ciudad varchar(50)
AS
BEGIN
	INSERT INTO [dbo].[Estudiante]
           ([TipoIdentificacion]
           ,[Identificacion]
           ,[Nombre1]
           ,[Nombre2]
           ,[Apellido1]
           ,[Apellido2]
           ,[Email]
           ,[Celular]
           ,[Direccion]
           ,[Ciudad])
     VALUES
           (@TipoIdentificacion
           , @Identificacion
           ,@Nombre1
           ,@Nombre2
           ,@Apellido1
           ,@Apellido2
           ,@Email
           ,@Celular
           ,@Direccion
           ,@Ciudad)

SELECT SCOPE_IDENTITY() Id
 

END
